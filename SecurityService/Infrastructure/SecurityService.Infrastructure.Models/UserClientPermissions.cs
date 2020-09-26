namespace SecurityService.Infrastructure.Models
{
	public class UserClientPermissions
	{
		public int Id { get; set; }
		public Client Client { get; set; }
		public ApplicationUser User { get; set; }
		public ApplicationPermission Permission { get; set; }
	}
}