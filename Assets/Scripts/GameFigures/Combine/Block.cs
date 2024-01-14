using System.Collections;
using Service;
using UnityEngine;

namespace GameFigures.Combine
{
    public class Block : MonoBehaviour, IManageable, IColorable
    {
        [SerializeField] private Vector3Int position;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private const float _AnimationDelay = 0.05f;
        public string Name => gameObject.name;
        public ObjectStatus Status { get; set; }
        
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

        public bool SetAsCreated()
        {
            Status = ObjectStatus.Created;
            return true;
        }

        public bool SetAsPreview()
        {
            if(Status != ObjectStatus.Created) return false;
            
            Status = ObjectStatus.Preview;
            return true;
        }

        public bool SetAsReady()
        {
            if(Status != ObjectStatus.Preview) return false;
            
            Status = ObjectStatus.Active;
            return true;
        }

        public bool SetAsMakeComplete()
        {
            if(Status != ObjectStatus.Active) return false;
            
            Status = ObjectStatus.MakeComplete;
            return true;
        }

        public bool SetAsCompleted()
        {
            if(Status != ObjectStatus.MakeComplete) return false;
            
            Status = ObjectStatus.Completed;
            return true;
        }

        public void DropMeDown(int positions)
        {
            Position += Vector3.down * positions;
            transform.position += Vector3.down * positions;
        }

        public void VanishMe(int index)
        {
            StartCoroutine(WaitUntilAnimation(index));
        }
        
        public void DestroyMe()
        {
            Destroy(gameObject);
        }
        
        private IEnumerator WaitUntilAnimation(int index)
        {
            yield return new WaitForSeconds(_AnimationDelay * index);
            _animator.SetTrigger("Vanish");
            Status = ObjectStatus.Destroyed;
        }
    }
}
