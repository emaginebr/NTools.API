namespace zTools.ACL.Interfaces
{
    public interface IDocumentClient
    {
        Task<bool> validarCpfOuCnpjAsync(string cpfCnpj);
    }
}
