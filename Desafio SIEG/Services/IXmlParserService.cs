using Desafio_SIEG.Models;

namespace Desafio_SIEG.Services

{
    public interface IXmlParserService
    {
        FiscalDocument Parse(string xml);
        string GenerateHash(string xml);
    }
}
