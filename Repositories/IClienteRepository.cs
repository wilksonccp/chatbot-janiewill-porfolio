public interface IClienteRepository
{
    void SalvarCliente(string numero, string nome, string estadoAtendimento);
    (string Nome, string EstadoAtendimento)? ObterCliente(string numero);
    void RemoverCliente(string numero);
}
