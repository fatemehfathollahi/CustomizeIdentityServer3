using System.Data.Entity.ModelConfiguration;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Data.Configurations
{
	public class RoleMap : EntityTypeConfiguration<Role>
	{
		public RoleMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
			this.ToTable("Roles");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Description).HasColumnName("Description");
		}
	}
}