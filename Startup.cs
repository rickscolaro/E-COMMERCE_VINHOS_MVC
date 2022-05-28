using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Models;
using Projeto.Repositories;
using Projeto.Repositories.Interfaces;
using Projeto.Services;

namespace LanchesMac;
public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services) {

        services.AddControllersWithViews();

        // Registrando serviço para popular as tabelas
        services.AddScoped<SeedingService>();


        services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


        // Identity
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


        // Registrando os Repositórios
        // Injetando instancias nos construtores
        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();


        services.AddTransient<IPedidoRepository, PedidoRepository>();




        // Criando usuários e administradores
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddAuthorization(options => {
            options.AddPolicy("Admin",
            politica => {
                politica.RequireRole("Admin");
            });
        });


        //Fornece uma instancia de HttpContextAccessor(uso da Session)
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        //Fornecendo instancia de CarrinhoCompra para ser injetada no construtor em CarrinhoCompraController
        //gerando um carrinho automaticamente quando chamar o controller
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));


        //Registra o uso da Session
        services.AddMemoryCache();
        services.AddSession();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService, ISeedUserRoleInitial seedUserRoleInitial) {

        if (env.IsDevelopment()) {

            app.UseDeveloperExceptionPage();

            seedingService.Seed();// Chamando para popular a base de dados

        } else {

            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();



        // Cria os perfis
        seedUserRoleInitial.SeedRoles();
        // Cria os usuários e atribui aos perfis
        seedUserRoleInitial.SeedUsers();


        // Ativar o Session
        app.UseSession();

        // Identity
        app.UseAuthentication();

        app.UseAuthorization();


        app.UseEndpoints(endpoints => {

            // Novo endpoint de Areas
            endpoints.MapControllerRoute(
                name: "AdminArea",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");




            // Filtra e mostra por categorias  categorias 
            endpoints.MapControllerRoute(name: "categoriaFiltro",
                pattern: "ProdutoRepository/{action}/{categoria?}",
                defaults: new { Controller = "ProdutoRepository", action = "List" });


            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}