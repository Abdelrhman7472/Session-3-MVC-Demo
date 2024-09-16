namespace ServiceLifeTime.Services
{
    public interface ISingleToneService
    {
        string GetGuid();
    }
    public class SingleToneService  : ISingleToneService
    {
        private Guid guid;
        public SingleToneService()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid()=>guid.ToString();
        
    }
}
