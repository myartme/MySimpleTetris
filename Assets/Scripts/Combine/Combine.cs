using System.Collections.Generic;
using Engine;
using Unity.VisualScripting;
using UnityEngine;

namespace Combine
{
    public class Combine<T> : List<T> where T : MonoBehaviour, ICombinable
    {
        public Sprite BlockSprite;
        public BlockType BlockType;
        
        private GameObject _parent;
        private int _angleRotation;

        public Combine(Sprite blockSprite, BlockType blockType, GameObject parent)
        {
            BlockSprite = blockSprite;
            BlockType = blockType;
            _parent = parent;
            Initialize();
        }

        public void SetChildrenColor(Color32 color)
        {
            ForEach(item => item.Color = color);
        }

        private void Initialize()
        {
            var coordinates = BlockCoordinates.Coordinates[BlockType];
            for (var i = 0; i < coordinates.Length; i++)
            {
                Add(CreateObject($"Block{i + 1}", coordinates[i]));
            }

            SetChildrenColor(BlockColors.Colors[BlockType]);
        }

        private T CreateObject(string name, Vector3 position)
        {
            var obj = new GameObject(name).AddComponent<SpriteRenderer>().AddComponent<T>();
            obj.GetComponent<SpriteRenderer>().sprite = BlockSprite;
            obj.gameObject.transform.position = position;
            obj.gameObject.transform.SetParent(_parent.transform);
            return obj;
        }
    }
}