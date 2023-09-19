using System;
using Engine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combine
{
    public class Tetromino : ICombinable
    {
        protected GameObject GameObject;
        protected Combine<Block> CombineObject;
        private int _angleRotation;
        private Shadow _shadow;
        private Color32 _color;

        public static event Action<Tetromino> OnChangeStatus;
        public event Action OnTransform;

        public string Name => GameObject.name;
        public Shadow Shadow => _shadow;

        public Combine<Block> Children => CombineObject;
        public Vector3 AlignTopPosition { get; private set; }

        public ObjectStatus Status { get; private set; }

        public Vector3 Position
        {
            get => GameObject.transform.position;
            set
            {
                GameObject.transform.position = value;
                UpdateChildrenPosition();
                OnTransform?.Invoke();
            }
        }

        public Quaternion Rotation
        {
            get => GameObject.transform.rotation;
            protected set => GameObject.transform.rotation = value;
        }

        public int AngleRotation
        {
            get => _angleRotation;
            set
            {
                _angleRotation = value;
                Rotation = GetRightAngleZRotation(value);
                UpdateChildrenPosition();
                OnTransform?.Invoke();
            }
        }
        
        public Color32 Color
        {
            get => _color;
            set
            {
                _color = value;
                CombineObject.SetChildrenColor(_color);
            }
        }

        public Bounds Bounds { get; private set; }

        public Tetromino(Sprite blocksSprite, BlockType blockType)
        {
            GameObject = new GameObject($"Tetromino {blockType}");
            CombineObject = new Combine<Block>(blocksSprite, blockType, GameObject);
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
            Bounds = GetAllBoundsCollapse(GameObject);
            var position = GameObject.transform.position;
            var pointX = (int)Bounds.size.x % 2 == 0 ? 0.5f : 0f;
            var pointY = Math.Abs(position.y - Bounds.max.y) < 0.5f ? Bounds.min.y + 0.5f : Bounds.max.y - 0.5f;
            AlignTopPosition = new Vector3(
                position.x + pointX - Bounds.center.x,
                position.y - pointY,
                0);
        }
        
        private void UpdateChildrenPosition()
        {
            CombineObject.ForEach(item => 
                {
                    var childPosWithRotation = Rotation * item.transform.localPosition;
                    item.Position = Position + childPosWithRotation;
                });
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
            if(!renderer)
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
            _shadow = new Shadow(this, CombineObject.BlockSprite, CombineObject.BlockType);
            OnChangeStatus?.Invoke(this);
        }

        public void SetAsCompleted()
        {
            Status = ObjectStatus.Completed;
            OnChangeStatus?.Invoke(this);
        }
    }
}