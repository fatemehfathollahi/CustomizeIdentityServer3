using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ClientClaimMap : EntityTypeConfiguration<ClientClaim>
	{
		public ClientClaimMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Type)
				.IsRequired()
				.HasMaxLength(250);

			this.Property(t => t.Value)
				.IsRequired()
				.HasMaxLength(250);

			// Table & Column Mappings
			this.ToTable("ClientClaims");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Type).HasColumnName("Type");
			this.Property(t => t.Value).HasColumnName("Value");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientClaims)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}