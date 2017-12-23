namespace SecondHandShop.Test.Services
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Implementations;
    using SecondHandShop.Web.Data;
    using SecondHandShop.Web.Infrastructures.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class PictureServiceTest
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

        private IEnumerable<Picture> GetTestData()
        {
            return new List<Picture>()
            {
                new Picture()
                {
                    Id =1,
                    UrlPath="htt://somesight.jpg",
                    IsPrime=true,
                    Advertisement=new Advertisement
                    {
                        Id=1,
                        User=new User
                        {
                            UserName="pesho"
                        }
                    },
                    AdvertisementId =1
                },
                new Picture()
                {
                    Id =2,
                    UrlPath="htt://somesight.jpg",
                    IsPrime=false,
                    AdvertisementId=1
                },
                new Picture()
                {
                    Id =3,
                    UrlPath="htt://somesight.jpg",
                    IsPrime=false,
                    AdvertisementId=1
                }
            };
        }

        private void PopulateData(SecondHandShopDbContext db)
        {
            db.Pictures.AddRange(this.GetTestData());
            db.SaveChanges();
        }

        [Fact]
        public async Task CountShouldReturnCorrectCountOfPicturesByAdvertisementId()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var picturesService = new PictureService(db);
            var advertisementId = 1;

            // Act
            var result = await picturesService.Count(advertisementId);

            // Assert
            result
                .Should()
                .Be(3);
        }

        [Fact]
        public async Task EditPicuresShoudBeSuccessful()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var picturesService = new PictureService(db);
            var pictureId = 1;
            var newUrlPath = "htp://somepathsuccess.jpg";

            // Act
            await picturesService.Edit(pictureId, newUrlPath);

            // Assert
            db
                .Pictures
                .Where(p => p.Id == pictureId)
                .Select(p => p.Id)
                .FirstOrDefault()
                .Should()
                .Be(pictureId);

            db
               .Pictures
               .Where(p => p.Id == pictureId)
               .Select(p => p.UrlPath)
               .FirstOrDefault()
               .Should()
               .Be(newUrlPath);
        }

        [Fact]
        public async Task PicturesIdsShouldReturnPictureWhereIdIsTheSame()
        {
            // Arrange
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var picturesService = new PictureService(db);
            var pictureId = 1;

            // Act
            var result = await picturesService.PicturesIds(pictureId);

            // Assert
            result
                .Id
                .Should()
                .Be(pictureId);
        }

        [Fact]
        public async Task ExistsShouldReturnBoolean()
        {
            //Arrange
            var db = this.Context;
            this.PopulateData(db);

            var picturesService = new PictureService(db);
            var pictureId = 1;

            // Act
            var result = await picturesService.Exists(pictureId);

            // Assert
            result
                .Should()
                .Be(true);
        }

        [Fact]
        public async Task CreatorUserNameShouldReturnNameOfUserThatCreatedThatPicture()
        {
            //Arrange
            var db = this.Context;
            this.PopulateData(db);

            var picturesService = new PictureService(db);
            var pictureId = 1;

            // Act
            var result = await picturesService.CreatorUserName(pictureId);

            // Assert
            result
                .Should()
                .Be("pesho");
        }

        [Fact]
        public async Task CreateShouldReturnDbWithThatItem()
        {
            //Arrange
            var db = this.Context;

            var picturesService = new PictureService(db);
            var urlPathFirst = "htp://dwadwd.jpg";
            var urlPathSecond = "htp://dwadwd2.jpg";
            var urlPathThird = "htp://dwadwd3.jpg";
            var isPrimeFirst = true;
            var isPrimeSecond = false;
            var isPrimeThird = false;
            var advertisementId = 1;

            // Act
            await picturesService.Create(
                urlPathFirst,
                isPrimeFirst,
                urlPathSecond,
                isPrimeSecond,
                urlPathThird,
                isPrimeThird,
                advertisementId);

            // Assert
            db
                .Pictures
                .Where(m => m.UrlPath == urlPathFirst)
                .FirstOrDefault()
                .Should()
                .NotBe(null);

            db
                .Pictures
                .Where(m => m.UrlPath == urlPathSecond)
                .FirstOrDefault()
                .Should()
                .NotBe(null);

            db
                .Pictures
                .Where(m => m.UrlPath == urlPathThird)
                .FirstOrDefault()
                .Should()
                .NotBe(null);
        }
    }
}