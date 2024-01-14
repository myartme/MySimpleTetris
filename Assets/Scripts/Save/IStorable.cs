using JetBrains.Annotations;

namespace Save
{
    public interface IStorable<T>
    {
        public bool IsExists { get; }
        public void Create();
        public void Save(T data);
        [CanBeNull] public T Load();
    }
}