using Exercise8.Controllers;
using Exercise8.DTO;
using Exercise8.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Exercise8.Tests;

public class PrescriptionControllersTests
{
    private readonly Mock<IPrescriptionService> _mockPrescriptionService;
    private readonly PrescriptionsController _controller;

    public PrescriptionControllersTests()
    {
        _mockPrescriptionService = new Mock<IPrescriptionService>();
        _controller = new PrescriptionsController(_mockPrescriptionService.Object);
    }

    [Fact]
    public async Task CreatePrescriptionShouldReturnOkWhenPrescriptionIsCreatedSuccessfully()
    {
        // Arrange
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(1),
            Patient = new PatientDto { IdPatient = 1, FirstName = "Jan", LastName = "Testowy" },
            Doctor = new DoctorDto { IdDoctor = 1, FirstName = "Dr. Joanna", LastName = "Doctor" },
            Medicaments = new List<MedicamentDto>
            {
                new MedicamentDto { IdMedicament = 1, Name = "Paracetamol", Dose = 100, Description = "Pain relief" }
            }
        };

        _mockPrescriptionService.Setup(service => service.CreatePrescriptionAsync(request)).ReturnsAsync(true);

        // Act
        var result = await _controller.CreatePrescription(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Prescription created successfully.", okResult.Value);
    }

    [Fact]
    public async Task CreatePrescriptionShouldReturnBadRequestWhenDueDateIsEarlierThanDate()
    {
        // Arrange
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now.AddDays(7),
            DueDate = DateTime.Now,
            Patient = new PatientDto { IdPatient = 1, FirstName = "Jan", LastName = "Testowy" },
            Doctor = new DoctorDto { IdDoctor = 1, FirstName = "Dr. Joanna", LastName = "Doctor" },
            Medicaments = new List<MedicamentDto>
            {
                new MedicamentDto { IdMedicament = 1, Name = "Paracetamol", Dose = 100, Description = "Pain relief" }
            }
        };

        _mockPrescriptionService.Setup(s => s.CreatePrescriptionAsync(request))
            .ThrowsAsync(new ArgumentException("DueDate cannot be earlier than Date."));

        // Act
        var result = await _controller.CreatePrescription(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("DueDate cannot be earlier than Date.", badRequestResult.Value);
    }

    [Fact]
    public async Task CreatePrescriptionShouldReturnBadRequestWhenMedicamentsCountExceedsLimit()
    {
        // Arrange
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(1),
            Patient = new PatientDto { IdPatient = 1, FirstName = "Jan", LastName = "Testowy" },
            Doctor = new DoctorDto { IdDoctor = 1, FirstName = "Dr. Joanna", LastName = "Doctor" },
            Medicaments = new List<MedicamentDto>
            {
                new MedicamentDto { IdMedicament = 1, Name = "Paracetamol", Dose = 100, Description = "Pain relief" },
                new MedicamentDto { IdMedicament = 2, Name = "Ibuprofen", Dose = 200, Description = "Anti-inflammatory" },
                new MedicamentDto { IdMedicament = 3, Name = "Aspirin", Dose = 300, Description = "Pain relief" },
                new MedicamentDto { IdMedicament = 4, Name = "Codeine", Dose = 400, Description = "Pain relief" },
                new MedicamentDto { IdMedicament = 5, Name = "Amoxicillin", Dose = 500, Description = "Antibiotic" },
                new MedicamentDto { IdMedicament = 6, Name = "Ciprofloxacin", Dose = 600, Description = "Antibiotic" },
                new MedicamentDto { IdMedicament = 7, Name = "Paracetamol", Dose = 100, Description = "Pain relief" },
                new MedicamentDto { IdMedicament = 8, Name = "Ibuprofen", Dose = 200, Description = "Anti-inflammatory" },
                new MedicamentDto { IdMedicament = 9, Name = "Aspirin", Dose = 300, Description = "Pain relief" },
                new MedicamentDto { IdMedicament = 10, Name = "Codeine", Dose = 400, Description = "Pain relief" },
                new MedicamentDto { IdMedicament = 11, Name = "Amoxicillin", Dose = 500, Description = "Antibiotic" }
            }
        };

        _mockPrescriptionService.Setup(s => s.CreatePrescriptionAsync(request))
           .ThrowsAsync(new ArgumentException("A prescription cannot contain more than 10 medicaments."));

        // Act
        var result = await _controller.CreatePrescription(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("A prescription cannot contain more than 10 medicaments.", badRequestResult.Value);
    }

    [Fact]
    public async Task CreatePrescriptionShouldReturnBadRequestWhenDoctorDoesNotExist()
    {
        // Arrange
        var request = new PrescriptionRequestDto
        {
            Date = DateTime.Now.AddDays(7),
            DueDate = DateTime.Now,
            Patient = new PatientDto { IdPatient = 1, FirstName = "Jan", LastName = "Testowy" },
            Doctor = new DoctorDto { IdDoctor = 999, FirstName = "Dr. Joan", LastName = "Invisible" },
            Medicaments = new List<MedicamentDto>
            {
                new MedicamentDto { IdMedicament = 1, Name = "Paracetamol", Dose = 100, Description = "Pain relief" }
            }
        };

        _mockPrescriptionService.Setup(s => s.CreatePrescriptionAsync(request)).ThrowsAsync(new ArgumentException("Doctor with the given id does not exist."));

        // Act
        var result = await _controller.CreatePrescription(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Doctor with the given id does not exist.", badRequestResult.Value);
    }
}
