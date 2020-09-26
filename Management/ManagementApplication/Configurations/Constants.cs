namespace ManagementApplication.Configurations
{
	internal class Constants
	{
		public static string BaseAddress => Properties.Settings.Default.BaseAddress;
		public static string TokenEndpoint => BaseAddress + "/connect/token";
		public static string UserInfoEndpoint => BaseAddress + "/connect/userinfo";
		public static string AuthorizeEndpoint => BaseAddress + "/connect/authorize";
		public static string LogoutEndpoint => BaseAddress + "/connect/endsession";
		public static string IdentityTokenValidationEndpoint => BaseAddress + "/connect/identitytokenvalidation";
		public static string TokenRevocationEndpoint => BaseAddress + "/connect/revocation";
	}
}