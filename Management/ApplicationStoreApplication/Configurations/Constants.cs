namespace ApplicationStoreApplication.Configurations
{
	internal class Constants
	{
		public static string BaseAddress { get { return Properties.Settings.Default.BaseAddress; } }
		public static string TokenEndpoint { get { return BaseAddress + "/connect/token"; } }
		public static string UserInfoEndpoint { get { return BaseAddress + "/connect/userinfo"; } }
		public static string AuthorizeEndpoint { get { return BaseAddress + "/connect/authorize"; } }
		public static string LogoutEndpoint { get { return BaseAddress + "/connect/endsession"; } }
		public static string IdentityTokenValidationEndpoint { get { return BaseAddress + "/connect/identitytokenvalidation"; } }
		public static string TokenRevocationEndpoint { get { return BaseAddress + "/connect/revocation"; } }
	}
}