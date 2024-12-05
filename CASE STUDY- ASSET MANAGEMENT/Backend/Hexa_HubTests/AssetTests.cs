using AssetManagement.Interface;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class AssetTests
    {
        private Mock<IAsset> assetMock;
        private IAsset asset;

        [SetUp]
        public void SetUp()
        {
            assetMock = new Mock<IAsset>();
            asset = assetMock.Object;
        }

        [Test]
        public async Task ReturnsAllAssetsAsync()
        {
            // Arrange
            var expectedAssets = new List<Asset>
            {
                new Asset { AssetId = 1, AssetName = "Dell Laptop", SubCategoryId = 1, SerialNumber = "7GHY5265", Model = "G14", Location = "Chennai" },
                new Asset { AssetId = 2, AssetName = "Headphone", SubCategoryId = 2, SerialNumber = "DT6788", Model = "5G67", Location = "Mumbai" }
            };

            assetMock.Setup(a => a.GetAllAssets())
                .ReturnsAsync(expectedAssets);

            // Act
            var result = await asset.GetAllAssets();

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Asset count should be 2");
            Assert.That(result[0].AssetName, Is.EqualTo("Dell Laptop"));
            Assert.That(result[1].AssetName, Is.EqualTo("Headphone"));
        }

        [Test]
        public async Task AddAsset_ShouldAddAsset()
        {
            // Arrange
            var newAsset = new Asset { AssetId = 3, AssetName = "DriverDisk" };

            assetMock.Setup(repo => repo.AddAsset(It.IsAny<Asset>())).Callback((Asset asset) => { });

            // Act
            await asset.AddAsset(newAsset);

            // Assert
            assetMock.Verify(repo => repo.AddAsset(It.Is<Asset>(a => a.AssetId == newAsset.AssetId && a.AssetName == newAsset.AssetName)), Times.Once);
        }

        [Test]
        public async Task Save_ShouldCallSaveChanges()
        {
            // Act
            await asset.Save();

            // Assert
            assetMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Test]
        public async Task DeleteAsset_ShouldRemoveAsset()
        {
            // Arrange
            var assetIdToDelete = 1;

            assetMock.Setup(repo => repo.DeleteAsset(It.IsAny<int>())).Callback<int>(id => { });

            // Act
            await asset.DeleteAsset(assetIdToDelete);

            // Assert
            assetMock.Verify(repo => repo.DeleteAsset(It.Is<int>(id => id == assetIdToDelete)), Times.Once);
        }

        [Test]
        public async Task UpdateAsset_ShouldUpdateAsset()
        {
            // Arrange
            var updatedAsset = new Asset { AssetId = 1, AssetName = "Lenovo Laptop" };

            assetMock.Setup(repo => repo.UpdateAsset(It.IsAny<Asset>())).ReturnsAsync((Asset asset) => asset);

            // Act
            var result = await asset.UpdateAsset(updatedAsset);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.AssetId, Is.EqualTo(updatedAsset.AssetId), "Asset ID should match");
            Assert.That(result.AssetName, Is.EqualTo(updatedAsset.AssetName), "Asset name should be updated");

            assetMock.Verify(repo => repo.UpdateAsset(It.Is<Asset>(a =>
                a.AssetId == updatedAsset.AssetId && a.AssetName == updatedAsset.AssetName)), Times.Once);
        }
    }
}
