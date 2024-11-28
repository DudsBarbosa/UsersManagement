using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests
{
    public class UserTests
    {
        private readonly Mock<UserRepository> _userRepositoryMock;
        public UserTests()
        {
            _userRepositoryMock = new Mock<UserRepository>();
        }

        [Fact]
        public async Task AddAsyncTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<UserContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);
            var user = new User
            {
                Id = 1,
                Name = "John Doe",
                HourValue = 50.0m,
                AddDate = DateTime.Now,
                Active = true
            };

            // Act
            await service.AddAsync(user);

            // Assert
            mockSet.Verify(m => m.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<UserContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);
            // Act
            var result = await service.GetAllAsync();
            // Assert
            mockSet.Verify(m => m.ToListAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsyncTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<UserContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);
            var user = new User
            {
                Id = 1,
                Name = "John Doe",
                HourValue = 50.0m,
                AddDate = DateTime.Now,
                Active = true
            };
            // Act
            var result = await service.GetByIdAsync(1);
            // Assert
            mockSet.Verify(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task RemoveAsyncTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<UserContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);
            var user = new User
            {
                Id = 1,
                Name = "John Doe",
                HourValue = 50.0m,
                AddDate = DateTime.Now,
                Active = true
            };
            // Act
            await service.RemoveAsync(user);
            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
