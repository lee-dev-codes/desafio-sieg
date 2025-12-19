using Desafio_SIEG.Data;
using Desafio_SIEG.DTOs;
using Desafio_SIEG.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_SIEG.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _service;

    public DocumentsController(IDocumentService service)
    {
        _service = service;
    }

    [HttpGet] // GET para listar todos os documentos
    public async Task<IActionResult> GetAll(
        int page = 1,
        int pageSize = 10,
        string? cnpj = null,
        string? uf = null,
        DateTime? dataInicio = null,
        DateTime? dataFim = null)
    {
        var result = await _service.GetAllAsync(
            page, pageSize, cnpj, uf, dataInicio, dataFim);

        return Ok(result);
    }

    [HttpGet("{id:guid}")] // GET para listar documento especifico 
    public async Task<IActionResult> GetById(Guid id)
    {
        var doc = await _service.GetByIdAsync(id);
        return doc == null ? NotFound() : Ok(doc);
    }

    [HttpPost("upload")] // POST para fazer upload do XML
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido");

        using var reader = new StreamReader(file.OpenReadStream());
        var xml = await reader.ReadToEndAsync();

        try
        {
            var id = await _service.CreateAsync(xml);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
        catch (InvalidOperationException)
        {
            return Conflict("Documento já processado");
        }
    }

    [HttpPut("{id:guid}")] // PUT para atualizar informações do documento
    public async Task<IActionResult> Update(Guid id, UpdateDocumentDto dto)
    {
        var success = await _service.UpdateUfAsync(id, dto.UfEmitente);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")] // DELETE para excluir documento
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
