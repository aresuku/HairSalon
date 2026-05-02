using Models;
namespace Services
{
    public interface IMasterService
    {
        Task<IEnumerable<MasterDto>> GetFormattedMastersAsync(IEnumerable<Master> master);
        string GetServiceInfo();
    }
    public class MasterDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Experience { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string Displayinfo { get; set; }
    }
    public class MasterService: IMasterService
    {
        private readonly IConfiguration Configuration;
        public MasterService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Task<IEnumerable<MasterDto>> GetFormattedMastersAsync(IEnumerable<Master> Master)
        {
            var AppName = Configuration["AppSettings:AppName"];
            var AppVersion = Configuration["AppSettings:Version"];
            var MaxItems = Configuration.GetValue<int>("AppSettings:MaxItems");
            var Formattedmasters = Master
                .OrderBy(m => m.MasterId)
                .Take(MaxItems)
                .Select(m => new MasterDto
                {
                    Id = m.MasterId,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Experience = m.Experience,
                    Gender = m.Gender,
                    Description = m.Description,
                    Displayinfo = $"{m.MasterId}|{m.FirstName} {m.LastName} [{m.Gender}, {m.Experience}]"
                }
                );
            return Task.FromResult(Formattedmasters);            
        }
        public string GetServiceInfo()
        {
            return $"MasterService is running. Processed by: {Configuration["AppSettings:AppName"]}";
        }
    }    
}
