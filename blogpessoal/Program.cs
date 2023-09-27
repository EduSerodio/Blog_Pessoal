using blogpessoal.Data;
using blogpessoal.Model;
using blogpessoal.Service;
using blogpessoal.Service.Implements;
using blogpessoal.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


//Configuração do CORS  - NEW
builder.Services.AddCors(options =>
{   
    options.AddPolicy(name: "MyPolicy",
    policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    } );
});


// Add services to the container.
//configuração para não criar um loop infinito no JASON quando fizermos requisição
builder.Services.AddControllers()
    .AddNewtonsoftJson(
        Options =>
        {
            Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    );

// CONEXAO COM O BANCO DE DADOS - NEW
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext> (options => options.UseSqlServer(connectionString));

// registrar validações do banco de dados -NEW
builder.Services.AddTransient<IValidator<Postagem>, PostagemValidator>();
builder.Services.AddTransient<IValidator<Tema>, TemaValidator>();

//Registrar as classes de serviço (SERVICE)
builder.Services.AddScoped<IPostagemService, PostagemService> ();
builder.Services.AddScoped<ITemaService, TemaService> ();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

//criar o banco de dados e as tabelas
using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Inicializa o CORS  - NEW
app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
