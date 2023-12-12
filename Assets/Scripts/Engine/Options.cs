using UnityEngine;

namespace Engine
{
    public class Options : MonoBehaviour
    {
        public static bool IsGameOver = false;
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