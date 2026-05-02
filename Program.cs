using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
namespace pr_3._4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<HairSalonContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IMasterService, MasterService>();
            builder.Services.AddScoped<IReceptionService, ReceptionService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            var app = builder.Build();

            app.MapGet("/api/masters", async (HairSalonContext context, IMasterService masterService) =>
            {
                var MastersFromDb = await context.Masters.ToListAsync();
                var FormattedMasters = await masterService.GetFormattedMastersAsync(MastersFromDb);
                var Response = new
                {
                    AppVersion = builder.Configuration["AppSettings:Version"],
                    ServiceInfo = masterService.GetServiceInfo(),
                    Data = FormattedMasters
                };
                return Results.Ok(Response);
            })
            .WithName("GetMasters");            

            app.MapGet("/api/services", async (HairSalonContext context, IServiceService serviceService) =>
            {
                var ServicesFromDb = await context.Services.ToListAsync();
                var FormattedServices = await serviceService.GetFormattedServicesAsync(ServicesFromDb);
                var Response = new
                {
                    AppVersion = builder.Configuration["AppSettings:Version"],
                    ServiceInfo = serviceService.GetServiceInfo(),
                    Data = FormattedServices
                };
                return Results.Ok(Response);
            })
            .WithName("GetServices");

            app.MapGet("/api/receptions", async (HairSalonContext context, IReceptionService receptionService) =>
            {
                var ReceptionsFromDb = await context.Receptions
                .Include(r => r.Master)
                .Include(r => r.Service)
                .ToListAsync();
                var FormattedReceptions = await receptionService.GetFormattedReceptionsAsync(ReceptionsFromDb);
                var Response = new
                {
                    AppVersion = builder.Configuration["AppSettings:Version"],
                    ServiceInfo = receptionService.GetServiceInfo(),
                    Data = FormattedReceptions
                };
                return Results.Ok(Response);
            })
            .WithName("GetReceptions");

            app.MapGet("/api/config", (IConfiguration config) =>
            {
                var appSettings = new
                {
                    AppName = config["AppSettings:AppName"],
                    Version = config["AppSettings:Version"],
                    MaxItems = config.GetValue<int>("AppSettings:MaxItems")
                };

                return Results.Ok(appSettings);
            })
            .WithName("GetConfig");

            app.MapPost("/api/services", async (ServiceDto serviceDto, HairSalonContext context) =>
            {
                if (string.IsNullOrWhiteSpace(serviceDto.Name))
                {
                    return Results.BadRequest(new { message = "Название услуги обязательно" });
                }
                if (serviceDto.Price<1)
                {
                    return Results.BadRequest(new { message = "цена обязательна (от 1)" });
                }                
                var newService = new Service
                {
                    Name = serviceDto.Name,
                    Price = serviceDto.Price,
                    Description = serviceDto.Description,
                };
                context.Services.Add(newService);
                await context.SaveChangesAsync();
                return Results.Ok("услуга создана");
            })
            .WithName("CreateService");   
            
            app.Run();
        }
    }
}



