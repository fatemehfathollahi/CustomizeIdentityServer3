using System;

namespace SecurityService.Infrastructure.Models
{
	public partial class ClientSecret
	{
		public int Id { get; set; }
		public string Value { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public Nullable<System.DateTimeOffset> Expiration { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}