# Carglass.DivisorPrime

Carglass.DivisorPrime é um projeto desenvolvido para calcular divisores e divisores primos de números inteiros fornecidos pelo usuário. Ele é composto por uma API desenvolvida em .NET 8 e um cliente console que consome essa API.

## 🔧️ Tecnologias utilizadas
- **ASP.NET Core 8**: Para o desenvolvimento da API.
- **xUnit**: Para testes unitários.
- **Moq**: Para mocks em testes.
- **Middleware**: Para logging de requisições.
- **Dependency Injection**: Configuração robusta para gerenciar dependências.

## 📁 Estrutura do Projeto
```plaintext
Carglass
├── Backend
│   ├── Carglass.DivisorPrime.Api
│   │   ├── Controllers
│   │   │   └── DivisorController.cs
│   │   ├── Middlewares
│   │   │   └── RequestLoggingMiddleware.cs
│   ├── Carglass.DivisorPrime.Domain
│   │   ├── Dtos
│   │   │   └── DivisorDto.cs, DivisorsDto.cs, PrimeDivisorDto.cs, ResponseDto.cs
│   │   ├── Enums
│   │   │   └── ApiHttpStatusCode.cs
│   ├── Carglass.DivisorPrime.Service
│   │   ├── Services
│   │   │   └── DivisorService.cs
│   ├── Carglass.DivisorPrime.Mappers
│   │   ├── Builders
│   │   │   └── ResponseBuilder.cs
```

## 📌 Funcionalidades
- **GET /api/Divisor/{number?}**
  - Retorna divisores e divisores primos de um número.
  - Exemplo:
    ```json
    {
      "isSuccess": true,
      "message": "Operação realizada com sucesso",
      "statusCode": 200,
      "divisors": {
        "number": 10,
        "divisors": [1, 2, 5, 10],
        "primeDivisors": [2, 5]
      }
    }
    ```

## 🖥️ Como executar o projeto
### Pré-requisitos
- .NET 8 SDK instalado
- Visual Studio ou Visual Studio Code configurado

### Passos
1. Clone o repositório:
   ```bash
   git clone <url-do-repositorio>
   cd Carglass/Backend
   ```
2. Restaure as dependências:
   ```bash
   dotnet restore
   ```
3. Execute a API:
   ```bash
   dotnet run --project Carglass.DivisorPrime.Api
   ```
4. Acesse a aplicação:
   - API: [http://localhost:5000/api/Divisor/{number}](http://localhost:5000/api/Divisor/{number})

## 🤦️‍♂️ Testes
Para rodar os testes:
```bash
dotnet test
```

## 🖋️ Contribuição
1. Faça um fork do repositório.
2. Crie uma branch para sua feature:
   ```bash
   git checkout -b minha-feature
   ```
3. Submeta um Pull Request.

## 🔖 Licença
Este projeto está licenciado sob a MIT License.
