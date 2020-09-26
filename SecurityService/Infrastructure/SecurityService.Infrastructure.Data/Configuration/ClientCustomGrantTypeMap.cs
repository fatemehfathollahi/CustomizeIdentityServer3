using SecurityService.Infrastructure.Models;
using System.Data.Entity.ModelConfiguration;

namespace SecurityService.Infrastructure.Data.Configuration
{
	public class ClientCustomGrantTypeMap : EntityTypeConfiguration<ClientCustomGrantType>
	{
		public ClientCustomGrantTypeMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.GrantType)
				.IsRequired()
				.HasMaxLength(250);

			// Table & Column Mappings
			this.ToTable("ClientCustomGrantTypes");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.GrantType).HasColumnName("GrantType");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientCustomGrantTypes)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}