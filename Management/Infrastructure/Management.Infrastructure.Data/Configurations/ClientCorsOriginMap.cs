using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ClientCorsOriginMap : EntityTypeConfiguration<ClientCorsOrigin>
	{
		public ClientCorsOriginMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Origin)
				.IsRequired()
				.HasMaxLength(150);

			// Table & Column Mappings
			this.ToTable("ClientCorsOrigins");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Origin).HasColumnName("Origin");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientCorsOrigins)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}