using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class NgrokService
{
    private Process _ngrokProcess;
    private readonly HttpClient _httpClient = new HttpClient();

    public void StartNgrok()
    {
        try
        {
            _ngrokProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ngrok",
                    Arguments = "http 5000",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            _ngrokProcess.Start();
            Console.WriteLine("🔹 Ngrok iniciado automaticamente!");

            // Espera alguns segundos antes de buscar a URL
            Task.Delay(3000).Wait();

            // Obtém a URL do ngrok e exibe no console
            GetNgrokUrl().Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao iniciar ngrok: {ex.Message}");
        }
    }

    public void StopNgrok()
    {
        if (_ngrokProcess != null && !_ngrokProcess.HasExited)
        {
            _ngrokProcess.Kill();
            Console.WriteLine("🔹 Ngrok encerrado.");
        }
    }

    public async Task GetNgrokUrl()
    {
        try
        {
            var response = await _httpClient.GetStringAsync("http://localhost:4040/api/tunnels");
            using var doc = JsonDocument.Parse(response);
            var root = doc.RootElement;

            var ngrokUrl = root.GetProperty("tunnels")[0].GetProperty("public_url").GetString();
            Console.WriteLine($"🚀 Ngrok está rodando em: {ngrokUrl}/webhook");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao obter a URL do ngrok: {ex.Message}");
        }
    }
}
