namespace SecondHandShop.Test.Services.Moderator
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Moderator.Implementations;
    using SecondHandShop.Web.Data;
    using SecondHandShop.Web.Infrastructures.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ModAdvertisementServiceTest
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

        private IEnumerable<Advertisement> GetTestData()
        {
            return new List<Advertisement>()
            {
                new Advertisement()
                {
                    Id=1,
                    Price=300,
                    Name="very good one",
                    Description="need to be in that site",
                    PublishDate=DateTime.ParseExact("01/01/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
                    CategoryId=1,
                    UserId="geagaegaegaegea",
                    User=new User
                    {
                        UserName="pesho"
                    },
                    Category=new Category
                    {
                        Name="very good category"
                    }
                },
                new Advertisement()
                {
                    Id=2,
                    Price=300,
                    Name="very good one2",
                    Description="need to be in that site2",
                    PublishDate=DateTime.ParseExact("01/01/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
                    CategoryId=1,
                    UserId="geagaegaegaegea2",
                    User=new User
                    {
                        UserName="pesho2"
                    },
                    Category=new Category
                    {
                        Name="very good category2"
                    }
                },
                new Advertisement()
                {
                    Id=3,
                    Price=300,
                    Name="very good one3",
                    Description="need to be in that site3",
                    PublishDate=DateTime.ParseExact("01/01/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
                    CategoryId=3,
                    UserId="geagaegaegaegea",
                    User=new User
                    {
                        UserName="pesho"
                    },
                    Category=new Category
                    {
                        Name="very good category3"
                    }
                }
            };
        }

        private void PopulateData(SecondHandShopDbContext db)
        {
            db.Advertisements.AddRange(this.GetTestData());
            db.SaveChanges();
        }

        [Fact]
        public async Task ExistsShouldReturnBooleanWhenThisItemExists()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new ModeratorAdvertisementService(db);
            var advertisementId = 1;

            // Act
            var result = await advertisementsService.Exists(advertisementId);

            // Assert
            result
                .Should()
                .Be(true);
        }

        [Fact]
        public async Task DeleteShouldReturnDbWithoutThatItem()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new ModeratorAdvertisementService(db);
            var advertisementId = 1;

            // Act
            await advertisementsService.Delete(advertisementId);

            // Assert
            db
                .Advertisements
                .Where(a=>a.Id==advertisementId)
                .FirstOrDefault()
                .Should()
                .Be(null);

            db
                .Advertisements
                .Count()
                .Should()
                .Be(2);
        }

        [Fact]
        public async Task EditShouldReturnDbWithEditedItem()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new ModeratorAdvertisementService(db);
            var advertisementId = 1;
            var newCategoryId = 2;

            // Act
            await advertisementsService.Edit(
                advertisementId,
                newCategoryId);

            // Assert
            db
                .Advertisements
                .Where(a => a.Id == advertisementId)
                .FirstOrDefault()
                .Should()
                .NotBe(null);
            

            db
               .Advertisements
               .Where(a => a.Id == advertisementId)
               .Select(a => a.CategoryId)
               .FirstOrDefault()
               .Should()
               .Be(newCategoryId);
        }

        [Fact]
        public async Task ByIdShouldReturnCorrectItemFromDb()
        {
            // Arrange
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new ModeratorAdvertisementService(db);
            var advertisementId = 1;

            // Act
            var result= await advertisementsService.ById(advertisementId);

            // Assert
            result
                .Id
                .Should()
                .Be(advertisementId);

            result
                .Name
                .Should()
                .Be("very good one");
        }
    }
}