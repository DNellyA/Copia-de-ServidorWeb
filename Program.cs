using Microsoft.AspNetCore.Http.Json;
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options=>options.SerializerOptions.PropertyNamingPolicy=null);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapGet("/", () => "Hello World!");

app.MapPost("/usuarios/registrar",UsuarioRequestHandler.Registrar);

app.MapPost("/usuarios/acceso",UsuarioRequestHandler.Acceso);

app.MapPost("/usuarios/recuperar",UsuarioRequestHandler.Recuperar);

app.MapGet("/categorias",CategoriasRequestHandler.Listar);

app.MapPost("/categorias/crear",CategoriasRequestHandler.Crear);

app.MapGet("lenguaje/{idCategoria}",LenguajeRequestHandler.ListarRegistros);

app.MapPost("/lenguaje",LenguajeRequestHandler.CrearRegistro);

app.MapDelete("/lenguaje/{id}",LenguajeRequestHandler.Eliminar);

app.MapGet("/lenguaje/buscar",LenguajeRequestHandler.Buscar);

app.Run();