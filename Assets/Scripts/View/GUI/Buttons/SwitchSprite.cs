using UnityEngine;
using UnityEngine.EventSystems;

namespace View.GUI.Buttons
{
    public class SwitchSprite : ResourcesButton
    {
        [SerializeField] private Sprite _switchNormal, _switchPressed;

        private bool _isSwitched;

        public override void OnPointerUp(PointerEventData eventData)
        {
            _image.sprite = _isSwitched ? _normal : _switchNormal;
            _audioSource.PlayOneShot(_uncompressClip);
            _isSwitched = !_isSwitched;
        }
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            _image.sprite = _isSwitched ? _pressed : _switchPressed;
            _audioSource.PlayOneShot(_compressClip);
        }
    }
}
