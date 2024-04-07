using UnityEngine;
using UnityEngine.EventSystems;

namespace View.GUI.Buttons
{
    public class SwitchSprite : ResourcesButton
    {
        [SerializeField] protected Sprite switchNormal;
        [SerializeField] protected Sprite switchPressed;
        protected bool isSwitched;
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            image.sprite = isSwitched ? normal : switchNormal;
            audioSource.PlayOneShot(uncompressClip);
            isSwitched = !isSwitched;
        }
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            image.sprite = isSwitched ? pressed : switchPressed;
            audioSource.PlayOneShot(compressClip);
        }
    }
}
