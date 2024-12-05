using Moq;
using AssetManagement.Interface;
using AssetManagement.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AssetManagement.Models.MultiValues;
using System;

namespace Tests
{
    public class ReturnRequestTests
    {
        private Mock<IReturnReqRepo> returnRequestRepoMock;

        [SetUp]
        public void SetUp()
        {
            returnRequestRepoMock = new Mock<IReturnReqRepo>();
        }

        [Test]
        public async Task ReturnsAllReturnRequestsAsync()
        {
            // Arrange
            var expectedReturnRequests = new List<ReturnRequest>
            {
                new ReturnRequest { ReturnId = 1, ReturnStatus = ReturnReqStatus.Sent },
                new ReturnRequest { ReturnId = 2, ReturnStatus = ReturnReqStatus.Approved }
            };

            returnRequestRepoMock
                .Setup(repo => repo.GetAllReturnRequest())
                .ReturnsAsync(expectedReturnRequests);

            // Act
            var result = await returnRequestRepoMock.Object.GetAllReturnRequest();

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Result count should match the expected count");
            Assert.That(result[0].ReturnStatus, Is.EqualTo(ReturnReqStatus.Sent), "First request status should be Sent");
            Assert.That(result[1].ReturnStatus, Is.EqualTo(ReturnReqStatus.Approved), "Second request status should be Approved");
        }

        [Test]
        public async Task AddReturnRequest_ShouldAddReturnRequest()
        {
            // Arrange
            var newReturnRequest = new ReturnRequest { ReturnId = 3, ReturnStatus = ReturnReqStatus.Approved };

            returnRequestRepoMock
                .Setup(repo => repo.AddReturnRequest(It.IsAny<ReturnRequest>()))
                .Callback<ReturnRequest>(req => { });

            // Act
            await returnRequestRepoMock.Object.AddReturnRequest(newReturnRequest);

            // Assert
            returnRequestRepoMock.Verify(
                repo => repo.AddReturnRequest(It.Is<ReturnRequest>(
                    r => r.ReturnId == newReturnRequest.ReturnId && r.ReturnStatus == newReturnRequest.ReturnStatus)),
                Times.Once
            );
        }

        [Test]
        public async Task DeleteReturnRequest_ShouldRemoveRequest()
        {
            // Arrange
            var reqIdToDelete = 1;

            returnRequestRepoMock
                .Setup(repo => repo.DeleteReturnRequest(It.IsAny<int>()))
                .Callback<int>(id => { });

            // Act
            await returnRequestRepoMock.Object.DeleteReturnRequest(reqIdToDelete);

            // Assert
            returnRequestRepoMock.Verify(
                repo => repo.DeleteReturnRequest(It.Is<int>(id => id == reqIdToDelete)),
                Times.Once
            );
        }

        [Test]
        public async Task UpdateReturnRequest_ShouldUpdateRequest()
        {
            // Arrange
            var updatedRequest = new ReturnRequest { ReturnId = 1, ReturnStatus = ReturnReqStatus.Approved };

            returnRequestRepoMock
                .Setup(repo => repo.UpdateReturnRequest(It.IsAny<ReturnRequest>()))
                .Callback<ReturnRequest>(req => { });

            // Act
            returnRequestRepoMock.Object.UpdateReturnRequest(updatedRequest);

            // Assert
            returnRequestRepoMock.Verify(
                repo => repo.UpdateReturnRequest(It.Is<ReturnRequest>(
                    r => r.ReturnId == updatedRequest.ReturnId && r.ReturnStatus == updatedRequest.ReturnStatus)),
                Times.Once
            );
        }

        [Test]
        public async Task Save_ShouldCallSaveChanges()
        {
            // Act
            await returnRequestRepoMock.Object.Save();

            // Assert
            returnRequestRepoMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Test]
        public async Task GetReturnRequestsByUserId_ShouldReturnRequestsForUser()
        {
            // Arrange
            var userId = 1;
            var userRequests = new List<ReturnRequest>
            {
                new ReturnRequest { ReturnId = 1, UserId = userId, ReturnStatus = ReturnReqStatus.Sent },
                new ReturnRequest { ReturnId = 2, UserId = userId, ReturnStatus = ReturnReqStatus.Approved }
            };

            returnRequestRepoMock
                .Setup(repo => repo.GetReturnRequestsByUserId(userId))
                .ReturnsAsync(userRequests);

            // Act
            var result = await returnRequestRepoMock.Object.GetReturnRequestsByUserId(userId);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Result count should match the expected count");
            Assert.That(result[0].UserId, Is.EqualTo(userId), "UserId of first request should match");
        }

        [Test]
        public async Task UserHasAsset_ShouldReturnTrueIfUserHasAsset()
        {
            // Arrange
            var userId = 1;

            returnRequestRepoMock
                .Setup(repo => repo.UserHasAsset(userId))
                .ReturnsAsync(true);

            // Act
            var result = await returnRequestRepoMock.Object.UserHasAsset(userId);

            // Assert
            Assert.That(result, Is.True, "User should have asset");
        }
    }
}
