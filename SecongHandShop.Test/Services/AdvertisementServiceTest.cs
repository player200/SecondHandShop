namespace SecondHandShop.Test.Services
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Implementations;
    using SecondHandShop.Web.Data;
    using SecondHandShop.Web.Infrastructures.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdvertisementServiceTest
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
                        Id=1,
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
                        Id=1,
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
                        Id=3,
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
        public async Task CreatorUserNameShouldReturnUserNameOfUserThatCreatedThatAdvertisement()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);
            var advertisementId = 1;

            // Act
            var result = await advertisementsService.CreatorUserName(advertisementId);

            // Assert
            result
                .Should()
                .Be("pesho");
        }

        [Fact]
        public async Task ExistsShouldReturnBooleanDependsOfAdvertisment()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);
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

            var advertisementsService = new AdvertisementService(db);
            var advertisementId = 1;

            // Act
            await advertisementsService.Delete(advertisementId);

            // Assert
            db
                .Advertisements
                .Where(a => a.Id == advertisementId)
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

            var advertisementsService = new AdvertisementService(db);
            var advertisementId = 1;
            var newName = "newnamenewnamenewname";
            var newDescription = "newnamenewnamenewname";
            var newPrice = 350m;
            var newCategoryId = 1;

            // Act
            await advertisementsService.Edit(
                advertisementId,
                newName,
                newDescription,
                newPrice,
                newCategoryId);

            // Assert
            db
                .Advertisements
                .Where(a => a.Id == advertisementId)
                .Select(a => a.Name)
                .FirstOrDefault()
                .Should()
                .Be(newName);

            db
               .Advertisements
               .Where(a => a.Id == advertisementId)
               .Select(a => a.Description)
               .FirstOrDefault()
               .Should()
               .Be(newDescription);
            db
               .Advertisements
               .Where(a => a.Id == advertisementId)
               .Select(a => a.Price)
               .FirstOrDefault()
               .Should()
               .Be(newPrice);
            db
               .Advertisements
               .Where(a => a.Id == advertisementId)
               .Select(a => a.CategoryId)
               .FirstOrDefault()
               .Should()
               .Be(newCategoryId);
        }

        [Fact]
        public async Task ByIdShouldReturnItemWithThatId()
        {
            // Arrange
            //Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);
            var advertisementId = 1;

            // Act
            var result = await advertisementsService.ById(advertisementId);

            // Assert
            result
                .Name
                .Should()
                .Be("very good one");

            result
                .Description
                .Should()
                .Be("need to be in that site");

            result
                .Price
                .Should()
                .Be(300);
        }

        [Fact]
        public void GetDetailsShouldReturnCorrectDataFromDb()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);
            var advertisementId = 1;

            // Act
            var result = advertisementsService.GetDetails(advertisementId);

            // Assert
            result
                .Id
                .Should()
                .Be(advertisementId);

            result
                .Name
                .Should()
                .Be("very good one");

            result
                .Description
                .Should()
                .Be("need to be in that site");

            result
                .Price
                .Should()
                .Be(300);
        }

        [Fact]
        public async Task CreateShouldReturnAddNewAdvertisementInDb()
        {
            // Arrange
            var db = this.Context;

            var advertisementsService = new AdvertisementService(db);
            var newName = "sttttstststtststs";
            var newDescrtiption = "sttttstststtststss";
            var newPrice = 320m;
            var newPublishedDate = DateTime.ParseExact("01/01/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var newCategoryId = 4;
            var newUserId = "geagaegaeagae";


            // Act
            await advertisementsService.Create(
                                            newName,
                                            newDescrtiption,
                                            newPrice,
                                            newPublishedDate,
                                            newCategoryId,
                                            newUserId);

            // Assert
            db
                .Advertisements
                .Where(a => a.Id == 1)
                .FirstOrDefault()
                .Should()
                .NotBe(null);

            db
                .Advertisements
                .Where(a => a.Id == 1)
                .Select(a => a.Name)
                .FirstOrDefault()
                .Should()
                .Be(newName);

            db
                .Advertisements
                .Where(a => a.Id == 1)
                .Select(a => a.Description)
                .FirstOrDefault()
                .Should()
                .Be(newDescrtiption);
        }

        [Fact]
        public async Task AllAsyncShouldReturnAllItemsWithTheirPictures()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);

            // Act
            var result = await advertisementsService.AllAsync();

            // Assert
            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(0)
                .Should()
                .Be(3);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(1)
                .Should()
                .Be(2);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(2)
                .Should()
                .Be(1);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .Should()
                .HaveCount(3);

            result
               .Pictures
               .ElementAt(0)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .ElementAt(1)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .ElementAt(2)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .Should()
               .HaveCount(3);
        }

        [Fact]
        public async Task AllAsyncWithUserNameShouldReturnAllItemsWithTheirPictures()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);
            var userName = "pesho";

            // Act
            var result = await advertisementsService.AllAsync(userName);

            // Assert
            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(0)
                .Should()
                .Be(3);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(1)
                .Should()
                .Be(1);
            
            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .Should()
                .HaveCount(2);

            result
               .Pictures
               .ElementAt(0)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .ElementAt(1)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);
            
            result
               .Pictures
               .Should()
               .HaveCount(2);
        }

        [Fact]
        public async Task AllAsyncWithCategoryIdShouldReturnAllItemsWithTheirPictures()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);
            var categoryId = 1;

            // Act
            var result = await advertisementsService.AllAsync(categoryId);

            // Assert
            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(0)
                .Should()
                .Be(2);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(1)
                .Should()
                .Be(1);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .Should()
                .HaveCount(2);

            result
               .Pictures
               .ElementAt(0)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .ElementAt(1)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .Should()
               .HaveCount(2);
        }

        [Fact]
        public async Task GetSixAsyncShouldReturnAllItemsWithTheirPictures()
        {
            // Arrange
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var advertisementsService = new AdvertisementService(db);

            // Act
            var result = await advertisementsService.GetSixAsync();

            // Assert
            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(0)
                .Should()
                .Be(3);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(1)
                .Should()
                .Be(2);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .ElementAt(2)
                .Should()
                .Be(1);

            result
                .Advertisements
                .Select(a => a.Id)
                .ToList()
                .Should()
                .HaveCount(3);

            result
               .Pictures
               .ElementAt(0)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .ElementAt(1)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .ElementAt(2)
               .Should()
               .Be(DataConstants.ImgDefoutNotFound);

            result
               .Pictures
               .Should()
               .HaveCount(3);
        }
    }
}