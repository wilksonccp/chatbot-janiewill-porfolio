using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/clientes")]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _repository;

    public ClienteController(IClienteRepository repository)
    {
        _repository = repository;
    }

    // 🔹 Endpoint para salvar ou atualizar um cliente
    [HttpPost]
    public IActionResult SalvarCliente([FromBody] ClienteRequest request)
    {
        _repository.SalvarCliente(request.NumeroWhatsApp, request.Nome, request.EstadoAtendimento);
        return Ok(new { message = "Cliente salvo com sucesso!" });
    }

    // 🔹 Endpoint para obter um cliente pelo número de WhatsApp
    [HttpGet("{numero}")]
    public IActionResult ObterCliente(string numero)
    {
        var cliente = _repository.ObterCliente(numero);
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado." });

        return Ok(cliente);
    }
    // 🔹 Endpoint para remover um cliente do banco
    [HttpDelete("{numero}")]
    public IActionResult RemoverCliente(string numero)
    {
        var cliente = _repository.ObterCliente(numero);
        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado." });

        _repository.RemoverCliente(numero);

        return Ok(new { message = "Cliente removido com sucesso!" });
    }

}

// 🔹 Classe DTO para capturar dados da requisição
public class ClienteRequest
{
    public string NumeroWhatsApp { get; set; }
    public string Nome { get; set; }
    public string EstadoAtendimento { get; set; }
}
