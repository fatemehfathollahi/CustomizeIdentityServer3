using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ClientPostLogoutRedirectUriMap : EntityTypeConfiguration<ClientPostLogoutRedirectUri>
	{
		public ClientPostLogoutRedirectUriMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Uri)
				.IsRequired()
				.HasMaxLength(2000);

			// Table & Column Mappings
			this.ToTable("ClientPostLogoutRedirectUris");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Uri).HasColumnName("Uri");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientPostLogoutRedirectUris)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}