using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ScopeClaimMap : EntityTypeConfiguration<ScopeClaim>
	{
		public ScopeClaimMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.Description)
				.HasMaxLength(1000);

			// Table & Column Mappings
			this.ToTable("ScopeClaims");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.AlwaysIncludeInIdToken).HasColumnName("AlwaysIncludeInIdToken");
			this.Property(t => t.Scope_Id).HasColumnName("Scope_Id");

			// Relationships
			this.HasRequired(t => t.Scope)
				.WithMany(t => t.ScopeClaims)
				.HasForeignKey(d => d.Scope_Id);
		}
	}
}