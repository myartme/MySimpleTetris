using Service;
using UnityEngine;

namespace GameFigures.Combine
{
    public class Block : MonoBehaviour, IManageable, IColorable
    {
        [SerializeField] private Vector3Int position;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        public string Name => gameObject.name;
        public ObjectStatus Status { get; private set; }
        
        public Vector3 Position
        {
            get => position;
            set => position = Vector3Int.RoundToInt(value);
        }

        public int Rotation { get; set; }

        public Color32 Color
        {
            get => _spriteRenderer.color;
            set => _spriteRenderer.color = value;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = gameObject.AddComponent<Animator>();
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/BlockAnimatorController");
        }

        public void SetAsCreated()
        {
            Status = ObjectStatus.Created;
        }

        public void SetAsPreview()
        {
            Status = ObjectStatus.Preview;
        }

        public void SetAsReady()
        {
            Status = ObjectStatus.Active;
        }

        public void SetAsMakeComplete()
        {
            Status = ObjectStatus.MakeComplete;
        }

        public void SetAsCompleted()
        {
            Status = ObjectStatus.Completed;
        }

        public void DropMeDown(int positions)
        {
            Position += Vector3.down * positions;
            transform.position += Vector3.down * positions;
        }

        public void VanishMe()
        {
            _animator.SetTrigger("Vanish");
        }
        
        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}
