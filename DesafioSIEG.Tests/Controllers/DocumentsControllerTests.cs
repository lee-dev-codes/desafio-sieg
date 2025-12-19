using Desafio_SIEG.Controllers;
using Desafio_SIEG.DTOs;
using Desafio_SIEG.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class DocumentsControllerTests
{
    [Fact]
    public async Task GetAll_Should_Return_Ok_With_Documents()
    {
        // Arrange
        var mockService = new Mock<IDocumentService>();

        mockService
            .Setup(s => s.GetAllAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>()))
            .ReturnsAsync(new List<DocumentResponseDto>
            {
                new DocumentResponseDto
                {
                    Id = Guid.NewGuid(),
                    Tipo = "NFe",
                    CnpjEmitente = "12345678000199",
                    UfEmitente = "SP",
                    DataEmissao = DateTime.UtcNow
                }
            });

        var controller = new DocumentsController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();

        var data = okResult!.Value as IEnumerable<DocumentResponseDto>;
        data.Should().HaveCount(1);
    }
}
