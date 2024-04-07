using UnityEngine;

namespace View.GUI.Tabs
{
    public class TabBody : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        private bool _isInitialized;
        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _isInitialized = true;
        }
        
        private void OnEnable()
        {
            if(!_isInitialized) return;
            UpdateValues();
        }
        
        protected virtual void UpdateValues(){}
    }
}