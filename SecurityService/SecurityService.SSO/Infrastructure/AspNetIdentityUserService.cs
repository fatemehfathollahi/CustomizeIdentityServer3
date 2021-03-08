using IdentityModel;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.AspNet.Identity;
using SecurityService.Infrastructure.Data;
using SecurityService.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Constants = IdentityServer3.Core.Constants;

namespace SecurityService.SSO.Infrastructure
{
    public class AspNetIdentityUserService<TUser, TKey> : UserServiceBase
        where TUser : class, Microsoft.AspNet.Identity.IUser<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        public string DisplayNameClaimType { get; set; }
        public bool EnableSecurityStamp { get; set; }

        protected readonly UserManager<TUser, TKey> userManager;

        protected readonly Func<string, TKey> ConvertSubjectToKey;

        private readonly OwinEnvironmentService _owinEnv;

        public AspNetIdentityUserService(Microsoft.AspNet.Identity.UserManager<TUser, TKey> userManager,
            OwinEnvironmentService env,//add fathollahi
            Func<string, TKey> parseSubject = null)
        {
            _owinEnv = env;//add fathollahi
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

            if (parseSubject != null)
            {
                ConvertSubjectToKey = parseSubject;
            }
            else
            {
                Type keyType = typeof(TKey);
                if (keyType == typeof(string))
                {
                    ConvertSubjectToKey = subject => (TKey)ParseString(subject);
                }
                else if (keyType == typeof(int))
                {
                    ConvertSubjectToKey = subject => (TKey)ParseInt(subject);
                }
                else if (keyType == typeof(uint))
                {
                    ConvertSubjectToKey = subject => (TKey)ParseUInt32(subject);
                }
                else if (keyType == typeof(long))
                {
                    ConvertSubjectToKey = subject => (TKey)ParseLong(subject);
                }
                else if (keyType == typeof(Guid))
                {
                    ConvertSubjectToKey = subject => (TKey)ParseGuid(subject);
                }
                else
                {
                    throw new InvalidOperationException("Key type not supported");
                }
            }

            EnableSecurityStamp = true;
        }

        private object ParseString(string sub)
        {
            return sub;
        }

        private object ParseInt(string sub)
        {
            if (!int.TryParse(sub, out int key))
            {
                return 0;
            }

            return key;
        }

        private object ParseUInt32(string sub)
        {
            if (!uint.TryParse(sub, out uint key))
            {
                return 0;
            }

            return key;
        }

        private object ParseLong(string sub)
        {
            if (!long.TryParse(sub, out long key))
            {
                return 0;
            }

            return key;
        }

        private object ParseGuid(string sub)
        {
            if (!Guid.TryParse(sub, out Guid key))
            {
                return Guid.Empty;
            }

            return key;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext ctx)
        {
            ClaimsPrincipal subject = ctx.Subject;
            IEnumerable<string> requestedClaimTypes = ctx.RequestedClaimTypes;

            string client = ctx.Client.ClientId;

            if (subject == null)
            {
                throw new ArgumentNullException("subject");
            }

            TKey key = ConvertSubjectToKey(subject.GetSubjectId());
            TUser acct = await userManager.FindByIdAsync(key);
            if (acct == null)
            {
                throw new ArgumentException("Invalid subject identifier");
            }

            IEnumerable<Claim> claims = await GetClaimsFromAccount(acct);
            List<Claim> claimList = claims.ToList();

            IdentityContext context = new IdentityContext(ConfigurationManager.ConnectionStrings["SecurityDBContext"].Name);

            ApplicationUser currectUser = context.Users.FirstOrDefault(o => o.UserName == acct.UserName);
            foreach (UserClientPermissions item in context.UserClientPermissions.ToList())
            {
                context.Entry(item).Reference(o => o.Client).Load();
                context.Entry(item).Reference(o => o.Permission).Load();
            }

            IList<Claim> UserPermissions =
                currectUser
                    .UserPermissions.Where(o => o.Client.ClientId == client)
                    .Select(p => new Claim(Constants.ClaimTypes.Role, p.Permission.Name))
                    .ToList();

            claimList.AddRange(UserPermissions);

            context.Dispose();
            context = null;

            ctx.IssuedClaims = claimList;
        }

