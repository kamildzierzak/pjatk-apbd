using Exercise8.Controllers;
using Exercise8.DTO;
using Exercise8.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Exercise8.Tests;

public class PatientsControllerTests
{
    private readonly Mock<IPatientService> _mockPatientService;
    private readonly PatientsController _controller;

    public PatientsControllerTests()
    {
        _mockPatientService = new Mock<IPatientService>();
        _controller = new PatientsController(_mockPatientService.Object);
    }

    [Fact]
    public async Task GetPatientDataShouldReturnPatientDataIfPatientExists()
    {
        // Arrange
        var idPatient = 1;
        var patientDto = new PatientDto
        {
            IdPatient = idPatient,
            FirstName = "Jan",
            LastName = "Tester",
            Birthdate = new System.DateTime(1999, 9, 9),
            Prescriptions = new List<PrescriptionDto>()
        };

        _mockPatientService.Setup(service => service.GetPatientDataAsync(idPatient)).ReturnsAsync(patientDto);

        // Act
        var result = await _controller.GetPatientData(idPatient);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
        Assert.Equal(patientDto.IdPatient, returnedPatient.IdPatient);
    }

    [Fact]
    public async Task GetPatientDataShouldReturnNotFoundIfPatientDoesNotExists()
    {
        // Arrange
        var idPatient = 9999;

        _mockPatientService.Setup(service => service.GetPatientDataAsync(idPatient)).ReturnsAsync((PatientDto)null);

        // Act
        var result = await _controller.GetPatientData(idPatient);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal($"Patient with id {idPatient} not found.", notFoundResult.Value);
    }
}