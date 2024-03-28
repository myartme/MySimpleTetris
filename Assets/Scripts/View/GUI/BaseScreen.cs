using Engine;
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

		protected void Start()
		{
			backgroung.ColorElementType = ColorElementType.Background;
			SetShowScreen(false);
		}

		protected void SetShowScreen(bool isShowScreen)
		{
			canvasGroup.alpha = isShowScreen ? 1 : 0;
			canvasGroup.blocksRaycasts = isShowScreen;
			SetActiveByAlpha();
		}

		protected void SetActiveByAlpha()
		{
			ClassInstance.gameObject.SetActive(canvasGroup.alpha > 0);
		}
	}
}
