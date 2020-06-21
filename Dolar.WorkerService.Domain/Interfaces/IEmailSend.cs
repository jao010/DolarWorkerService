using System.Threading.Tasks;

namespace Dolar.WorkerService.Domain.Interfaces
{
    public interface IEmailSend
    {
        Task SendEmailAsync(decimal dolarAntes, decimal dolarVlrNovo);
    }
}
