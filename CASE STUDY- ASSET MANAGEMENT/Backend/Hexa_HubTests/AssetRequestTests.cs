using Moq;
using AssetManagement.Interface;
using static AssetManagement.Models.MultiValues;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class AssetRequestTests
    {
        private Mock<IAssetRequest> assetRequestMock;

        [SetUp]
        public void SetUp()
        {
            assetRequestMock = new Mock<IAssetRequest>();
        }

        [Test]
        public async Task ReturnsAllAssetRequestsAsync()
        {
            // Arrange
            var expectedAssetRequests = new List<AssetRequest>
            {
                new AssetRequest { AssetReqId = 1, RequestStatus = RequestStatus.Pending },
                new AssetRequest { AssetReqId = 2, RequestStatus = RequestStatus.Allocated }
            };

            assetRequestMock
                .Setup(a => a.GetAllAssetRequests())
                .ReturnsAsync(expectedAssetRequests);

            // Act
            var result = await assetRequestMock.Object.GetAllAssetRequests();

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Result count should match the expected count");
            Assert.That(result[0].RequestStatus, Is.EqualTo(RequestStatus.Pending), "First request status should be Pending");
            Assert.That(result[1].RequestStatus, Is.EqualTo(RequestStatus.Allocated), "Second request status should be Allocated");
        }

        [Test]
        public async Task AddAssetRequest_ShouldAddAssetRequest()
        {
            // Arrange
            var newAssetRequest = new AssetRequest { AssetReqId = 3, RequestStatus = RequestStatus.Rejected };

            assetRequestMock
                .Setup(repo => repo.AddAssetRequest(It.IsAny<AssetRequest>()))
                .Callback<AssetRequest>(assetReq => { });

            // Act
            await assetRequestMock.Object.AddAssetRequest(newAssetRequest);

            // Assert
            assetRequestMock.Verify(
                repo => repo.AddAssetRequest(It.Is<AssetRequest>(
                    a => a.AssetReqId == newAssetRequest.AssetReqId && a.RequestStatus == newAssetRequest.RequestStatus)),
                Times.Once
            );
        }

        [Test]
        public async Task Save_ShouldCallSaveChanges()
        {
            // Act
            await assetRequestMock.Object.Save();

            // Assert
            assetRequestMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Test]
        public async Task DeleteRequest_ShouldRemoveRequest()
        {
            // Arrange
            var reqIdToDelete = 1;

            assetRequestMock
                .Setup(repo => repo.DeleteAssetRequest(It.IsAny<int>()))
                .Callback<int>(id => { });

            // Act
            await assetRequestMock.Object.DeleteAssetRequest(reqIdToDelete);

            // Assert
            assetRequestMock.Verify(
                repo => repo.DeleteAssetRequest(It.Is<int>(id => id == reqIdToDelete)),
                Times.Once
            );
        }

        [Test]
        public async Task UpdateRequest_ShouldUpdateRequest()
        {
            // Arrange
            var updatedRequest = new AssetRequest { AssetReqId = 1, RequestStatus = RequestStatus.Pending };

            assetRequestMock
                .Setup(repo => repo.UpdateAssetRequest(It.IsAny<AssetRequest>()))
                .ReturnsAsync(updatedRequest);

            // Act
            var result = await assetRequestMock.Object.UpdateAssetRequest(updatedRequest);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.AssetReqId, Is.EqualTo(updatedRequest.AssetReqId), "AssetReq ID should match");
            Assert.That(result.RequestStatus, Is.EqualTo(updatedRequest.RequestStatus), "Request Status should be updated");

            assetRequestMock.Verify(
                repo => repo.UpdateAssetRequest(It.Is<AssetRequest>(
                    a => a.AssetReqId == updatedRequest.AssetReqId && a.RequestStatus == updatedRequest.RequestStatus)),
                Times.Once
            );
        }
    }
}
