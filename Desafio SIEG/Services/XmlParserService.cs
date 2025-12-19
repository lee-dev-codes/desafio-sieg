using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Desafio_SIEG.Models;

namespace Desafio_SIEG.Services;

public class XmlParserService : IXmlParserService
{
    public FiscalDocument Parse(string xml)
    {
        var xDoc = XDocument.Parse(xml);
        var tipo = DetectTipo(xDoc);

        string? cnpj = xDoc.Descendants()
            .FirstOrDefault(x => x.Name.LocalName == "CNPJ")?.Value;

        string? uf = xDoc.Descendants()
            .FirstOrDefault(x => x.Name.LocalName == "UF")?.Value;


        return new FiscalDocument
        {
            Id = Guid.NewGuid(),
            Tipo = tipo,
            Chave = Guid.NewGuid().ToString(),
            CnpjEmitente = cnpj ?? "N/A",
            UfEmitente = uf ?? "N/A",
            DataEmissao = DateTime.UtcNow,
            XmlOriginal = xml,
            XmlHash = GenerateHash(xml)
        };
    }

    public string GenerateHash(string xml)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(xml));
        return Convert.ToHexString(bytes);
    }

    private string DetectTipo(XDocument xDoc)
    {
        if (xDoc.Descendants().Any(x => x.Name.LocalName == "NFe"))
            return "NFe";

        if (xDoc.Descendants().Any(x => x.Name.LocalName == "CTe"))
            return "CTe";

        if (xDoc.Descendants().Any(x => x.Name.LocalName == "NFSe"))
            return "NFSe";

        return "Desconhecido";
    }
}