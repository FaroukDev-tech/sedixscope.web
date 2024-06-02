using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace sedixscope.web.Data
{
    public class SedixAuthDbContext : IdentityDbContext
    {
        public SedixAuthDbContext(DbContextOptions<SedixAuthDbContext> options) : base(options)
        {
        }

        // Seeding Data
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding Roles
            var adminRoleId = "5e4b3fcc-fc0d-46c3-8387-c5d9ed0aa6ca";
            var superAdminRoleId = "863760b1-b293-49f5-bab6-537980c9b2f4";
            var userRoleId = "e05da575-912f-4f77-b0dc-d4e76574950f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },

                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            // Creating Roles table and adding seeded data
            builder.Entity<IdentityRole>().HasData(roles);

            // Seeding superadminuser to have superadmin control
            var superAdminUserId = "cee99467-8934-4132-bf09-d2f1be3945fb";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin",
                NormalizedUserName = "SUPERADMIN",
                Email = "sadikalhanssah@gmail.com",
                NormalizedEmail = "sadikalhanssah@gmail.com".ToUpper(),
                Id = superAdminUserId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                                            .HashPassword(superAdminUser, "superadmin@1@2@3");

            // Creating superadmin user and User table
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Adding all roles to superadmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminUserId
                },

                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminUserId
                },

                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminUserId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}