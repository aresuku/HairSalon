using Models;
namespace Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetFormattedServicesAsync(IEnumerable<Service> service);
        string GetServiceInfo();
    }
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Displayinfo { get; set; }
    }
    public class ServiceService : IServiceService
    {
        private readonly IConfiguration Configuration;
        public ServiceService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Task<IEnumerable<ServiceDto>> GetFormattedServicesAsync(IEnumerable<Service> Service)
        {
            var AppName = Configuration["AppSettings:AppName"];
            var AppVersion = Configuration["AppSettings:Version"];
            var MaxItems = Configuration.GetValue<int>("AppSettings:MaxItems");
            var FormattedServices = Service
                .OrderBy(s => s.ServiceId)
                .Take(MaxItems)
                .Select(s => new ServiceDto
                {
                    Id = s.ServiceId,
                    Name = s.Name,
                    Price = s.Price,
                    Description = s.Description,
                    Displayinfo = $"{s.ServiceId}|{s.Name} [{s.Price}], {s.Description}"
                }
                );
            return Task.FromResult(FormattedServices);            
        }
        public string GetServiceInfo()
        {
            return $"ServiceService is running. Processed by: {Configuration["AppSettings:AppName"]}";
        }
    }    
}
