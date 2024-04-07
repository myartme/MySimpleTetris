using UnityEngine;

namespace Engine
{
    public class ScreenDetector : MonoBehaviour
    {
        public static Device CurrentDevice { get; private set; }

        private void OnEnable()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                CurrentDevice = Device.Mobile;
            }
            else
            {
                CurrentDevice = Device.Desktop;
            }
        }

        public enum Device
        {
            Desktop,
            Mobile
        }
    }
}