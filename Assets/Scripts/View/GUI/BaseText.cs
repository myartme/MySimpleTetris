using TMPro;
using UnityEngine;

namespace View.GUI
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public abstract class BaseText : MonoBehaviour
	{
		protected TextMeshProUGUI TextMeshPro;
		protected virtual string DefaultText => "";

		protected void Awake()
		{
			TextMeshPro = GetComponent<TextMeshProUGUI>();
			Engine.GUIManager.TextFields.Add(this);
		}
		
		public virtual void UpdateCountText(int count)
		{
			TextMeshPro.text = DefaultText + count;
		}
		
		public virtual void UpdateCountText(float count)
		{
			TextMeshPro.text = DefaultText + count;
		}
	}
}
