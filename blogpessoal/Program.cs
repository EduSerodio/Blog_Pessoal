using System.Text;
using blogpessoal.Configuration;
using blogpessoal.Data;
using blogpessoal.Model;
using blogpessoal.Security;
using blogpessoal.Security.Implements;
using blogpessoal.Service;
using blogpessoal.Service.Implements;
using blogpessoal.Validator;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
namespace blogpessoal
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                });
            });
            // Add services to the container.
            //configuração para não criar um loop infinito no JASON quando fizermos requisição
            builder.Services.AddControllers()
                .AddNewtonsoftJson(
                    Options =>
                    {
                        Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        Options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    }
                );

            // CONEXAO COM O BANCO DE DADOS - NEW
            if(builder.Configuration["Enviroment:Start"] == "PROD")
            {
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("secrets.json");

                var connectionString = builder.Configuration.GetConnectionString("ProdConnection");
                builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
            }
            else
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            }

            // registrar validações do banco de dados -NEW
            builder.Services.AddTransient<IValidator<Postagem>, PostagemValidator>();
            builder.Services.AddTransient<IValidator<Tema>, TemaValidator>();
            builder.Services.AddTransient<IValidator<User>, UserValidator>();

            //Registrar as classes de serviço (SERVICE)
            builder.Services.AddScoped<IPostagemService, PostagemService>();
            builder.Services.AddScoped<ITemaService, TemaService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        }).AddJwtBearer(options =>
                        {
                            var key = Encoding.UTF8.GetBytes(Settings.Secret);
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(key)
                            };
                        });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Configuração do Swagger
            builder.Services.AddSwaggerGen(options => 
            {   //configuração do projeto e da pessoa desenvolvedora
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Projeto Blog Pessoal",
                    Description = "Projeto Blog Pessoal - APS.NET Core 7.0",
                    Contact = new OpenApiContact
                    {
                        Name = "Eduardo Serodio Souto",
                        Email = "eduardo.serodio@outlook.com",
                        Url = new Uri("https://github.com/EduSerodio")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GitHub",
                        Url = new Uri("https://github.com/EduSerodio")
                    }
                });
                //configuração de segurança do swagger
                options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Digite um Token JWT válido",
                    Name = "Autorizathion",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //Adicionar a indicação de endpoint protegido
                options.OperationFilter<AuthResponsesOperationFilter>();
            });

            //Adicionar o Fluent Validation no Swagger
            builder.Services.AddFluentValidationRulesToSwagger();

            var app = builder.Build();

            //criar o banco de dados e as tabelas
            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
                app.UseSwagger();
                
            // Swagger como página inicial na NUVEM
            if(app.Environment.IsProduction())
            {
                app.UseSwaggerUI(options => 
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Pessoal - v1");
                    options.RoutePrefix = String.Empty;

                });
            }

            //app.UseHttpsRedirection();

            //Inicializa o CORS  - NEW
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
            app.Run();
        }
    }
}
