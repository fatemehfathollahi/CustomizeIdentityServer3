using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ClientScopeMap : EntityTypeConfiguration<ClientScope>
	{
		public ClientScopeMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Scope)
				.IsRequired()
				.HasMaxLength(200);

			// Table & Column Mappings
			this.ToTable("ClientScopes");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Scope).HasColumnName("Scope");
			this.Property(t => t.Client_Id).HasColumnName("Client_Id");

			// Relationships
			this.HasRequired(t => t.Client)
				.WithMany(t => t.ClientScopes)
				.HasForeignKey(d => d.Client_Id);
		}
	}
}