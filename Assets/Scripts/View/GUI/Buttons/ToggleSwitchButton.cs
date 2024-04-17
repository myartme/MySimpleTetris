using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using View.GUI.Scheme.ColorStyleWrappers;
using View.Scene;

namespace View.GUI.Buttons
{
    public class ToggleSwitchButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject handle;
        [SerializeField] private GameObject fill;
        
        private Toggle _toggle;
        private RectTransform _handleTransform;
        private ColorImageWrapper _handleColorWrapper;
        private ColorImageWrapper _fillColorWrapper;
        private float _swapX;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _fillColorWrapper = fill.GetComponent<ColorImageWrapper>();
            _handleColorWrapper = handle.GetComponent<ColorImageWrapper>();
            _handleTransform = handle.GetComponent<RectTransform>();
            _swapX = _handleTransform.rect.width / 2;
        }

        private void Start()
        {
            CheckToggle();
            _handleColorWrapper.ColorElementType = ColorElementType.Main;
        }
        
        public IEnumerator SetToggle(bool isToggleOn)
        {
            yield return new WaitWhile(() => _toggle == null);
            _toggle.isOn = isToggleOn;
            CheckToggle();
        }

        public void ToggleEnable()
        {
            _fillColorWrapper.ColorElementType = ColorElementType.Main;
            _handleTransform.localPosition = new Vector3(_swapX, _handleTransform.localPosition.y);
        }

        public void ToggleDisable()
        {
            _fillColorWrapper.ColorElementType = ColorElementType.Shadow;
            _handleTransform.localPosition = new Vector3(-_swapX, _handleTransform.localPosition.y);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            CheckToggle();
        }
        
        private void CheckToggle()
        {
            if (_toggle.isOn)
            {
                ToggleEnable();
            }
            else
            {
                ToggleDisable();
            }
        }
    }
}