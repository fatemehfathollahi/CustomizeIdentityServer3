using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.AspNet.Identity;
using SecurityService.Infrastructure.Data;
using SecurityService.Infrastructure.IdentityManager;
using SecurityService.Infrastructure.Models;
using System.Linq;

namespace SecurityService.SSO.IdentityService
{
	public static class IdentityUserServiceExtensions
	{
		public static void ConfigureIdentityUserService(this IdentityServerServiceFactory factory, string connString)
		{
			IdentityContext context = new IdentityContext(connString);

			if (!context.Users.Any())
			{
				context.Users.Add(new ApplicationUser { UserName = "Admin", PasswordHash = new PasswordHasher().HashPassword("123"), UserProfile = new ApplicationUserProfile() });
				context.Roles.Add(new ApplicationPermission { Name = "ADMAdmin" });
				//context.Roles.Add(new ApplicationPermission { Name = "Admin" });

				context.SaveChanges();

				ApplicationUser AdminUser = context.Users.FirstOrDefault(o => o.UserName == "Admin");
				//var AdminRole = context.Roles.FirstOrDefault(o => o.Name == "Admin");
				ApplicationPermission ADMAdminRole = context.Roles.FirstOrDefault(o => o.Name == "ADMAdmin");

				Client ADMClient = context.Clients.FirstOrDefault(o => o.ClientId.ToUpper() == "ADM");
				context.UserClientPermissions.Add(new UserClientPermissions { Client = ADMClient, Permission = ADMAdminRole, User = AdminUser });
				ADMClient.Permissions.Add(ADMAdminRole);
				context.SaveChanges();

				//var RIMSClient = context.Clients.FirstOrDefault(o => o.ClientId.ToUpper() == "RIMS");
				//context.UserClientPermissions.Add(new UserClientPermissions { Client = RIMSClient, Permission = AdminRole, User = AdminUser });
				//RIMSClient.Permissions.Add(AdminRole);
				//context.SaveChanges();

				System.Collections.Generic.List<UserClientPermissions> AdminUserPermissions = context.UserClientPermissions.Where(o => o.User.UserName == AdminUser.UserName).ToList();

				AdminUser.Roles.Add(new ApplicationUserPermission { RoleId = ADMAdminRole.Id, UserId = AdminUser.Id });

				foreach (UserClientPermissions item in AdminUserPermissions)
				{
					ADMClient.UserPermissions.Add(item);
				}

				//foreach (var item in AdminUserPermissions)
				//	AdminUser.UserPermissions.Add(item);

				//foreach (var item in AdminUserPermissions)
				//	AdminRole.UserPermissions.Add(item);

				context.SaveChanges();
			}

			factory.UserService = new Registration<IUserService, IdentityUserService>();
			factory.Register(new Registration<IdentityUserManager>());
			factory.Register(new Registration<IdentityUserStore>());
			factory.Register(new Registration<IdentityContext>(resolver => new IdentityContext(connString)));
		}
	}
}