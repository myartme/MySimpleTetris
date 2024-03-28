using System.Collections.Generic;
using Engine;
using UnityEngine;
using View.GUI.Scheme.ColorStyleWrappers;
using View.GUI.TextField;
using View.Scene;

namespace View.GUI.Grid
{
    public class TotalTableHorizontalGroup : MonoBehaviour
    {
        [SerializeField] private TableElementField _tableElementField;

        private List<TableElementField> _elemList;

        private void Awake()
        {
            _elemList = new List<TableElementField>();
        }

        public void CreateElementList(int value, bool selectElement = false)
        {
            var element = Instantiate(_tableElementField, gameObject.transform);
            element.UpdateCountText(value);
            if (selectElement)
            {
                element.GetComponent<ColorTextWrapper>().ColorElementType = ColorElementType.Active;
            }
            
            _elemList.Add(element);
        }
    }
}