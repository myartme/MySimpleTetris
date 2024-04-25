using Engine;

namespace View.GUI.Buttons
{
    public class MasterSoundSwitchSprite : SwitchSprite
    {
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

        private void Start()
        {
            MasterIconInit(MixerController.Instance.IsMasterEnabled);
        }

        private void OnEnable()
        {
            MixerController.OnIsMasterEnabled += MasterIconInit;
        }

        private void OnDisable()
        {
            MixerController.OnIsMasterEnabled -= MasterIconInit;
        }
    }
}
