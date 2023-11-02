using Service;

namespace GameFigures.Combine
{
    public interface IStatable
    {
        public ObjectStatus Status { get; }
        
        public void SetAsCreated();

        public void SetAsPreview();

        public void SetAsReady();

        public void SetAsCompleted();
    }
}