
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MordidaCerta.WebAPI.BdContextLanches;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LanchesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IComidaRepository, ComidaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            //valida quem está solicitando
            ValidateIssuer = true,

            //valida quem está recebendo
            ValidateAudience = true,

            //define se o tempo de expiração será validado
            ValidateLifetime = true,

            //forma de criptografia e valida a chave de autenticação
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Pratos-chave-autenticacao-webapi-dev")),

            //valida o tempo de expiração do token
            ClockSkew = TimeSpan.FromMinutes(5),

            //nome do issuer (de onde está vindo)
            ValidIssuer = "api_Pratos",

            //nome do audience (para onde está indo
            ValidAudience = "api_Pratos"
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "Uma API com um catálogo de Pratos de Comida",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "diogodev",
            Url = new Uri("https://github.com/diogo2p2e5")
        },
        License = new Microsoft.OpenApi.OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT:"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Adiciona serviço de Controllers
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

// Adiciona o mapeamento de Controllers
app.MapControllers();

app.Run();
