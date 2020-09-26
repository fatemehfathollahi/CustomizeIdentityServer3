using Framework.Core.Contracts.Model;
using System;

namespace Framework.Core.Contracts.Audit.Model
{
	public class AuditEvent : IEntityObject
	{
		public int Id { get; set; }
		public Nullable<DateTime> InsertedDate { get; set; }
		public Nullable<DateTime> LastUpdatedDate { get; set; }
		public string Data { get; set; }
		public string Year { get; set; }
	}
}