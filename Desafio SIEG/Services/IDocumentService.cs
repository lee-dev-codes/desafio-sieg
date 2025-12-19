using Desafio_SIEG.DTOs;

namespace Desafio_SIEG.Services;

public interface IDocumentService
{
    Task<IEnumerable<DocumentResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? cnpj,
        string? uf,
        DateTime? dataInicio,
        DateTime? dataFim);

    Task<DocumentResponseDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(string xml);

    Task<bool> UpdateUfAsync(Guid id, string uf);

    Task<bool> DeleteAsync(Guid id);
}

