using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View.Scene;

namespace View.GUI.Tabs
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private TabHead _defaultTab;
        
        private List<TabHead> _headTabList;
        private TabHead _selectedTab;

        private void Awake()
        {
            _headTabList = new List<TabHead>();
        }

        /*private void OnEnable()
        {
            SelectTab(_defaultTab);
        }*/

        public void CloseGroups()
        {
            SelectTab(_defaultTab);
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
            _selectedTab.TabWrapper.ColorElementType = ColorElementType.Active;
            _selectedTab.TabLabel.ColorElementType = ColorElementType.Active;
            _selectedTab.TabBody.SetActive(true);
            foreach (var tabHead in _headTabList.Where(tabHead => tabHead != _selectedTab))
            {
                SetActive(tabHead.TabBody, false);
                tabHead.TabWrapper.ColorElementType = ColorElementType.Main;
                tabHead.TabLabel.ColorElementType = ColorElementType.Main;
            }
        }

        private void SetActive(TabBody tabBody, bool isActive)
        {
            tabBody.CanvasGroup.alpha = isActive ? 1 : 0;
            tabBody.CanvasGroup.blocksRaycasts = isActive;
        }
    }
}