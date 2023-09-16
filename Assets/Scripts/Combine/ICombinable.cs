using Engine;
using UnityEngine;

namespace Combine
{
    public interface ICombinable
    {
        public string Name { get; }
        public ObjectStatus Status { get; }

        public Vector3 Position { get; set; }

        public void SetAsCreated();

        public void SetAsPreview();

        public void SetAsReady();

        public void SetAsCompleted();
    }
}