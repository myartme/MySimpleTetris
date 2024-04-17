using System;
using Service;
using UnityEngine;

namespace GameFigures.Shape
{
    public class Tetromino : GeometricShape, IManageable
    {
        public event Action<Tetromino> OnChangeStatus;
        
        private Shadow _shadow;
        private ObjectStatus _status;

        public ObjectStatus Status
        {
            get => _status;
            private set
            {
                _status = value;
                foreach (var child in Children)
                {
                    child.Status = Status;
                }
            }
        }
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
            SetAsCreated();
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
            _shadow?.UpdatePositionAndRotation(this);
        }
        
        public bool SetAsCreated()
        {
            Status = ObjectStatus.Created;
            OnChangeStatus?.Invoke(this);

            return true;
        }

        public bool SetAsPreview()
        {
            if(Status != ObjectStatus.Created) return false;
            
            Status = ObjectStatus.Preview;
            OnChangeStatus?.Invoke(this);

            return true;
        }

        public bool SetAsReady()
        {
            if(Status != ObjectStatus.Preview) return false;
            Status = ObjectStatus.Active;
            _shadow.SetupAsActive(this);
            OnChangeStatus?.Invoke(this);
            
            return true;
        }
        
        public bool SetAsMakeComplete()
        {
            if(Status != ObjectStatus.Active) return false;
            
            Status = ObjectStatus.MakeComplete;
            _shadow.DestroyMe();
            OnChangeStatus?.Invoke(this);

            return true;
        }

        public bool SetAsCompleted()
        {
            if(Status != ObjectStatus.MakeComplete) return false;
            
            Status = ObjectStatus.Completed;
            OnChangeStatus?.Invoke(this);

            return true;
        }
    }
}