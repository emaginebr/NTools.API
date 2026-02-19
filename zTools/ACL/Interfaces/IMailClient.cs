using zTools.DTO.MailerSend;

namespace zTools.ACL.Interfaces
{
    public interface IMailClient
    {
        Task<bool> SendmailAsync(MailerInfo mail);
        Task<bool> IsValidEmailAsync(string email);
    }
}
