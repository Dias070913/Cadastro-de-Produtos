using ProjetoCadastroDeProdutos.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adicionando a injeção de dependência

builder.Services.AddScoped<UsuarioRepositorio>(); //Repositorio do usuario

builder.Services.AddScoped<ProdutoRepositorio>(); //Repositorio do produto

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
