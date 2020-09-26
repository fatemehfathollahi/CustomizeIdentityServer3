using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class ScopeMap : EntityTypeConfiguration<Scope>
	{
		public ScopeMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.DisplayName)
				.HasMaxLength(200);

			this.Property(t => t.Description)
				.HasMaxLength(1000);

			this.Property(t => t.ClaimsRule)
				.HasMaxLength(200);

			// Table & Column Mappings
			this.ToTable("Scopes");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Enabled).HasColumnName("Enabled");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.DisplayName).HasColumnName("DisplayName");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.Required).HasColumnName("Required");
			this.Property(t => t.Emphasize).HasColumnName("Emphasize");
			this.Property(t => t.Type).HasColumnName("Type");
			this.Property(t => t.IncludeAllClaimsForUser).HasColumnName("IncludeAllClaimsForUser");
			this.Property(t => t.ClaimsRule).HasColumnName("ClaimsRule");
			this.Property(t => t.ShowInDiscoveryDocument).HasColumnName("ShowInDiscoveryDocument");
			this.Property(t => t.AllowUnrestrictedIntrospection).HasColumnName("AllowUnrestrictedIntrospection");
		}
	}
}