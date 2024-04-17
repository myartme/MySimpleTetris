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
        [SerializeField] private TableElementField tableElementField;
        [SerializeField] private ColorTheme colorTheme;

        private List<TableElementField> _elemList;

        private void Awake()
        {
            _elemList = new List<TableElementField>();
        }

        public void CreateElementList(int value, bool selectElement = false)
        {
            var element = Instantiate(tableElementField, gameObject.transform);
            element.UpdateCountText(value);
            var colorTextWrapper = element.GetComponent<ColorTextWrapper>();
            colorTextWrapper.ColorTheme = colorTheme;
            colorTextWrapper.ColorElementType = selectElement 
                ? ColorElementType.Active 
                : ColorElementType.Main;
            
            _elemList.Add(element);
        }
    }
}