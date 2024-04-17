using TMPro;
using UnityEngine;

namespace View.GUI
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public abstract class BaseText : MonoBehaviour
	{
		protected TextMeshProUGUI textMeshPro;
		protected virtual string DefaultText => "";

		protected void Awake()
		{
			textMeshPro = GetComponent<TextMeshProUGUI>();
			Engine.GUIManager.TextFields.Add(this);
		}
		
		public virtual void UpdateCountText(string count)
		{
			textMeshPro.text = DefaultText + count;
		}
		
		public virtual void UpdateCountText(int count)
		{
			textMeshPro.text = DefaultText + count;
		}
		
		public virtual void UpdateCountText(float count)
		{
			textMeshPro.text = DefaultText + count;
		}
	}
}
