using zTools.DTO.MailerSend;
using System.Threading.Tasks;

namespace zTools.Domain.Services.Interfaces
{
    public interface IMailerSendService
    {
        Task<bool> Sendmail(MailerInfo email);
    }
}
