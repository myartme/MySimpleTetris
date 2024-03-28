using UnityEngine;
using UnityEngine.EventSystems;
using View.GUI.Scheme.ColorStyleWrappers;

namespace View.GUI.Tabs
{
    public class TabHead : MonoBehaviour, IPointerClickHandler
    {
        public TabGroup TabGroup;
        public TabBody TabBody;
        public ColorTextWrapper TabLabel;

        public ColorImageWrapper TabWrapper { get; private set; }
        
        private void Awake()
        {
            TabWrapper = GetComponent<ColorImageWrapper>();
        }

        private void Start()
        {
            TabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TabGroup.OnTabClick(this);
        }
    }
}