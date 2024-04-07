using UnityEngine;
using View.GUI.Scheme.ColorStyleWrappers;

namespace View.GUI.Buttons
{
    public class Slider : MonoBehaviour
    {
        [SerializeField] private ColorImageWrapper _background;
        [SerializeField] private ColorImageWrapper _handle;
        [SerializeField] private ColorImageWrapper _fill;
    }
}