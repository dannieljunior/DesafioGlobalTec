using DesafioGlobalTec.Comum;
using DesafioGlobalTec.Comum.Models;
using DesafioGlobalTec.Repository;
using DesafioGlobalTec.Repository.Comum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobalTec.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = Constantes.SolicitanteToken,
                           ValidAudience = Constantes.GrupoSolicitantes,
                           ClockSkew = TimeSpan.Zero,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constantes.FraseSegura))
                  };
              });

            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.DateFormatString = "dd/MM/yyyy";
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(Constantes.VersaoApi, new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = Constantes.TituloMicroservico,
                    Version = Constantes.VersaoApi,
                    Description = "Api desenvolvida para atender ao desafio Globaltec",                    
                });

                opt.AddSecurityDefinition(
                    "Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Informe 'Bearer ' + token'",
                        Name = "Authorization",
                        Type = "apiKey", 
                    });

                opt.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                });
            });

            services.AddScoped<IRepository<Pessoa>, PessoasRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint($"/swagger/{Constantes.VersaoApi}/swagger.json", Constantes.NomeEndPoint);
                opt.RoutePrefix = string.Empty;
            });
        }
    }

}
