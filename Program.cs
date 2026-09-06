// Importar OpenApi
using Microsoft.OpenApi;

// Cria e configura o objeto `WebApplicationBuilder`, que é usado para configurar os serviços e o pipeline de solicitação HTTP.
var builder = WebApplication.CreateBuilder(args);

// Configura os serviços necessários para a aplicação. 
// `AddControllers` adiciona suporte para controllers MVC, o que permite criar endpoints de API e renderizar views.
builder.Services.AddControllers();

// Configura o Swagger
// AddEndpointsApiExplorer: permite a geração automática de documentação para endpoints da API.
builder.Services.AddEndpointsApiExplorer();

// AddSwaggerGen: adiciona os serviços necessários para gerar a documentação do Swagger.
builder.Services.AddSwaggerGen(c =>
{
    // Define um documento Swagger chamado "v1"
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Minha API", // Título da API
        Version = "v1",      // Versão da API
        Description = "Uma API para gerenciar um CRUD de pessoas." // Descrição da API
    });
}); 

// Obtém a string de conexão para o banco de dados a partir da configuração do aplicativo.
// Se a string de conexão 'DefaultConnection' não for encontrada, uma exceção é lançada para indicar que a configuração está ausente.
var stringDeConexao = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");

// Registra o repositório `PessoaRepositorio` como um serviço singleton no contêiner de injeção de dependência.
// Um singleton garante que apenas uma instância do repositório será criada e usada em toda a aplicação.
builder.Services.AddSingleton(new PessoaRepositorio(stringDeConexao));

// Constrói o objeto `WebApplication` com base nas configurações fornecidas e no contêiner de serviços configurado.
var app = builder.Build();

// Configura o Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Define a rota onde o Swagger JSON estará disponível
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
    
    // Define a rota do Swagger UI como a raiz do aplicativo
    c.RoutePrefix = "swagger"; // Assim, você acessa o Swagger UI em http://localhost:<porta>
});

// Mapeia os controllers para os endpoints disponíveis na aplicação.
// Isso faz com que os controllers sejam acessíveis através das rotas definidas nas suas classes de controller.
app.MapControllers();

// Inicia a aplicação e começa a escutar as solicitações HTTP.
app.Run();