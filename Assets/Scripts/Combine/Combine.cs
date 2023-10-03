using System.Collections.Generic;
using Service;
using Unity.VisualScripting;
using UnityEngine;

namespace Combine
{
    public class Combine<T> : List<T> where T : MonoBehaviour, ICombinable
    {
        public Sprite BlockSprite;
        public BlockType BlockType;

        public int AngleRotation
        {
            get => _angleRotation;
            set
            {
                _angleRotation = value;

                if (Count > 0)
                {
                    SetChildrenPosition(_angleRotation);
                }
            }
        }

        private GameObject _parent;
        private int _angleRotation;

        public Combine(Sprite blockSprite, BlockType blockType, GameObject parent)
        {
            BlockSprite = blockSprite;
            BlockType = blockType;
            _parent = parent;
            AngleRotation = Random.Range(0, 4);
            Initialize();
        }

        public void SetChildrenColor(Color32 color)
        {
            ForEach(item => item.Color = color);
        }

        public Vector3[] GetChildrenLocalPositionsByRotation(int angleRotation)
        {
            return BlockCoordinates.Coordinates[BlockType][angleRotation];
        }

        private void Initialize()
        {
            var coordinates = BlockCoordinates.Coordinates[BlockType];
            for (var i = 0; i < coordinates[AngleRotation].Length; i++)
            {
                var block = CreateObject($"Block{i + 1}");
                block.gameObject.transform.position = coordinates[AngleRotation][i];
                Add(block);
            }

            SetChildrenColor(BlockColors.Colors[BlockType]);
        }

        private T CreateObject(string name)
        {
            var obj = new GameObject(name).AddComponent<SpriteRenderer>().AddComponent<T>();
            obj.GetComponent<SpriteRenderer>().sprite = BlockSprite;
            obj.gameObject.transform.SetParent(_parent.transform);
            return obj;
        }

        private void SetChildrenPosition(int angleRotation)
        {
            var coordinates = BlockCoordinates.Coordinates[BlockType][angleRotation];
            for (var i = 0; i < coordinates.Length; i++)
            {
                this[i].gameObject.transform.localPosition = coordinates[i];
            }
        }
    }
}