        protected virtual async Task<IEnumerable<Claim>> GetClaimsFromAccount(TUser user)
        {
            ApplicationUser currentUser = user as ApplicationUser;

            List<Claim> claims = new List<Claim>
            {
                new Claim(Constants.ClaimTypes.Subject, user.Id.ToString()),
                new Claim(Constants.ClaimTypes.PreferredUserName, user.UserName),
                new Claim(Constants.ClaimTypes.Name, user.UserName),
            };

            if (userManager.SupportsUserEmail)
            {
                string email = await userManager.GetEmailAsync(user.Id);
                if (!string.IsNullOrWhiteSpace(email))
                {
                    claims.Add(new Claim(Constants.ClaimTypes.Email, email));
                    bool verified = await userManager.IsEmailConfirmedAsync(user.Id);
                    claims.Add(new Claim(Constants.ClaimTypes.EmailVerified, verified ? "true" : "false"));
                }
            }

            if (userManager.SupportsUserPhoneNumber)
            {
                string phone = await userManager.GetPhoneNumberAsync(user.Id);
                if (!string.IsNullOrWhiteSpace(phone))
                {
                    claims.Add(new Claim(Constants.ClaimTypes.PhoneNumber, phone));
                    bool verified = await userManager.IsPhoneNumberConfirmedAsync(user.Id);
                    claims.Add(new Claim(Constants.ClaimTypes.PhoneNumberVerified, verified ? "true" : "false"));
                }
            }

            if (userManager.SupportsUserClaim)
            {
                claims.AddRange(await userManager.GetClaimsAsync(user.Id));
            }

            //if (userManager.SupportsUserRole)
            //{
            //	var roleClaims =
            //		from role in await userManager.GetRolesAsync(user.Id)
            //		select new Claim(Constants.ClaimTypes.Role, role);
            //	claims.AddRange(roleClaims);
            //}

            return claims;
        }

        protected virtual async Task<string> GetDisplayNameForAccountAsync(TKey userID)
        {
            TUser user = await userManager.FindByIdAsync(userID);
            IEnumerable<Claim> claims = await GetClaimsFromAccount(user);

            Claim nameClaim = null;
            if (DisplayNameClaimType != null)
            {
                nameClaim = claims.FirstOrDefault(x => x.Type == DisplayNameClaimType);
            }
            if (nameClaim == null)
            {
                nameClaim = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);
            }

            if (nameClaim == null)
            {
                nameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            }

            if (nameClaim != null)
            {
                return nameClaim.Value;
            }

