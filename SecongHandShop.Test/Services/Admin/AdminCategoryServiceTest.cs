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

    public class AdminCategoryServiceTest
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

        private IEnumerable<Category> GetTestData()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id=1,
                    Name="firstOne"
                },
                new Category()
                {
                    Id=2,
                    Name="secondOne"
                },
                new Category()
                {
                    Id=3,
                    Name="thirdOne"
                }
            };
        }

        private void PopulateData(SecondHandShopDbContext db)
        {
            db.Categories.AddRange(this.GetTestData());
            db.SaveChanges();
        }

        [Fact]
        public async Task FindNameShouldReturnNameOfTheCategory()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var categoriesService = new AdminCategoryService(db);
            var categoryId = 1;

            // Act
            var result = await categoriesService.FindName(categoryId);

            // Assert
            result
                .Should()
                .Be("firstOne");
        }

        [Fact]
        public async Task ExistsShouldReturnBooleanDependsIfItemIsInDb()
        {
            // Arrange
            var db = this.Context;
            this.PopulateData(db);

            var categoriesService = new AdminCategoryService(db);
            var categoryId = 1;

            // Act
            var result = await categoriesService.Exists(categoryId);

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

            var categoriesService = new AdminCategoryService(db);
            var categoryId = 1;

            // Act
            await categoriesService.Delete(categoryId);

            // Assert
            db
                .Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefault()
                .Should()
                .Be(null);

            db
                .Categories
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

            var categoriesService = new AdminCategoryService(db);
            var categoryId = 1;
            var newName = "newoneboiiiiii";

            // Act
            await categoriesService.Edit(categoryId, newName);

            // Assert
            db
                .Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefault()
                .Should()
                .NotBe(null);

            db
                .Categories
                .Where(c => c.Id == categoryId)
                .Select(c => c.Name)
                .FirstOrDefault()
                .Should()
                .Be(newName);
        }

        [Fact]
        public async Task ByIdShouldReturnTheItemThatIsCalled()
        {
            // Arrange
            //Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var categoriesService = new AdminCategoryService(db);
            var categoryId = 1;

            // Act
            var result = await categoriesService.ById(categoryId);

            // Assert
            result
                .Should()
                .NotBe(null);

            result
                .Name
                .Should()
                .Be("firstOne");
        }

        [Fact]
        public async Task AllAsyncShouldReturnCollectionOfAllItems()
        {
            // Arrange
            //Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var categoriesService = new AdminCategoryService(db);

            // Act
            var result = await categoriesService.AllAsync();

            // Assert
            result
                .Should()
                .Match(r=>r.ElementAt(0).Id==1
                            &&r.ElementAt(1).Id==2
                            &&r.ElementAt(2).Id==3)
                .And
                .HaveCount(3);
        }

        [Fact]
        public async Task CreateShouldReturnDbWithThatItem()
        {
            // Arrange
            var db = this.Context;

            var categoriesService = new AdminCategoryService(db);
            var newName = "forthetestname";

            // Act
            await categoriesService.Create(newName);

            // Assert
            db
                .Categories
                .Where(c => c.Name == newName)
                .FirstOrDefault()
                .Should()
                .NotBe(null);

            db
                .Categories
                .Where(c => c.Name == newName)
                .Select(c=>c.Id)
                .FirstOrDefault()
                .Should()
                .Be(1);
        }

        [Fact]
        public void AllShouldReturnCollectionOfAllItems()
        {
            // Arrange
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var categoriesService = new AdminCategoryService(db);

            // Act
            var result = categoriesService.All();

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 1
                            && r.ElementAt(1).Id == 2
                            && r.ElementAt(2).Id == 3)
                .And
                .HaveCount(3);
        }
    }
}