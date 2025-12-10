//using Microsoft.EntityFrameworkCore;
//using OCR_05_Express_Voiture.Data;
//using OCR_05_Express_Voiture.Models.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
//using OCR_05_Express_Voiture.Models.Entities;
//using Xunit;

//namespace OCR_05_Express_Voiture_Test
//{
//    public abstract class GenericRepositoryTest<TEntity, TRepository>
//        where TEntity : class
//        where TRepository : IGenericRepository<TEntity>
//    {
//        // Contexte de base de données en mémoire pour les tests
//        private static ApplicationDbContext CreateInMemoryContextTest()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                            .Options;
//            var context = new ApplicationDbContext(options);
//            context.Database.EnsureCreated();
//            return context;
//        }
//        // Méthode pour créer une instance du repository

//        protected abstract TRepository CreateRepositoryTest(ApplicationDbContext context);
//        protected abstract TRepository InsertEntityTest(ApplicationDbContext context);

//        [Fact]
//            public async Task GetAllAsync_ShouldReturnAllEntities()
//            {
//                //Arrange
//               var context = CreateInMemoryContextTest();
//               var repository = CreateRepositoryTest(context);
//                InsertEntityTest(context);
//                InsertEntityTest(context);

//            //  wait context.SaveChangesAsync();

//            // Act
//            var results = await repository.GetAllAsync();

//                // Assert
//                Assert.NotNull(results);
//                Assert.True(results.Count() >= 2);
//               // Assert.True(true);

//        }

//    }
//}

