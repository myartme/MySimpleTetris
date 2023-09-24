using Engine;
using UnityEngine;

namespace View
{
	public abstract class BaseScreen<T> : MonoBehaviour where T : Component
	{
		public static T ClassInstance;
		public bool IsWindowSetActive { get; protected set; }

		protected virtual void Awake()
		{
			ClassInstance = GetComponent<T>();
		}

		protected void SetActiveScreen(bool isShowScreen)
		{
			ClassInstance.gameObject.SetActive(isShowScreen);
		}

		protected void GamePauseIfWindowIsActive()
		{
			if(IsWindowSetActive)
				Game.PauseGame();
			else
				Game.ResumeGame();
		}
	}
}
