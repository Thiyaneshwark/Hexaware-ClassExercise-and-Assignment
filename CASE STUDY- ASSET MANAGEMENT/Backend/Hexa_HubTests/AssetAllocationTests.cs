using Moq;
using AssetManagement.Interface;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class AssetAllocationTests
    {
        private Mock<IAssetAllocation> assetAllocationMock;

        [SetUp]
        public void SetUp()
        {
            assetAllocationMock = new Mock<IAssetAllocation>();
        }

        [Test]
        public async Task ReturnsAllAssetAllocationsAsync()
        {
            // Arrange
            var expectedAssetAllocations = new List<AssetAllocation>
            {
                new AssetAllocation { AllocationId = 1, AssetId = 1 },
                new AssetAllocation { AllocationId = 2, AssetId = 2 }
            };

            assetAllocationMock
                .Setup(a => a.GetAllAllocations())
                .ReturnsAsync(expectedAssetAllocations);

            // Act
            var result = await assetAllocationMock.Object.GetAllAllocations();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].AllocationId, Is.EqualTo(1));
            Assert.That(result[1].AllocationId, Is.EqualTo(2));
        }

        [Test]
        public async Task AddAssetAllocation_ShouldAddAssetAllocation()
        {
            // Arrange
            var newAssetAllocation = new AssetAllocation { AllocationId = 3, AssetId = 4 };

            assetAllocationMock
                .Setup(repo => repo.AddAllocation(It.IsAny<AssetAllocation>()))
                .Callback<AssetAllocation>(assetAlloc => { });

            // Act
            await assetAllocationMock.Object.AddAllocation(newAssetAllocation);

            // Assert
            assetAllocationMock.Verify(
                repo => repo.AddAllocation(It.Is<AssetAllocation>(
                    a => a.AllocationId == newAssetAllocation.AllocationId && a.AssetId == newAssetAllocation.AssetId)),
                Times.Once
            );
        }

        [Test]
        public async Task Save_ShouldCallSaveChanges()
        {
            // Act
            await assetAllocationMock.Object.Save();

            // Assert
            assetAllocationMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Test]
        public async Task DeleteAllocation_ShouldRemoveAllocation()
        {
            // Arrange
            var allocationIdToDelete = 1;

            assetAllocationMock
                .Setup(repo => repo.DeleteAllocation(It.IsAny<int>()))
                .Callback<int>(id => { });

            // Act
            await assetAllocationMock.Object.DeleteAllocation(allocationIdToDelete);

            // Assert
            assetAllocationMock.Verify(
                repo => repo.DeleteAllocation(It.Is<int>(id => id == allocationIdToDelete)),
                Times.Once
            );
        }

        [Test]
        public async Task UpdateAllocation_ShouldUpdateAllocation()
        {
            // Arrange
            var updatedAllocation = new AssetAllocation { AllocationId = 1, AssetId = 5 };

            assetAllocationMock
                .Setup(repo => repo.UpdateAllocation(It.IsAny<AssetAllocation>()))
                .ReturnsAsync(updatedAllocation);

            // Act
            var result = await assetAllocationMock.Object.UpdateAllocation(updatedAllocation);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AssetId, Is.EqualTo(updatedAllocation.AssetId));
            Assert.That(result.AllocationId, Is.EqualTo(updatedAllocation.AllocationId));

            assetAllocationMock.Verify(
                repo => repo.UpdateAllocation(It.Is<AssetAllocation>(
                    a => a.AllocationId == updatedAllocation.AllocationId && a.AssetId == updatedAllocation.AssetId)),
                Times.Once
            );
        }
    }
}
