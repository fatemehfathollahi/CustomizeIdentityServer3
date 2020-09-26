using SecurityService.Infrastructure.Models;
using System.Data.Entity.ModelConfiguration;

namespace SecurityService.Infrastructure.Data.Configuration
{
	public class ClientRedirectUriMap : EntityTypeConfiguration<ClientRedirectUri>
	{
		public ClientRedirectUriMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Uri)
				.IsRequired()
				.HasMaxLength(2000);

			// Table & Column Mappings
			this.ToTable("ClientRedirectUris");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Uri).HasColumnName("Uri");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientRedirectUris)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}