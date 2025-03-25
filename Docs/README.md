# 🤖 Chatbot JanieWill

Este é um projeto de chatbot desenvolvido com **C#**, **.NET** e **Twilio**, voltado para atendimento automatizado via WhatsApp.  
Ele foi originalmente criado para auxiliar a empresa da minha esposa no atendimento a clientes, e agora também faz parte do meu portfólio técnico.

---

## 🚀 Funcionalidades

- Recebimento e resposta automática de mensagens via WhatsApp
- Integração com a API do Twilio
- Organização em camadas: Controllers, Services e Repositories
- Separação de ambientes com múltiplos arquivos de configuração
- Automatização de túnel usando ngrok
- Estrutura pronta para evoluir com autenticação e persistência em banco

---

## 🧱 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [C#](https://learn.microsoft.com/dotnet/csharp/)
- [Twilio API](https://www.twilio.com/)
- [Ngrok](https://ngrok.com/)
- SQLite (versão privada)
- Visual Studio Code

---

## 🗂️ Estrutura de Pastas

```bash
ChatbotTwilio/
├── Config/
│   └── appsettings.example.json
├── Controllers/
├── Repositories/
├── Services/
├── Docs/
├── Program.cs
├── ChatbotTwilio.csproj
└── README.md

📦 Endpoints Disponíveis
POST /api/cliente – Cadastra um novo cliente

POST /api/mensagem – Recebe mensagens do Twilio

GET /api/status – Verifica status do bot (em construção)

🧠 O que aprendi com esse projeto
Gerenciar ambientes (Development, Test, Production)

Separar responsabilidades em Services, Repositories, Controllers

Integrar APIs externas com segurança

Usar Git e GitHub em fluxos reais de trabalho

## ⚙️ Como Executar o Projeto

1. Clone este repositório:

```bash
git clone https://github.com/wilksonccp/chatbot-cleide-portfolio.git