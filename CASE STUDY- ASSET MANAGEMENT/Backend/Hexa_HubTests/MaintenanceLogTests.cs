using Moq;
using NUnit.Framework;
using AssetManagement.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Tests
{
    public class MaintenanceLogTests
    {
        private IMaintenanceLogRepo _maintenanceLogRepo;
        private Mock<IMaintenanceLogRepo> _mockMaintenanceLogRepo;

        [SetUp]
        public void SetUp()
        {
            _mockMaintenanceLogRepo = new Mock<IMaintenanceLogRepo>();
            _maintenanceLogRepo = _mockMaintenanceLogRepo.Object;
        }

        [Test]
        public async Task GetAllMaintenanceLog_ShouldReturnAllLogs()
        {
            // Arrange
            var logs = new List<MaintenanceLog>
            {
                new MaintenanceLog
                {
                    MaintenanceId = 1,
                    AssetId = 101,
                    UserId = 1001,
                    Maintenance_date = new DateTime(2024, 11, 19),
                    Cost = 150.5m,
                    Maintenance_Description = "Replaced keyboard"
                },
                new MaintenanceLog
                {
                    MaintenanceId = 2,
                    AssetId = 102,
                    UserId = 1002,
                    Maintenance_date = new DateTime(2024, 11, 18),
                    Cost = 200.0m,
                    Maintenance_Description = "Repaired monitor"
                }
            };

            _mockMaintenanceLogRepo.Setup(repo => repo.GetAllMaintenanceLog()).ReturnsAsync(logs);

            // Act
            var result = await _maintenanceLogRepo.GetAllMaintenanceLog();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].MaintenanceId, Is.EqualTo(1));
            Assert.That(result[1].Maintenance_Description, Is.EqualTo("Repaired monitor"));
        }

        [Test]
        public async Task GetMaintenanceLogById_ShouldReturnCorrectLog()
        {
            // Arrange
            var maintenanceLog = new MaintenanceLog
            {
                MaintenanceId = 1,
                AssetId = 101,
                UserId = 1001,
                Maintenance_date = new DateTime(2024, 11, 19),
                Cost = 150.5m,
                Maintenance_Description = "Replaced keyboard"
            };

            _mockMaintenanceLogRepo.Setup(repo => repo.GetMaintenanceLogById(1)).ReturnsAsync(maintenanceLog);

            // Act
            var result = await _maintenanceLogRepo.GetMaintenanceLogById(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.MaintenanceId, Is.EqualTo(1));
            Assert.That(result.Maintenance_Description, Is.EqualTo("Replaced keyboard"));
        }

        [Test]
        public async Task AddMaintenanceLog_ShouldAddLogSuccessfully()
        {
            // Arrange
            var newLog = new MaintenanceLog
            {
                MaintenanceId = 3,
                AssetId = 103,
                UserId = 1003,
                Maintenance_date = new DateTime(2024, 11, 20),
                Cost = 300.0m,
                Maintenance_Description = "Updated BIOS"
            };

            _mockMaintenanceLogRepo.Setup(repo => repo.AddMaintenanceLog(It.IsAny<MaintenanceLog>()));

            // Act
            await _maintenanceLogRepo.AddMaintenanceLog(newLog);

            // Assert
            _mockMaintenanceLogRepo.Verify(repo => repo.AddMaintenanceLog(It.Is<MaintenanceLog>(log =>
                log.MaintenanceId == newLog.MaintenanceId &&
                log.AssetId == newLog.AssetId &&
                log.Cost == newLog.Cost)), Times.Once);
        }

        [Test]
        public async Task UpdateMaintenanceLog_ShouldUpdateLogSuccessfully()
        {
            // Arrange
            var updatedLog = new MaintenanceLog
            {
                MaintenanceId = 1,
                AssetId = 101,
                UserId = 1001,
                Maintenance_date = new DateTime(2024, 11, 19),
                Cost = 170.5m,
                Maintenance_Description = "Replaced keyboard and mouse"
            };

            _mockMaintenanceLogRepo.Setup(repo => repo.UpdateMaintenanceLog(It.IsAny<MaintenanceLog>())).ReturnsAsync(updatedLog);

            // Act
            var result = await _maintenanceLogRepo.UpdateMaintenanceLog(updatedLog);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Cost, Is.EqualTo(170.5m));
            Assert.That(result.Maintenance_Description, Is.EqualTo("Replaced keyboard and mouse"));
        }

        [Test]
        public async Task DeleteMaintenanceLog_ShouldDeleteLogSuccessfully()
        {
            // Arrange
            var logId = 1;

            _mockMaintenanceLogRepo.Setup(repo => repo.DeleteMaintenanceLog(logId)).Verifiable();

            // Act
            await _maintenanceLogRepo.DeleteMaintenanceLog(logId);

            // Assert
            _mockMaintenanceLogRepo.Verify(repo => repo.DeleteMaintenanceLog(It.Is<int>(id => id == logId)), Times.Once);
        }

        [Test]
        public async Task GetMaintenanceLogByUserId_ShouldReturnLogsForSpecificUser()
        {
            // Arrange
            var userId = 1001;
            var logs = new List<MaintenanceLog>
            {
                new MaintenanceLog
                {
                    MaintenanceId = 1,
                    AssetId = 101,
                    UserId = userId,
                    Maintenance_date = new DateTime(2024, 11, 19),
                    Cost = 150.5m,
                    Maintenance_Description = "Replaced keyboard"
                }
            };

            _mockMaintenanceLogRepo.Setup(repo => repo.GetMaintenanceLogByUserId(userId)).ReturnsAsync(logs);

            // Act
            var result = await _maintenanceLogRepo.GetMaintenanceLogByUserId(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].UserId, Is.EqualTo(userId));
        }

        [Test]
        public async Task Save_ShouldSaveChangesSuccessfully()
        {
            // Arrange
            _mockMaintenanceLogRepo.Setup(repo => repo.Save()).Verifiable();

            // Act
            await _maintenanceLogRepo.Save();

            // Assert
            _mockMaintenanceLogRepo.Verify(repo => repo.Save(), Times.Once);
        }
    }
}
