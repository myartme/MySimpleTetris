using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View.Scene;

namespace View.GUI.Tabs
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private TabHead defaultTab;
        
        private List<TabHead> _headTabList;
        private TabHead _selectedTab;

        private void Awake()
        {
            _headTabList = new List<TabHead>();
        }

        public void ResetGroup()
        {
            SelectTab(defaultTab);
        }

        public void Subscribe(TabHead button)
        {
            _headTabList.Add(button);
        }

        public void Unsubscribe(TabHead button)
        {
            _headTabList.Remove(button);
        }
        
        public void OnTabClick(TabHead button)
        {
            SelectTab(button);
        }

        private void SelectTab(TabHead button)
        {
            _selectedTab = button;
            foreach (var tabHead in _headTabList.Where(tabHead => tabHead != _selectedTab))
            {
                SetActiveHead(tabHead, false);
            }
            SetActiveHead(_selectedTab, true);
        }

        private void SetActiveHead(TabHead tabHead, bool isActive)
        {
            var color = isActive ? ColorElementType.Active : ColorElementType.Main;
            tabHead.TabBody.SetActive(isActive);
            tabHead.TabWrapper.ColorElementType = color;
            tabHead.TabLabel.ColorElementType = color;
        }
    }
}