using Service;

namespace GameFigures
{
    public interface IManageable
    {
        public ObjectStatus Status { get; }
        
        public bool SetAsCreated();

        public bool SetAsPreview();

        public bool SetAsReady();

        public bool SetAsMakeComplete();
        public bool SetAsCompleted();
    }
}