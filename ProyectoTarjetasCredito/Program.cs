using ProyectoTarjetasCredito.Utils;
using ProyectoTarjetasCredito.Controllers;
using System.Text.Json.Serialization;

namespace ProyectoTarjetasCredito
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            
            CargaDatosClientes.CargarDesdeJson(ClientesController.arbolclienteid, ClientesController.Clientes, ClientesController.tarjetas, ClientesController.tablaHash, ClientesController.Transacciones, ClientesController.colaGestiones, ClientesController.pilaBloqueosTemporales, ClientesController.arbolClienteDPI);


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
