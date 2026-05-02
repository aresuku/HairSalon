using Models;
namespace Services
{
    public interface IReceptionService
    {
        Task<IEnumerable<ReceptionDto>> GetFormattedReceptionsAsync(IEnumerable<Reception> reception);
        string GetServiceInfo();
    }
    public class ReceptionDto
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public MasterDto? Master { get; set; }
        public ServiceDto? Service { get; set; }        
        public string Displayinfo { get; set; }
    }
    public class ReceptionService: IReceptionService
    {
        private readonly IConfiguration Configuration;
        public ReceptionService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Task<IEnumerable<ReceptionDto>> GetFormattedReceptionsAsync(IEnumerable<Reception> Reception)
        {
            var AppName = Configuration["AppSettings:AppName"];
            var AppVersion = Configuration["AppSettings:Version"];
            var MaxItems = Configuration.GetValue<int>("AppSettings:MaxItems");
            var FormattedReceptions = Reception
                .OrderBy(r => r.Id)
                .Take(MaxItems)
                .Select(r => new ReceptionDto
                {
                    Id = r.Id,
                    Time = r.Time,                                   
                    Master = r.Master == null ? null : new MasterDto
                    {
                        Id = r.Master.MasterId,
                        FirstName = r.Master.FirstName,
                        LastName = r.Master.LastName,
                        Experience = r.Master.Experience,
                        Gender = r.Master.Gender,
                        Description = r.Master.Description,
                        Displayinfo = $"{r.Master.MasterId} |{r.Master.FirstName} {r.Master.LastName} [{r.Master.Gender}, {r.Master.Experience}]"
                    },
                    Service = r.Service == null ? null : new ServiceDto
                    {
                        Id = r.Service.ServiceId,
                        Name = r.Service.Name,
                        Price = r.Service.Price,
                        Description = r.Service.Description,
                        Displayinfo = $"{r.Service.ServiceId}|{r.Service.Name} [{r.Service.Price}], {r.Service.Description}"                    
                    },
                    Displayinfo = $"{r.Id}|{r.Time:dd.MM.yyyy HH:mm} [Master: {r.Master?.FirstName} {r.Master?.LastName}, Service: {r.Service?.Name}]",
                }
                );
            return Task.FromResult(FormattedReceptions);            
        }
        public string GetServiceInfo()
        {
            return $"ReceptionService is running. Processed by: {Configuration["AppSettings:AppName"]}";
        }
    }    
}
