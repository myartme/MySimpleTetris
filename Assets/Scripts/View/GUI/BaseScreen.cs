using UnityEngine;
using UnityEngine.UI;
using View.GUI.Scheme.ColorStyleWrappers;
using View.Scene;

namespace View.GUI
{
	[RequireComponent(typeof(CanvasGroup), typeof(Image))]
	public abstract class BaseScreen<T> : MonoBehaviour where T : Component
	{
		public static T ClassInstance;
		protected CanvasGroup canvasGroup;
		protected ColorImageWrapper backgroung;

		protected virtual void Awake()
		{
			ClassInstance = GetComponent<T>();
			canvasGroup = ClassInstance.GetComponent<CanvasGroup>();
			backgroung = ClassInstance.GetComponent<ColorImageWrapper>();
		}

		protected virtual void Start()
		{
			backgroung.ColorElementType = ColorElementType.Background;
			SetShowScreen(false);
		}

		protected void SetActiveScreen(bool isShowScreen)
		{
			SetShowScreen(isShowScreen);
			SetActiveByAlpha();
		}
		
		protected void SetShowScreen(bool isShowScreen)
		{
			canvasGroup.alpha = isShowScreen ? 1 : 0;
			canvasGroup.blocksRaycasts = isShowScreen;
		}

		protected void SetActiveByAlpha()
		{
			ClassInstance.gameObject.SetActive(canvasGroup.alpha > 0);
		}
	}
}
