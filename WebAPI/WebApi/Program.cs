
using CasosDeUso.InterfacesCU.CUAuditoria;
using CasosDeUso.InterfacesCU.CUEquipo;
using CasosDeUso.InterfacesCU.CUPago;
using CasosDeUso.InterfacesCU.CUTipoGastos;
using CasosDeUso.InterfacesCU.CUUsuario;
using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.CasosUso.CUAuditoria;
using LogicaAplicacion.CasosUso.CUPago;
using LogicaAplicacion.CasosUso.CUTipoGasto;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //PAGO
            builder.Services.AddScoped<ICUBuscarPago, CUBuscarPago>();
            builder.Services.AddScoped<ICUAltaPagoUnico, CUAltaPagoUnico>();
            builder.Services.AddScoped<ICUAltaPagoRecurrente, CUAltaPagoRecurrente>();
            builder.Services.AddScoped<ICUListadoPago, CUListadoPagos>();
            builder.Services.AddScoped<ICUObtenerPagos, CUObtenerPagos>();

            //LOGIN
            builder.Services.AddScoped<ICULoguinUsuario, CULoguinUsuario>();
            //TIPO GASTO
            builder.Services.AddScoped<ICUListadoTipoGastos, CUListadoTipoGastos>();
            //USUARIO
            builder.Services.AddScoped<ICUModificarContrasenia, CUModificarContrasenia>();

            //EQUIPOS
            builder.Services.AddScoped<ICUEquiposMonto, CUObtenerEquipos>();

            //REPOSITORIOS
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
            builder.Services.AddScoped<IRepositorioTipoGasto, RepositorioTipoGastoEF>();
            builder.Services.AddScoped<IRepositorioEquipo, RepositorioEquipoEF>();
            builder.Services.AddScoped<IRepositorioPago, RepositorioPagoEF>();
            builder.Services.AddScoped<IRepositorioRol, RepositorioRolEF>();
            builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoriaEF>();

            //AUDITORIA

            builder.Services.AddScoped<ICUListadoAuditoriaDTO, CUListadoAuditoriaDTO>();


            string conexion = builder.Configuration.GetConnectionString("CadenaConexion");
            builder.Services.
                AddDbContext<EmpresaContexto>(options => options.UseSqlServer(conexion));

          


            ////Comienza JWT////

            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


 

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token JWT con la palabra Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
