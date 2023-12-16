using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.GUI.Buttons
{
    public class ResourcesButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] public Image _image;
        [SerializeField] public AudioSource _audioSource;
        [SerializeField] public Sprite _normal, _pressed;
        [SerializeField] public AudioClip _compressClip, _uncompressClip;

        private void Awake()
        {
            _image.sprite = _normal;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _image.sprite = _normal;
            _audioSource.PlayOneShot(_uncompressClip);
        }
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _image.sprite = _pressed;
            _audioSource.PlayOneShot(_compressClip);
        }
    }
}
