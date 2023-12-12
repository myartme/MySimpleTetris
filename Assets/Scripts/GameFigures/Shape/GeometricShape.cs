using Engine;
using GameFigures.Combine;
using Service;
using UnityEngine;

namespace GameFigures.Shape
{
    public abstract class GeometricShape : IColorable
    {
        protected GameObject gameObject;
        protected Combine<Block> combineObject;
        protected Color32 color;
        public string Name
        {
            get => gameObject.name;
            protected set => gameObject.name = value;
        }
        public virtual Vector3 Position {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }
        public virtual int Rotation
        {
            get => combineObject.AngleRotation;
            set => combineObject.AngleRotation = value;
        }
        
        public Combine<Block> Children => combineObject;

        public Color32 Color
        {
            get => color;
            set
            {
                color = value;
                combineObject.SetChildrenColor(color);
            }
        }

        protected GeometricShape(Sprite blocksSprite, BlockType blockType)
        {
            gameObject = new GameObject(blockType.ToString());
            combineObject = new Combine<Block>(blocksSprite, blockType, gameObject);
            gameObject.transform.SetParent(Game.BoardTransform);
        }
    }
}