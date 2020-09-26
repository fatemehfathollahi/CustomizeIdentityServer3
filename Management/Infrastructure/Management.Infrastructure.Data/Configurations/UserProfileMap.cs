using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class UserProfileMap : EntityTypeConfiguration<UserProfile>
	{
		public UserProfileMap()
		{
			// Primary Key
			this.HasKey(t => t.UserID);

			// Properties
			// Table & Column Mappings
			this.ToTable("UserProfiles");
			this.Property(t => t.UserID).HasColumnName("UserID");
			this.Property(t => t.FirstName).HasColumnName("FirstName");
			this.Property(t => t.LastName).HasColumnName("LastName");
			this.Property(t => t.Email).HasColumnName("Email");
			this.Property(t => t.Mobile).HasColumnName("Mobile");
			this.Property(t => t.InternalPhone).HasColumnName("InternalPhone");
			this.Property(t => t.InternalPhoneCode).HasColumnName("InternalPhoneCode");
			this.Property(t => t.EmployeeNumber).HasColumnName("EmployeeNumber");
			this.Property(t => t.Birthdate).HasColumnName("Birthdate");
			this.Property(t => t.ProfileImageUrl).HasColumnName("ProfileImageUrl");
			this.Property(t => t.NationalCode).HasColumnName("NationalCode");
			this.Property(t => t.PersonCode).HasColumnName("PersonCode");

			// Relationships
			//this.HasOptional(t => t.User)
			//	.WithRequired(o => o.Id);
		}
	}
}