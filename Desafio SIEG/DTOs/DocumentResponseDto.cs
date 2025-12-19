namespace Desafio_SIEG.DTOs;

public class DocumentResponseDto
{
    public Guid Id { get; set; }
    public string Tipo { get; set; } = null!;
    public string CnpjEmitente { get; set; } = null!;
    public string UfEmitente { get; set; } = null!;
    public DateTime DataEmissao { get; set; }
}
