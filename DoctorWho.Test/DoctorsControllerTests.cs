using DoctorWho.Db.Entities;
using DoctorWho.Db.Repositories;
using DoctorWho.Web.Controllers;
using DoctorWho.Web.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DoctorWho.Test
{
    public class DoctorsControllerTests
    {
        private Mock<IDoctorRepository> _mockDoctorRepository;
        private DoctorsController _controller;

        public DoctorsControllerTests()
        {
            _mockDoctorRepository = new Mock<IDoctorRepository>();
            _controller = new DoctorsController(_mockDoctorRepository.Object);
        }


        [Theory]
        [MemberData(nameof(GetAllDoctorsTestScenarios))]
        public async Task GetAllDoctors_ReturnsOkResultWithDoctorDtos(IEnumerable<DoctorDto> expectedDoctors)
        {
            // Arrange
            _mockDoctorRepository.Setup(repo => repo.GetAllDoctors())
                .ReturnsAsync(expectedDoctors);

            // Act
            var result = await _controller.GetAllDoctors();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualDoctors = Assert.IsAssignableFrom<IEnumerable<DoctorDto>>(okResult.Value);
            Assert.Equal(expectedDoctors.Count(), actualDoctors.Count());
            _mockDoctorRepository.Verify(repo => repo.GetAllDoctors(), Times.Once);
        }
        
        [Fact]
        public async Task DeleteDoctor_ExistingDoctor_ReturnsNoContent()
        {
            // Arrange
            var existingDoctorId = 1;
            _mockDoctorRepository.Setup(repo => repo.IsDoctorExistAsync(existingDoctorId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteDoctor(existingDoctorId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockDoctorRepository.Verify(repo => repo.DeleteDoctor(existingDoctorId), Times.Once);
        }

        [Fact]
        public async Task DeleteDoctor_NonExistingDoctor_ReturnsNotFound()
        {
            // Arrange
            var nonExistingDoctorId = 2;
            _mockDoctorRepository.Setup(repo => repo.IsDoctorExistAsync(nonExistingDoctorId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteDoctor(nonExistingDoctorId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            _mockDoctorRepository.Verify(repo => repo.DeleteDoctor(nonExistingDoctorId), Times.Never);
        }

        [Fact]
        public async Task UpsertDoctor_ExistingDoctor_ReturnsOkWithUpdatedDoctor()
        {
            // Arrange
            var existingDoctorId = 1;
            var doctorDto = new DoctorDto { DoctorId = existingDoctorId, DoctorName = "Updated Name" };
            var existingDoctorEntity = new Doctor{ DoctorId = existingDoctorId, DoctorName = "Original Name" };

            _mockDoctorRepository.Setup(repo => repo.GetDoctorById(existingDoctorId)).ReturnsAsync(existingDoctorEntity);
            _mockDoctorRepository.Setup(repo => repo.UpdateDoctor(existingDoctorEntity, doctorDto)).ReturnsAsync(doctorDto);

            // Act
            var result = await _controller.UpsertDoctor(doctorDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedDoctor = Assert.IsType<DoctorDto>(okResult.Value);
            Assert.Equal(doctorDto.DoctorName, updatedDoctor.DoctorName);
            Assert.Equal(doctorDto.DoctorName, updatedDoctor.DoctorName);
            Assert.Equal(doctorDto.DoctorName, updatedDoctor.DoctorName);
        }

        [Fact]
        public async Task UpsertDoctor_InvalidDoctorId_ReturnsNotFound()
        {
            // Arrange
            var invalidDoctorId = int.MaxValue;
            _mockDoctorRepository.Setup(repo => repo.GetDoctorById(invalidDoctorId))
                .ReturnsAsync((Doctor?)null);

            var invalidDoctorDto = new DoctorDto{ DoctorId = invalidDoctorId }; 

            // Act
            var result = await _controller.UpsertDoctor(invalidDoctorDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Doctor not found", notFoundResult.Value);
            _mockDoctorRepository.Verify(repo => repo.GetDoctorById(invalidDoctorId), Times.Once);
            _mockDoctorRepository.Verify(repo => repo.UpdateDoctor(It.IsAny<Doctor>(), It.IsAny<DoctorDto>()), Times.Never);
            _mockDoctorRepository.Verify(repo => repo.CreateDoctor(It.IsAny<DoctorDto>()), Times.Never);
        }
        [Fact]
        public async Task UpsertDoctor_NewDoctor_ReturnsOkWithCreatedDoctor()
        {
            // Arrange
            var newDoctorDto = new DoctorDto { DoctorName = "New Doctor" };

            _mockDoctorRepository.Setup(repo => repo.CreateDoctor(newDoctorDto))
                .ReturnsAsync(newDoctorDto);

            // Act
            var result = await _controller.UpsertDoctor(newDoctorDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var createdDoctor = Assert.IsType<DoctorDto>(okResult.Value);
            Assert.Equal(newDoctorDto.DoctorName, createdDoctor.DoctorName);
        }


        public static IEnumerable<object[]> GetAllDoctorsTestScenarios()
        {
            yield return new object[] { Enumerable.Empty<DoctorDto>() }; // Empty scenario

            yield return new object[]
            {
                new[]
                {
                    new DoctorDto
                    {
                        DoctorId = 1,
                        DoctorNumber = 9,
                        DoctorName = "The Ninth Doctor",
                        BirthDate = new DateTime(2000, 1, 1),
                        FirstEpisodeDate = new DateTime(2005, 3, 26),
                        LastEpisodeDate = new DateTime(2005, 6, 18)
                    },
                    new DoctorDto
                    {
                        DoctorId = 2,
                        DoctorNumber = 10,
                        DoctorName = "The Tenth Doctor",
                        BirthDate = new DateTime(2005, 1, 1),
                        FirstEpisodeDate = new DateTime(2005, 6, 18),
                        LastEpisodeDate = new DateTime(2010, 1, 1)
                    }
                }
            };
        }

    }
}






