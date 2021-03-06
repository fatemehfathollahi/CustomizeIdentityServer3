using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class UserClaimMap : EntityTypeConfiguration<UserClaim>
	{
		public UserClaimMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
			this.ToTable("UserClaims");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.UserId).HasColumnName("UserId");
			this.Property(t => t.ClaimType).HasColumnName("ClaimType");
			this.Property(t => t.ClaimValue).HasColumnName("ClaimValue");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.UserClaims)
				.HasForeignKey(d => d.UserId);
		}
	}
}