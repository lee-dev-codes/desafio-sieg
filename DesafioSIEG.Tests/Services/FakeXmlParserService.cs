using Desafio_SIEG.Models;
using Desafio_SIEG.Services;
using System.Security.Cryptography;
using System.Text;

public class FakeXmlParserService : IXmlParserService
{
    public FiscalDocument Parse(string xml)
    {
        return new FiscalDocument
        {
            Id = Guid.NewGuid(),
            Tipo = "NFe",
            Chave = "35191212345678000199550010000000011000000010", // coloquei qualquer coisa apenas pra não ser null, assimn como outros campos
            CnpjEmitente = "12345678000199",
            UfEmitente = "SP",
            DataEmissao = DateTime.UtcNow,
            XmlHash = GenerateHash(xml),
            XmlOriginal = xml
        };
    }

    public string GenerateHash(string xml)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(xml));
        return Convert.ToBase64String(bytes);
    }
}
