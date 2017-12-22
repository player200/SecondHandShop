namespace SecondHandShop.Test.Services.Admin
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Admin.Implementations;
    using SecondHandShop.Web.Data;
    using SecondHandShop.Web.Infrastructures.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdminUserServiceTest
    {
        private SecondHandShopDbContext Context
        {
            get
            {
                var dbOptions = new DbContextOptionsBuilder<SecondHandShopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new SecondHandShopDbContext(dbOptions);
            }
        }

        private IEnumerable<User> GetTestData()
        {
            return new List<User>()
            {
                new User()
                {
                    Id="eggegegegegeadmin",
                    UserName="Admin",
                    Email="admin@admin.com"
                },
                new User()
                {
                    Id="eggegegegegepesho",
                    UserName="pesho",
                    Email="pesho@pesho.com"
                },
                new User()
                {
                    Id="eggegegegegepesho2",
                    UserName="pesho2",
                    Email="pesho@pesho2.com"
                },
                new User()
                {
                    Id="eggegegegegepesho3",
                    UserName="pesho3",
                    Email="pesho@pesho3.com"
                }
            };
        }

        private void PopulateData(SecondHandShopDbContext db)
        {
            db.Users.AddRange(this.GetTestData());
            db.SaveChanges();
        }

        [Fact]
        public async Task FindNameShouldReturnNameOfTheCategory()
        {
            // Arrange
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var categoriesService = new AdminUserService(db);
            var adminId = "eggegegegegeadmin";

            // Act
            var result = await categoriesService.AllAsync(adminId);

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == "eggegegegegepesho"
                            && r.ElementAt(1).Id == "eggegegegegepesho2"
                            && r.ElementAt(2).Id == "eggegegegegepesho3");
        }
    }
}