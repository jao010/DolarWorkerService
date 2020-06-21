using System.Text;

namespace Dolar.WorkerService.Utils
{
    public static class CreateBodyEmail
    {
        public static string BodyTrocaDeValor(decimal dolarAntes, decimal dolarVlrNovo)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<html>");
            stringBuilder.AppendLine("<head></head>");
            stringBuilder.AppendLine("<body>");
            stringBuilder.AppendLine("<center>");
            stringBuilder.AppendLine("<h1>Monitoramento do Dolar</h1>");
            stringBuilder.AppendLine("<hr>");
            stringBuilder.AppendLine("<h3><p>Valor do Dolar alterado:</p></h3>");
            stringBuilder.AppendLine($"<p>Antes: {dolarAntes}</p>");
            stringBuilder.AppendLine($"<p>Valor novo: {dolarVlrNovo}</p>");
            stringBuilder.AppendLine("</center>");
            stringBuilder.AppendLine("</body>");
            stringBuilder.AppendLine("</html>");

            return stringBuilder.ToString();
        }

    }
}
