namespace ServiceLifeTime.Services
{
    public interface ITransientService
    {
        string GetGuid();
    }
    public class TransientService : ITransientService
    {
        private Guid guid;
        public TransientService()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();

    }
}

