using System;
using Engine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combine
{
    public class Tetromino : ICombinable
    {
        private GameObject _gameObject;
        private Combine<Block> _combineObject;
        private Vector3 _position;
        private int _angleRotation;
        
        public static event Action<Tetromino> OnChangeStatus;
        public event Action OnTransform;

        public string Name => _gameObject.name;
        public Combine<Block> Children => _combineObject;
        public Vector3 AlignTopPosition { get; private set; }

        public ObjectStatus Status { get; private set; }

        public Vector3 Position
        {
            get => _gameObject.transform.position;
            set
            {
                _gameObject.transform.position = value;
                OnTransform?.Invoke();
            }
        }
        
        public Quaternion Rotation
        {
            get => _gameObject.transform.rotation;
            private set => _gameObject.transform.rotation = value;
        }

        public int AngleRotation
        {
            get => _angleRotation;
            set
            {
                _angleRotation = value;
                Rotation = GetRightAngleZRotation(value);
                OnTransform?.Invoke();
            }
        }

        public Bounds Bounds { get; private set; }

        public Tetromino(Sprite blocksSprite, BlockType blockType)
        {
            OnTransform += UpdateChildrenPosition;
            _gameObject = new GameObject($"Tetromino {blockType}");
            _combineObject = new Combine<Block>(blocksSprite, blockType, _gameObject);
            AngleRotation = Random.Range(0, 4);
            SetAlignTopPosition();
        }
        
        public void SetToPreviewPosition(Vector3 spawnPosition)
        {
            Position = spawnPosition + AlignTopPosition;
            SetAsPreview();
        }

        public void SetToSpawnPosition(Vector3 spawnPosition)
        {
            Position = spawnPosition + AlignTopPosition;
            SetAsReady();
        }

        private void SetAlignTopPosition()
        {
            Bounds = GetAllBoundsCollapse(_gameObject);
            var position = _gameObject.transform.position;
            var pointX = (int)Bounds.size.x % 2 == 0 ? 0.5f : 0f;
            var pointY = Math.Abs(position.y - Bounds.max.y) < 0.5f ? Bounds.min.y + 0.5f : Bounds.max.y - 0.5f;
            AlignTopPosition = new Vector3(
                position.x + pointX - Bounds.center.x,
                position.y - pointY,
                0);
        }
        
        private void UpdateChildrenPosition()
        {
            foreach (var child in _combineObject)
            {
                var childPosWithRotation = _gameObject.transform.rotation * child.transform.localPosition;
                child.Position = Vector3Int.RoundToInt(childPosWithRotation + Position);
            }
        }
        
        private Bounds GetAllBoundsCollapse(GameObject gameObject)
        {
            var bounds = GetObjectBounds(gameObject);
            var childrenTransform = gameObject.transform;

            for (var i = 0; i < childrenTransform.childCount; i++)
            {
                var child = childrenTransform.GetChild(i);
                if (child.childCount == 0)
                {
                    if (i == 0)
                    {
                        bounds = GetObjectBounds(child.gameObject);
                    }
                    else
                    {
                        bounds.Encapsulate(GetObjectBounds(child.gameObject));
                    }
                }
                else
                {
                    bounds.Encapsulate(GetAllBoundsCollapse(child.gameObject));
                }
            }

            return bounds;
        }

        private Bounds GetObjectBounds(GameObject gameObject)
        {
            var renderer = gameObject.GetComponent<Renderer>();
            if(renderer == null)
                return new Bounds(gameObject.transform.position,Vector3.zero);
            
            return renderer.bounds;
        }
        
        public static Quaternion GetRightAngleZRotation(int angleZ)
        {
            return Quaternion.Euler(0, 0, angleZ * 90f);
        }
        
        public void SetAsCreated()
        {
            Status = ObjectStatus.Created;
            OnChangeStatus?.Invoke(this);
        }

        public void SetAsPreview()
        {
            Status = ObjectStatus.Preview;
            OnChangeStatus?.Invoke(this);
        }

        public void SetAsReady()
        {
            Status = ObjectStatus.Active;
            OnChangeStatus?.Invoke(this);
        }

        public void SetAsCompleted()
        {
            Status = ObjectStatus.Completed;
            OnChangeStatus?.Invoke(this);
        }
    }
}