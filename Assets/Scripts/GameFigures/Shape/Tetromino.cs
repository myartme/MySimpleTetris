using System;
using Service;
using UnityEngine;

namespace GameFigures.Shape
{
    public class Tetromino : GeometricShape, IManageable
    {
        public event Action<Tetromino> OnChangeStatus;
        
        private Shadow _shadow;

        public ObjectStatus Status { get; private set; }
        public Bounds Bounds { get; private set; }

        public override Vector3 Position
        {
            get => gameObject.transform.position;
            set
            {
                gameObject.transform.position = value;
                UpdateChildrenPosition();
                if (_shadow.IsActive)
                {
                    UpdateShadowPosition();
                }
            }
        }

        public override int Rotation
        {
            get => combineObject.AngleRotation;
            set
            {
                combineObject.AngleRotation = value;
                UpdateChildrenPosition();
                if (_shadow.IsActive)
                {
                    UpdateShadowPosition();
                }
            }
        }

        public int NextRotation 
            => Rotation == 0 ? 3 : Rotation - 1;
        public int PrevRotation 
            => Rotation == 3 ? 0 : Rotation + 1;

        public Tetromino(Sprite blocksSprite, BlockType blockType) : base(blocksSprite, blockType)
        {
            Name = $"Tetromino {blockType}";
            _shadow = new Shadow(blocksSprite, blockType);
            Bounds = BoundsInfo.GetAllBoundsCollapse(gameObject);
        }
        
        private void UpdateChildrenPosition()
        {
            combineObject.ForEach(item =>
            {
                item.Position = Position + item.transform.localPosition;
            });
        }
        
        private void UpdateShadowPosition()
        {
            _shadow?.UpdatePositionAndRotation(Position, Rotation, Children);
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
            _shadow.SetupAsActive(Position, Rotation, Children);
            OnChangeStatus?.Invoke(this);
        }
        
        public void SetAsMakeComplete()
        {
            Status = ObjectStatus.MakeComplete;
            _shadow.DestroyMe();
            OnChangeStatus?.Invoke(this);
        }

        public void SetAsCompleted()
        {
            Status = ObjectStatus.Completed;
            OnChangeStatus?.Invoke(this);
        }
    }
}