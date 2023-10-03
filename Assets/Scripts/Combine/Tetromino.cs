using System;
using Engine;
using Service;
using UnityEngine;

namespace Combine
{
    public class Tetromino : ICombinable
    {
        protected GameObject GameObject;
        private Combine<Block> _combineObject;
        private Color32 _color;
        public event Action<Tetromino> OnChangeStatus;
        public event Action OnTransform;

        public string Name => GameObject.name;

        public Combine<Block> Children => _combineObject;

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
        
        public int Rotation
        {
            get => _combineObject.AngleRotation;
            set
            {
                _combineObject.AngleRotation = value;
                UpdateChildrenPosition();
                OnTransform?.Invoke();
            }
        }

        public int NextRotation 
            => Rotation == 0 ? 3 : Rotation - 1;
        public int PrevRotation 
            => Rotation == 3 ? 0 : Rotation + 1;
        

        public Color32 Color
        {
            get => _color;
            set
            {
                _color = value;
                _combineObject.SetChildrenColor(_color);
            }
        }

        public Tetromino(Sprite blocksSprite, BlockType blockType)
        {
            GameObject = new GameObject($"Tetromino {blockType}");
            _combineObject = new Combine<Block>(blocksSprite, blockType, GameObject);
            GameObject.transform.SetParent(Game.BoardTransform);
        }
        
        private void UpdateChildrenPosition()
        {
            _combineObject.ForEach(item =>
            {
                item.Position = Position + item.transform.localPosition;
            });
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
            //TODO: после перезагрузки игры тень создается, но когда приходит инвоук от GameGrid, у тени нет ссылки на тетромино.
            new Shadow(this, _combineObject.BlockSprite, _combineObject.BlockType);
            OnChangeStatus?.Invoke(this);
        }

        public void SetAsCompleted()
        {
            Status = ObjectStatus.Completed;
            OnChangeStatus?.Invoke(this);
        }
    }
}