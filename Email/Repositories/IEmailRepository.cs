using Email.Models.Messages;
using System.Threading.Tasks;

namespace Email.Repositories
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
