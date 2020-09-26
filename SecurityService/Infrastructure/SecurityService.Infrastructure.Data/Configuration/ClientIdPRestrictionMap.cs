using SecurityService.Infrastructure.Models;
using System.Data.Entity.ModelConfiguration;

namespace SecurityService.Infrastructure.Data.Configuration
{
	public class ClientIdPRestrictionMap : EntityTypeConfiguration<ClientIdPRestriction>
	{
		public ClientIdPRestrictionMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Provider)
				.IsRequired()
				.HasMaxLength(200);

			// Table & Column Mappings
			this.ToTable("ClientIdPRestrictions");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Provider).HasColumnName("Provider");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientIdPRestrictions)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}