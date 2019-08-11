using PoisonLogic.Dim;

namespace PoisonLogic.Village.Jobs
{
    public interface IJobWorker 
    {
        string Label { get; }

        bool HasJob { get; }
        string JobId { get; }
        JobManager GetManager();

    }
}
