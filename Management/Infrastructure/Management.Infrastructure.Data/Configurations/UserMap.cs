using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Email)
				.HasMaxLength(256);

			this.Property(t => t.UserName)
				.IsRequired()
				.HasMaxLength(256);

			// Table & Column Mappings
			this.ToTable("Users");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Email).HasColumnName("Email");
			this.Property(t => t.EmailConfirmed).HasColumnName("EmailConfirmed");
			this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
			this.Property(t => t.SecurityStamp).HasColumnName("SecurityStamp");
			this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
			this.Property(t => t.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
			this.Property(t => t.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
			this.Property(t => t.LockoutEndDateUtc).HasColumnName("LockoutEndDateUtc");
			this.Property(t => t.LockoutEnabled).HasColumnName("LockoutEnabled");
			this.Property(t => t.AccessFailedCount).HasColumnName("AccessFailedCount");
			this.Property(t => t.UserName).HasColumnName("UserName");

			// Relationships
			this.HasMany(t => t.Roles)
				.WithMany(t => t.Users)
				.Map(m =>
					{
						m.ToTable("UserRoles");
						m.MapLeftKey("RoleId");
						m.MapRightKey("UserId");
					});

			this.HasRequired(iu => iu.UserProfile)
				.WithRequiredPrincipal(iup => iup.User)
				.WillCascadeOnDelete();
		}
	}
}