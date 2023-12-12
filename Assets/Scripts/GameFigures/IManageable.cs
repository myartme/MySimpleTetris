using Service;

namespace GameFigures
{
    public interface IManageable
    {
        public ObjectStatus Status { get; }
        
        public void SetAsCreated();

        public void SetAsPreview();

        public void SetAsReady();

        public void SetAsMakeComplete();
        public void SetAsCompleted();
    }
}