using Engine;
using UnityEngine;

namespace View.GUI.Buttons
{
    public class MasterSoundSwitchSprite : SwitchSprite
    {
        [SerializeField] private MixerController _mixerController;

        public void MasterIconInit(bool isMasterEnabled)
        {
            if (isMasterEnabled)
            {
                isSwitched = false;
                image.sprite = normal;
            }
            else
            {
                isSwitched = true;
                image.sprite = switchNormal;
            }
        }
        
        private void OnEnable()
        {
            _mixerController.OnIsMasterEnabled += MasterIconInit;
        }

        private void OnDisable()
        {
            _mixerController.OnIsMasterEnabled -= MasterIconInit;
        }
    }
}
