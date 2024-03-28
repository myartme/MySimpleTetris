using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.GUI.Buttons
{
    public class ResourcesButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] protected Image image;
        [SerializeField] protected AudioSource audioSource;
        [SerializeField] protected Sprite normal;
        [SerializeField] protected Sprite pressed;
        [SerializeField] protected AudioClip compressClip;
        [SerializeField] protected AudioClip uncompressClip;

        private void Awake()
        {
            image.sprite = normal;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            image.sprite = normal;
            audioSource.PlayOneShot(uncompressClip);
        }
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            image.sprite = pressed;
            audioSource.PlayOneShot(compressClip);
        }
    }
}
