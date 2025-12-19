namespace Desafio_SIEG.Models;

public class FiscalDocument
{
    public Guid Id { get; set; }

    public string Tipo { get; set; } = null!;
    public string Chave { get; set; } = null!;
    public string CnpjEmitente { get; set; } = null!;
    public string UfEmitente { get; set; } = null!;

    public DateTime DataEmissao { get; set; }

    public string XmlHash { get; set; } = null!;
    public string XmlOriginal { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
