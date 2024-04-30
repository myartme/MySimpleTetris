using UnityEngine;

namespace View.GUI.Tabs
{
    public class TabBody : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }
        
        public virtual void SetActive(bool isActive)
        {
            CanvasGroup.alpha = isActive ? 1 : 0;
            CanvasGroup.blocksRaycasts = isActive;
            if (isActive)
            {
                UpdateValues();
            }
        }
        
        protected virtual void UpdateValues(){}
    }
}