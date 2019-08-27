using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using YouLearn.Api.Seguranca;
using YouLearn.Domain.Interfaces.Repositorios;
using YouLearn.Domain.Interfaces.Serviços;
using YouLearn.Domain.Servicos;
using YouLearn.Infra.Persistencia.EF;
using YouLearn.Infra.Persistencia.Repositorios;
using YouLearn.Infra.Transacoes;

namespace YouLearn.Api
{
    public class Startup
    {

        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";

        public object JwBearerDefaults { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Adiciona a injecao de dependencia 
            services.AddScoped<YouLearnContext, YouLearnContext>();

            // Servicos para infra e suas transacoes
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // conexao com os SERVICIOS
            services.AddTransient<IServiceUsuario, ServicosUsuarios>();
            services.AddTransient<IServiceCanal, ServicoCanal>();
            services.AddTransient<IServiceVideo, ServicoVideo>();
            services.AddTransient<IServicePlayList, ServicoPlayList>();

            // conexao com os REPOSITORIOS
            services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
            services.AddTransient<IRepositorioCanal, RepositorioCanal>();
            services.AddTransient<IRepositorioVideo, RepositorioVideo>();
            services.AddTransient<IRepositorioPlayList, RepositorioPlayList>();

            // Configuração do TOKEN
            var signingConfiguracao = new SigningConfiguracao();
            services.AddSingleton(signingConfiguracao);

            var tokenConfiguracao = new TokenConfiguracao
            {
                Audience = AUDIENCE,
                Issuer = ISSUER,
                Seconds = int.Parse(TimeSpan.FromDays(1).TotalSeconds.ToString())
            };
            services.AddSingleton(tokenConfiguracao);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfiguracao.SigningCredentials.Key;
                paramsValidation.ValidAudience = tokenConfiguracao.Audience;
                paramsValidation.ValidIssuer = tokenConfiguracao.Issuer;

                // validacao da assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se o token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerancia para acabar o tempo do token ultilizado
                // caso tenha problemas de sicronia entre o tempo exercido atraves das maquinas no processo
                // de comunicação 
                paramsValidation.ClockSkew = TimeSpan.Zero;

            });

            //Ativa o uso do tokenConfiguracao como forma de autorizar o acesso
            // ativa os recursos deste Projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

            });

            // Para todas as requisicoes serem necessaria o token , para um endpoint nao exigir o token deve colocar o [AllowAnonymous]
            // caso remova essa linha , para todas as requisiçoes que precisar de token,deve colocar o atributo [Authorize("Bearer")]
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                   .RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors();
            services.AddHttpContextAccessor();


            // Aplicando documentacao com swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "YouLearn", Version = "V1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YouLearn - v1");
            });

        }
    }
}
