namespace ServiceLifeTime.Services
{
    public interface IScopedService
    {
        string GetGuid();
    }
    public class ScopedService : IScopedService
    {
        private Guid guid;
        public ScopedService()
        {
            guid = Guid.NewGuid();
        }

        public string GetGuid() { return guid.ToString(); }

    }
}

