using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ClientSecretMap : EntityTypeConfiguration<ClientSecret>
	{
		public ClientSecretMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Value)
				.IsRequired()
				.HasMaxLength(250);

			this.Property(t => t.Type)
				.HasMaxLength(250);

			this.Property(t => t.Description)
				.HasMaxLength(2000);

			// Table & Column Mappings
			this.ToTable("ClientSecrets");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Value).HasColumnName("Value");
			this.Property(t => t.Type).HasColumnName("Type");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.Expiration).HasColumnName("Expiration");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientSecrets)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}