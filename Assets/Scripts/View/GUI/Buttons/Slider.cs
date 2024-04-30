using UnityEngine;
using View.GUI.Scheme.ColorStyleWrappers;

namespace View.GUI.Buttons
{
    public class Slider : MonoBehaviour
    {
        [SerializeField] private ColorImageWrapper background;
        [SerializeField] private ColorImageWrapper handle;
        [SerializeField] private ColorImageWrapper fill;
    }
}