            return user.UserName;
        }

        protected virtual async Task<TUser> FindUserAsync(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        protected virtual Task<AuthenticateResult> PostAuthenticateLocalAsync(TUser user, SignInMessage message)
        {
            return Task.FromResult<AuthenticateResult>(null);
        }

        public static Dictionary<string, string> CaptchaStorage = new Dictionary<string, string>();
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext ctx)
        {
            #region Authenticate Captcha

            var requestContext = (System.Web.Routing.RequestContext)_owinEnv.Environment["System.Web.Routing.RequestContext"];
            var ss = HttpContext.Current.Session;

            var enteredCaptcha = "";
            enteredCaptcha = requestContext.HttpContext.Request.Form["CaptchaUserInput"];


            var requestId = requestContext.HttpContext.Request.Params["signin"];
            var serverCaptcha = CaptchaStorage.FirstOrDefault(a => a.Key == requestId).Value;
            if (requestId != null) CaptchaStorage.Remove(requestId);

            if (serverCaptcha != enteredCaptcha)
            {
                ctx.AuthenticateResult = new AuthenticateResult("کد امنیتی اشتباه است.");
                return;
            }

            #endregion

            string username = ctx.UserName;
            string password = ctx.Password;
            SignInMessage message = ctx.SignInMessage;

            ctx.AuthenticateResult = null;

            if (userManager.SupportsUserPassword)
            {
                TUser user = await FindUserAsync(username);
                if (user != null)
                {
                    if (userManager.SupportsUserLockout &&
                        await userManager.IsLockedOutAsync(user.Id))
                    {
                        return;
                    }

                    if (await userManager.CheckPasswordAsync(user, password))
                    {
                        if (userManager.SupportsUserLockout)
                        {
                            await userManager.ResetAccessFailedCountAsync(user.Id);
                        }

                        AuthenticateResult result = await PostAuthenticateLocalAsync(user, message);
                        if (result == null)
                        {
                            IEnumerable<Claim> claims = await GetClaimsForAuthenticateResult(user);
                            result = new AuthenticateResult(user.Id.ToString(), await GetDisplayNameForAccountAsync(user.Id), claims);
                        }

                        ctx.AuthenticateResult = result;
                    }
                    else if (userManager.SupportsUserLockout)
                    {
                        await userManager.AccessFailedAsync(user.Id);
                    }
                }
            }
        }

        protected virtual async Task<IEnumerable<Claim>> GetClaimsForAuthenticateResult(TUser user)
        {
            List<Claim> claims = new List<Claim>();
            if (EnableSecurityStamp && userManager.SupportsUserSecurityStamp)
            {
                string stamp = await userManager.GetSecurityStampAsync(user.Id);
                if (!string.IsNullOrWhiteSpace(stamp))
                {
                    claims.Add(new Claim("security_stamp", stamp));
                }
            }
            return claims;
        }

        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext ctx)
        {
            ExternalIdentity externalUser = ctx.ExternalIdentity;
            SignInMessage message = ctx.SignInMessage;

            if (externalUser == null)
            {
                throw new ArgumentNullException("externalUser");
            }

            TUser user = await userManager.FindAsync(new Microsoft.AspNet.Identity.UserLoginInfo(externalUser.Provider, externalUser.ProviderId));
            if (user == null)
            {
                ctx.AuthenticateResult = await ProcessNewExternalAccountAsync(externalUser.Provider, externalUser.ProviderId, externalUser.Claims);
            }
            else
            {
                ctx.AuthenticateResult = await ProcessExistingExternalAccountAsync(user.Id, externalUser.Provider, externalUser.ProviderId, externalUser.Claims);
            }
        }

        protected virtual async Task<AuthenticateResult> ProcessNewExternalAccountAsync(string provider, string providerId, IEnumerable<Claim> claims)
        {
            TUser user = await TryGetExistingUserFromExternalProviderClaimsAsync(provider, claims);
            if (user == null)
            {
                user = await InstantiateNewUserFromExternalProviderAsync(provider, providerId, claims);
                if (user == null)
                {
                    throw new InvalidOperationException("CreateNewAccountFromExternalProvider returned null");
                }

                IdentityResult createResult = await userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return new AuthenticateResult(createResult.Errors.First());
                }
            }

            UserLoginInfo externalLogin = new Microsoft.AspNet.Identity.UserLoginInfo(provider, providerId);
            IdentityResult addExternalResult = await userManager.AddLoginAsync(user.Id, externalLogin);
            if (!addExternalResult.Succeeded)
            {
                return new AuthenticateResult(addExternalResult.Errors.First());
            }

            AuthenticateResult result = await AccountCreatedFromExternalProviderAsync(user.Id, provider, providerId, claims);
            if (result != null)
            {
                return result;
            }

            return await SignInFromExternalProviderAsync(user.Id, provider);
        }

        protected virtual Task<TUser> InstantiateNewUserFromExternalProviderAsync(string provider, string providerId, IEnumerable<Claim> claims)
        {
            TUser user = new TUser() { UserName = Guid.NewGuid().ToString("N") };
            return Task.FromResult(user);
        }

        protected virtual Task<TUser> TryGetExistingUserFromExternalProviderClaimsAsync(string provider, IEnumerable<Claim> claims)
        {
            return Task.FromResult<TUser>(null);
        }

        protected virtual async Task<AuthenticateResult> AccountCreatedFromExternalProviderAsync(TKey userID, string provider, string providerId, IEnumerable<Claim> claims)
        {
            claims = await SetAccountEmailAsync(userID, claims);
            claims = await SetAccountPhoneAsync(userID, claims);

            return await UpdateAccountFromExternalClaimsAsync(userID, provider, providerId, claims);
        }

        protected virtual async Task<AuthenticateResult> SignInFromExternalProviderAsync(TKey userID, string provider)
        {
            TUser user = await userManager.FindByIdAsync(userID);
            IEnumerable<Claim> claims = await GetClaimsForAuthenticateResult(user);

            return new AuthenticateResult(
                userID.ToString(),
                await GetDisplayNameForAccountAsync(userID),
                claims,
                authenticationMethod: Constants.AuthenticationMethods.External,
                identityProvider: provider);
        }

        protected virtual async Task<AuthenticateResult> UpdateAccountFromExternalClaimsAsync(TKey userID, string provider, string providerId, IEnumerable<Claim> claims)
        {
            IList<Claim> existingClaims = await userManager.GetClaimsAsync(userID);
            IEnumerable<Claim> intersection = existingClaims.Intersect(claims, new ClaimComparer());
            IEnumerable<Claim> newClaims = claims.Except(intersection, new ClaimComparer());

            foreach (Claim claim in newClaims)
            {
                IdentityResult result = await userManager.AddClaimAsync(userID, claim);
                if (!result.Succeeded)
                {
                    return new AuthenticateResult(result.Errors.First());
                }
            }

            return null;
        }

        protected virtual async Task<AuthenticateResult> ProcessExistingExternalAccountAsync(TKey userID, string provider, string providerId, IEnumerable<Claim> claims)
        {
            return await SignInFromExternalProviderAsync(userID, provider);
        }

        protected virtual async Task<IEnumerable<Claim>> SetAccountEmailAsync(TKey userID, IEnumerable<Claim> claims)
        {
            Claim email = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Email);
            if (email != null)
            {
                string userEmail = await userManager.GetEmailAsync(userID);
                if (userEmail == null)
                {
                    // if this fails, then presumably the email is already associated with another account
                    // so ignore the error and let the claim pass thru
                    IdentityResult result = await userManager.SetEmailAsync(userID, email.Value);
                    if (result.Succeeded)
                    {
                        Claim email_verified = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.EmailVerified);
                        if (email_verified != null && email_verified.Value == "true")
                        {
                            string token = await userManager.GenerateEmailConfirmationTokenAsync(userID);
                            await userManager.ConfirmEmailAsync(userID, token);
                        }

                        string[] emailClaims = new string[] { Constants.ClaimTypes.Email, Constants.ClaimTypes.EmailVerified };
                        return claims.Where(x => !emailClaims.Contains(x.Type));
                    }
                }
            }

            return claims;
        }

        protected virtual async Task<IEnumerable<Claim>> SetAccountPhoneAsync(TKey userID, IEnumerable<Claim> claims)
        {
            Claim phone = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.PhoneNumber);
            if (phone != null)
            {
                string userPhone = await userManager.GetPhoneNumberAsync(userID);
                if (userPhone == null)
                {
                    // if this fails, then presumably the phone is already associated with another account
                    // so ignore the error and let the claim pass thru
                    IdentityResult result = await userManager.SetPhoneNumberAsync(userID, phone.Value);
                    if (result.Succeeded)
                    {
                        Claim phone_verified = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.PhoneNumberVerified);
                        if (phone_verified != null && phone_verified.Value == "true")
                        {
                            string token = await userManager.GenerateChangePhoneNumberTokenAsync(userID, phone.Value);
                            await userManager.ChangePhoneNumberAsync(userID, phone.Value, token);
                        }

                        string[] phoneClaims = new string[] { Constants.ClaimTypes.PhoneNumber, Constants.ClaimTypes.PhoneNumberVerified };
                        return claims.Where(x => !phoneClaims.Contains(x.Type));
                    }
                }
            }

            return claims;
        }

        public override async Task IsActiveAsync(IsActiveContext ctx)
        {
            ClaimsPrincipal subject = ctx.Subject;

            if (subject == null)
            {
                throw new ArgumentNullException("subject");
            }

            string id = subject.GetSubjectId();
            TKey key = ConvertSubjectToKey(id);
            TUser acct = await userManager.FindByIdAsync(key);

            ctx.IsActive = false;

            if (acct != null)
            {
                if (EnableSecurityStamp && userManager.SupportsUserSecurityStamp)
                {
                    string security_stamp = subject.Claims.Where(x => x.Type == "security_stamp").Select(x => x.Value).SingleOrDefault();
                    if (security_stamp != null)
                    {
                        string db_security_stamp = await userManager.GetSecurityStampAsync(key);
                        if (db_security_stamp != security_stamp)
                        {
                            return;
                        }
                    }
                }

                ctx.IsActive = true;
            }
        }
    }
}