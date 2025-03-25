using Microsoft.Data.Sqlite;
using System;

public class ClienteRepository : IClienteRepository
{
    private const string ConnectionString = "Data Source=chatbot.db";

    public ClienteRepository()
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Clientes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    NumeroWhatsApp TEXT UNIQUE,
                    Nome TEXT,
                    EstadoAtendimento TEXT
                );
            ";
            command.ExecuteNonQuery();
            Console.WriteLine("Banco de dados SQLite inicializado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar a tabela: {ex.Message}");
        }
    }

    public void SalvarCliente(string numero, string nome, string estadoAtendimento)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Clientes (NumeroWhatsApp, Nome, EstadoAtendimento) 
                VALUES (@numero, @nome, @estado)
                ON CONFLICT(NumeroWhatsApp) 
                DO UPDATE SET Nome = @nome, EstadoAtendimento = @estado;
            ";
            command.Parameters.AddWithValue("@numero", numero);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@estado", estadoAtendimento);
            command.ExecuteNonQuery();

            Console.WriteLine("Cliente salvo com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar cliente: {ex.Message}");
        }
    }
    public void RemoverCliente(string numero)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Clientes WHERE NumeroWhatsApp = @numero;";
            command.Parameters.AddWithValue("@numero", numero);
            command.ExecuteNonQuery();

            Console.WriteLine($"Cliente {numero} removido do banco.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao remover cliente: {ex.Message}");
        }
    }


    public (string Nome, string EstadoAtendimento)? ObterCliente(string numero)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = @"
            SELECT Nome, EstadoAtendimento FROM Clientes WHERE NumeroWhatsApp = @numero;
        ";
            command.Parameters.AddWithValue("@numero", numero);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return (reader.GetString(0), reader.GetString(1));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar cliente: {ex.Message}");
        }

        return null;
    }

}
