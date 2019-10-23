using LambdaForums.Data;
using LambdaForums.Data.Models;
using LambdaForums.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace LambdaForums.Tests
{
    [TestFixture]
    public class Search_Service_Should
    {
        //[Test]
        //[OneTimeSetUp]
        //[OneTimeTearDown]
        //[SetUp]
        //[TearDown]

        [TestCase("coffee", 3)]
        [TestCase("teA", 1)]
        [TestCase("water", 0)]
        public void Return_Results_Corresponding_To_Query(string query, int expected)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            //Arrange
            using(var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 5,
                    Title = "Test forum"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(2),
                    Id = 444,
                    Title = "First Post",
                    Content = "Coffee"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(2),
                    Id = -598,
                    Title = "Coffee",
                    Content = "Some Content"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(2),
                    Id = 234,
                    Title = "Tea",
                    Content = "Coffee"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetFilteredPosts(query);
                var postCount = result.Count();

                //Assert
                Assert.AreEqual(expected, postCount);
            }

        }
    }
}
