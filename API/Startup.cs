using Aplicacao.Aplicacao.CadastroProcesso;
using Aplicacao.Aplicacao.CadastroResponsavel;
using Aplicacao.Dominio.CadastroProcesso;
using Aplicacao.Dominio.CadastroResponsavel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositorio.Contexto;
using Repositorio.Repositorios;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<ContextoBanco>(options => options.UseNpgsql(Configuration.GetConnectionString("ConexaoBanco")));

            services.AddScoped<IAplicProcesso, AplicProcesso>();
            services.AddScoped<IAplicResponsavel, AplicResponsavel>();
            services.AddScoped<IRepProcessoResponsavel, RepProcessoResponsavel>();
            services.AddScoped<IRepProcesso, RepProcesso>();
            services.AddScoped<IRepResponsavel, RepResponsavel>();
            services.AddScoped<IValidadorProcesso, ValidadorProcesso>();
            services.AddScoped<IValidadorResponsavel, ValidadorResponsavel>();


            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.PropertyNamingPolicy = null;
               });

            services.AddSingleton(Configuration);
            services.AddHttpClient();
            services.AddControllers();


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Informa��es da API",
                    Version = "v1",
                    Description = "GPro info",
                });
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gest�o processos");
            });
        }
    }
}
