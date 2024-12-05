using Moq;
using AssetManagement.Interface;
using AssetManagement.Models;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class UserTests
    {
        private Mock<IUserRepository> userRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            userRepositoryMock = new Mock<IUserRepository>();
        }

        [Test]
        public async Task ReturnsAllUsersAsync()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { UserId = 1, UserName = "JohnDoe" },
                new User { UserId = 2, UserName = "JaneSmith" }
            };

            userRepositoryMock
                .Setup(u => u.GetAllUser())
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await userRepositoryMock.Object.GetAllUser();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.ElementAt(0).UserId, Is.EqualTo(1));
            Assert.That(result.ElementAt(1).UserName, Is.EqualTo("JaneSmith"));

            //var resultList = result.ToList();
            //Assert.That(resultList[0].UserId, Is.EqualTo(1));
            //Assert.That(resultList[1].UserName, Is.EqualTo("JaneSmith"));


        }

        [Test]
        public async Task AddUser_ShouldAddUser()
        {
            // Arrange
            var newUser = new User { UserId = 3, UserName = "NewUser" };

            userRepositoryMock
                .Setup(repo => repo.AddUser(It.IsAny<User>()))
                .Callback<User>(user => { });

            // Act
            await userRepositoryMock.Object.AddUser(newUser);

            // Assert
            userRepositoryMock.Verify(
                repo => repo.AddUser(It.Is<User>(
                    u => u.UserId == newUser.UserId && u.UserName == newUser.UserName)),
                Times.Once
            );
        }

        [Test]
        public async Task Save_ShouldCallSaveChanges()
        {
            // Act
            await userRepositoryMock.Object.Save();

            // Assert
            userRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Test]
        public async Task DeleteUser_ShouldRemoveUser()
        {
            // Arrange
            var userIdToDelete = 1;

            userRepositoryMock
                .Setup(repo => repo.DeleteUser(It.IsAny<int>()))
                .Callback<int>(id => { });

            // Act
            await userRepositoryMock.Object.DeleteUser(userIdToDelete);

            // Assert
            userRepositoryMock.Verify(
                repo => repo.DeleteUser(It.Is<int>(id => id == userIdToDelete)),
                Times.Once
            );
        }

        [Test]
        public async Task UpdateUser_ShouldUpdateUser()
        {
            // Arrange
            var updatedUser = new User { UserId = 1, UserName = "UpdatedUser" };

            userRepositoryMock
                .Setup(repo => repo.UpdateUser(It.IsAny<User>()))
                .Callback<User>(user => { });

            // Act
            await userRepositoryMock.Object.UpdateUser(updatedUser);

            // Assert
            userRepositoryMock.Verify(
                repo => repo.UpdateUser(It.Is<User>(
                    u => u.UserId == updatedUser.UserId && u.UserName == updatedUser.UserName)),
                Times.Once
            );
        }

        [Test]
        public async Task GetUserById_ShouldReturnUser()
        {
            // Arrange
            var expectedUser = new User { UserId = 1, UserName = "JohnDoe" };

            userRepositoryMock
                .Setup(repo => repo.GetUserById(It.IsAny<int>()))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await userRepositoryMock.Object.GetUserById(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserId, Is.EqualTo(expectedUser.UserId));
            Assert.That(result.UserName, Is.EqualTo(expectedUser.UserName));
        }
    }
}
