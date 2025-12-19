using Desafio_SIEG.Data;
using Desafio_SIEG.DTOs;
using Desafio_SIEG.Services;
using Microsoft.EntityFrameworkCore;

public class DocumentService : IDocumentService
{
    private readonly AppDbContext _context;
    private readonly IXmlParserService _parser;

    public DocumentService(AppDbContext context, IXmlParserService parser)
    {
        _context = context;
        _parser = parser;
    }

    public async Task<IEnumerable<DocumentResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? cnpj,
        string? uf,
        DateTime? dataInicio,
        DateTime? dataFim)
    {
        var query = _context.Documents.AsQueryable();

        if (!string.IsNullOrEmpty(cnpj))
            query = query.Where(d => d.CnpjEmitente == cnpj);

        if (!string.IsNullOrEmpty(uf))
            query = query.Where(d => d.UfEmitente == uf);

        if (dataInicio.HasValue)
            query = query.Where(d => d.DataEmissao >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(d => d.DataEmissao <= dataFim.Value);

        return await query
            .OrderByDescending(d => d.DataEmissao)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DocumentResponseDto
            {
                Id = d.Id,
                Tipo = d.Tipo,
                CnpjEmitente = d.CnpjEmitente,
                UfEmitente = d.UfEmitente,
                DataEmissao = d.DataEmissao
            })
            .ToListAsync();
    }

    public async Task<DocumentResponseDto?> GetByIdAsync(Guid id)
    {
        return await _context.Documents
            .Where(d => d.Id == id)
            .Select(d => new DocumentResponseDto
            {
                Id = d.Id,
                Tipo = d.Tipo,
                CnpjEmitente = d.CnpjEmitente,
                UfEmitente = d.UfEmitente,
                DataEmissao = d.DataEmissao
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(string xml)
    {
        var hash = _parser.GenerateHash(xml);

        if (await _context.Documents.AnyAsync(d => d.XmlHash == hash))
            throw new InvalidOperationException("Documento duplicado");

        var document = _parser.Parse(xml);

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        return document.Id;
    }

    public async Task<bool> UpdateUfAsync(Guid id, string uf)
    {
        var doc = await _context.Documents.FindAsync(id);
        if (doc == null) return false;

        doc.UfEmitente = uf;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var doc = await _context.Documents.FindAsync(id);
        if (doc == null) return false;

        _context.Documents.Remove(doc);
        await _context.SaveChangesAsync();
        return true;
    }
}
