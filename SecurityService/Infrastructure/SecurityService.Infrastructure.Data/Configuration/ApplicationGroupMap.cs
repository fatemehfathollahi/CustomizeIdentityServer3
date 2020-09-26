using SecurityService.Infrastructure.Models;
using System.Data.Entity.ModelConfiguration;

namespace SecurityService.Infrastructure.Data.Configuration
{
	public class ApplicationGroupMap : EntityTypeConfiguration<ApplicationRoles>
	{
		public ApplicationGroupMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Id)
				.IsRequired();

			// Table & Column Mappings
			this.ToTable("Roles");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Description).HasColumnName("Description");

			this.HasMany(t => t.Permissions)
				.WithMany(t => t.Groups)
				.Map(m =>
				{
					m.ToTable("RolePermissions");
					m.MapLeftKey("PermissionId");
					m.MapRightKey("RoleId");
				});

			// Relationships
			this.HasMany(t => t.Users)
				.WithMany(t => t.Groups)
				.Map(m =>
				{
					m.ToTable("UserRoles");
					m.MapLeftKey("UserId");
					m.MapRightKey("RoleId");
				});
		}
	}
}