using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ScopeSecretMap : EntityTypeConfiguration<ScopeSecret>
	{
		public ScopeSecretMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Description)
				.HasMaxLength(1000);

			this.Property(t => t.Type)
				.HasMaxLength(250);

			this.Property(t => t.Value)
				.IsRequired()
				.HasMaxLength(250);

			// Table & Column Mappings
			this.ToTable("ScopeSecrets");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.Expiration).HasColumnName("Expiration");
			this.Property(t => t.Type).HasColumnName("Type");
			this.Property(t => t.Value).HasColumnName("Value");
			this.Property(t => t.Scope_Id).HasColumnName("Scope_Id");

			// Relationships
			this.HasRequired(t => t.Scope)
				.WithMany(t => t.ScopeSecrets)
				.HasForeignKey(d => d.Scope_Id);
		}
	}
}