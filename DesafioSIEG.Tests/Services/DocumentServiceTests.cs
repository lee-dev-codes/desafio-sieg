using Desafio_SIEG.Data;
using Desafio_SIEG.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class DocumentServiceTests
{
    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_Not_Insert_Duplicate_Document()
    {
        // Arrange
        var context = CreateContext();
        var parser = new FakeXmlParserService();
        var service = new DocumentService(context, parser);

        var xml = "<NFe><infNFe><emit><CNPJ>12345678000199</CNPJ></emit></infNFe></NFe>";

        // Act
        var firstId = await service.CreateAsync(xml);

        Func<Task> act = async () => await service.CreateAsync(xml);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();

        var count = await context.Documents.CountAsync();
        count.Should().Be(1);
    }
}
