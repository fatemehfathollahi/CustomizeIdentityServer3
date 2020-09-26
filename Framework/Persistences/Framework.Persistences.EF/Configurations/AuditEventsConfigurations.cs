using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Framework.Persistences.EF.Configurations
{
	internal class AuditEventsConfigurations : EntityTypeConfiguration<Core.Contracts.Audit.Model.AuditEvent>
	{
		public AuditEventsConfigurations()
		{
			// Primary Key
			HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
			ToTable("Events");
			Property(t => t.Id)
				.HasColumnName("EventId")
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			Property(t => t.InsertedDate)
				.HasColumnName("InsertedDate");

			Property(t => t.LastUpdatedDate)
				.HasColumnName("LastUpdatedDate");

			Property(t => t.Data)
				.HasColumnName("Data")
				.IsUnicode()
				.IsMaxLength();

			Property(t => t.Year)
				.HasColumnName("Year")
				.HasMaxLength(5);
		}
	}
}