using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class TwilioService
{
    private readonly HttpClient _httpClient = new HttpClient();

    private const string AccountSid = "meu account sid";
    private const string AuthToken = "MY_AUTH_TOKEN"; 
    private const string TwilioPhoneNumber = "whatsapp:+14155238886"; // Ex: whatsapp:+14155238886
    private const string WebhookUrlBase = "https://api.twilio.com/2010-04-01/Accounts/";

    public async Task AtualizarWebhook(string ngrokUrl)
{
    try
    {
        var urlWebhook = $"{ngrokUrl}/webhook"; // 🔹 Adiciona "/webhook" na URL do ngrok
        Console.WriteLine($"🔹 Tentando atualizar o webhook para: {urlWebhook}");

        var requestUrl = "https://sandbox.twilio.com/v1/Webhooks/WhatsApp";

        var requestBody = new StringContent($"WebhookUrl={urlWebhook}&WebhookMethod=POST", Encoding.UTF8, "application/x-www-form-urlencoded");

        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", 
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{AccountSid}:{AuthToken}")));

        var response = await _httpClient.PostAsync(requestUrl, requestBody);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"✅ Webhook atualizado no Twilio Sandbox: {urlWebhook}");
        }
        else
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"❌ Erro ao atualizar o webhook no Twilio: {response.StatusCode}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao atualizar webhook no Twilio: {ex.Message}");
    }
}

}
