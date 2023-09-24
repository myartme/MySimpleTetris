using TMPro;
using UnityEngine;

namespace View
{
	public abstract class BaseText<T> : BaseScreen<T> where T : Component
	{
		protected static TextMeshProUGUI TextMeshPro;
		protected virtual string DefaultText => "";

		protected sealed override void Awake()
		{
			base.Awake();
			TextMeshPro = GetComponent<TextMeshProUGUI>();
		}
		
		protected void UpdateCountText(int count)
		{
			TextMeshPro.text = DefaultText + count;
		}
		
		protected void UpdateCountText(float count)
		{
			TextMeshPro.text = DefaultText + count;
		}
	}
}
