using System;
using JuniorStart.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JuniorStart.Tests
{
    [TestFixture()]
    public class TodoListTest
    {
        private DbContextOptions<ApplicationContext> options;

        [SetUp]
        public void Init()
        {
            options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AppDatabase")
                .Options;
        }
    }
}
