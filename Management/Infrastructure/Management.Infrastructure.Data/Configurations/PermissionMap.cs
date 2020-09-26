using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class PermissionMap : EntityTypeConfiguration<Permission>
	{
		public PermissionMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(256);

			// Table & Column Mappings
			this.ToTable("Permissions");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");

			// Relationships
			this.HasMany(t => t.Roles)
				.WithMany(t => t.Permissions)
				.Map(m =>
					{
						m.ToTable("RolePermissions");
						m.MapLeftKey("RoleId");
						m.MapRightKey("PermissionId");
					});

			this.HasMany(t => t.Users)
				.WithMany(t => t.Permissions)
				.Map(m =>
					{
						m.ToTable("UserPermissions");
						m.MapLeftKey("PermissionId");
						m.MapRightKey("UserId");
					});
		}
	}
}