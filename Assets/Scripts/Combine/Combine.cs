using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Combine
{
    public class Combine<T> : List<T> where T : MonoBehaviour, ICombinable
    {
        private BlockCoordinates _blockCoordinates = new ();
        private Sprite _blockSprite;
        private BlockType _blockType;
        private GameObject _parent;
        private int _angleRotation;

        public Combine(Sprite blockSprite, BlockType blockType, GameObject parent)
        {
            _blockSprite = blockSprite;
            _blockType = blockType;
            _parent = parent;
            Initialize();
        }

        private void Initialize()
        {
            var coordinates = _blockCoordinates.GetCoordinates(_blockType);
            for (var i = 0; i < coordinates.Length; i++)
            {
                Add(CreateObject($"Block{i + 1}", coordinates[i]));
            }
        }

        private T CreateObject(string name, Vector3 position)
        {
            var obj = new GameObject(name).AddComponent<SpriteRenderer>().AddComponent<T>();
            obj.GetComponent<SpriteRenderer>().sprite = _blockSprite;
            obj.gameObject.transform.position = position;
            obj.gameObject.transform.SetParent(_parent.transform);
            return obj;
        }
    }
}