using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReactRoastDotnet.Data.Roles;

namespace ReactRoastDotnet.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<CustomRole>
{
    public void Configure(EntityTypeBuilder<CustomRole> builder)
    {
        builder.HasData(
            new CustomRole
            {
                Id = AdministratorRole.Id,
                Name = AdministratorRole.Name,
                NormalizedName = AdministratorRole.NormalizedName,
            },
            new CustomRole
            {
                Id = UserRole.Id,
                Name = UserRole.Name,
                NormalizedName = UserRole.NormalizedName,
            },
            new CustomRole
            {
                Id = DemoUserRole.Id,
                Name = DemoUserRole.Name,
                NormalizedName = DemoUserRole.NormalizedName
            }
        );
    }
}