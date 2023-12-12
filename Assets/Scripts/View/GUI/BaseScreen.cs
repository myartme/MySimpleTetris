using UnityEngine;

namespace View.GUI
{
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class BaseScreen<T> : MonoBehaviour where T : Component
	{
		public static T ClassInstance;
		protected CanvasGroup canvasGroup;

		protected virtual void Awake()
		{
			ClassInstance = GetComponent<T>();
			canvasGroup = ClassInstance.GetComponent<CanvasGroup>();
			SetShowScreen(false);
		}

		protected void SetShowScreen(bool isShowScreen)
		{
			canvasGroup.alpha = isShowScreen ? 1 : 0;
			canvasGroup.blocksRaycasts = isShowScreen;
		}

		protected void SetActiveScreen(bool isActiveScreen)
		{
			ClassInstance.gameObject.SetActive(isActiveScreen);
		}
	}
}
