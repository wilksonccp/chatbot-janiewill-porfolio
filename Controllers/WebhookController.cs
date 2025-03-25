using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML;

[Route("webhook")]
[ApiController]
public class WebhookController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    public WebhookController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpPost]
    public IActionResult ReceiveMessage([FromForm] string Body, [FromForm] string From)
    {
        Console.WriteLine($"📲 Mensagem recebida de: {From}");
        Console.WriteLine($"🔹 Conteúdo da mensagem: {Body}");   

        var response = new MessagingResponse();
        string mensagemCliente = Body.Trim().ToLower();

        var cliente = _clienteRepository.ObterCliente(From);

        if (!cliente.HasValue)
        {
            _clienteRepository.SalvarCliente(From, "", "pedir_nome");
            response.Message("Oi, bem vinda! 🌸 Sou a assistente virtual da Jani e estou aqui para te ajudar. \n\n"
                            + "Antes da gente começar, me diz seu nome? 😊\n\n");
        }
        else
        {
            var (nome, estado) = cliente.Value;

            if (estado == "pedir_nome")
            {
                _clienteRepository.SalvarCliente(From, mensagemCliente, "confirmar_nome");
                response.Message($"Seu nome é *{mensagemCliente}*, está correto? (Responda 'Sim' ou 'Não')");
            }
            else if (estado == "confirmar_nome")
            {
                if (mensagemCliente == "sim")
                {
                    _clienteRepository.SalvarCliente(From, nome, "menu_cliente");
                    response.Message($"Ótimo, *{nome}*! Agora me diga:\n\n"
                                    + "1️⃣ É sua primeira compra.\n"
                                    + "2️⃣ Já sou cliente.");
                }
                else if (mensagemCliente == "não" || mensagemCliente == "nao")
                {
                    _clienteRepository.SalvarCliente(From, "", "pedir_nome"); // 🔹 Limpa o nome antes de pedir de novo
                    response.Message("Sem problemas! Me diga novamente o seu nome. 😊");
                }
                else
                {
                    response.Message("Por favor, responda apenas 'Sim' ou 'Não'. Está correto o nome?");
                }
            }

            else if (estado == "menu_cliente")
            {
                if (mensagemCliente == "1")
                {
                    _clienteRepository.SalvarCliente(From, nome, "novo_cliente");
                    response.Message("🎉 Que legal! Vou te apresentar nossos produtos e promoções.");
                }
                else if (mensagemCliente == "2")
                {
                    _clienteRepository.SalvarCliente(From, nome, "cliente_existente");
                    response.Message("🛍️ Que bom te ver de novo! Como posso te ajudar hoje?");
                }
                else
                {
                    response.Message("Por favor, escolha uma opção válida:\n"
                                    + "1️⃣ É sua primeira compra.\n"
                                    + "2️⃣ Já sou cliente.");
                }
            }
            else
            {
                if (From == "whatsapp:+558888850021")
                {
                    _clienteRepository.RemoverCliente(From);
                    response.Message("👀 Ainda não tenho uma resposta pra esse estágio... Reiniciei seu atendimento pra você testar tudo de novo! 🚀");
                }
                else
                {
                    response.Message("⚠️ Opa! Ainda não estou preparada pra esse estágio do atendimento. Em breve teremos novidades! 😉");
                }
            }
        }
        Console.WriteLine("🔹 XML de resposta enviado ao Twilio:");
        Console.WriteLine(response.ToString());
        // ✅ Garante que sempre retorna um resultado válido para a API
        return new ContentResult
        {
            Content = response.ToString(),
            ContentType = "application/xml",
            StatusCode = 200
        };


    }

}
