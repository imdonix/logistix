namespace Networking.Core
{
    public interface IEngineStatus
    {
        int GetPendingCount();
        bool IsRequestPendig();
    }
}
