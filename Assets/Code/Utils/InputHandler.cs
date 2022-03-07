using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public delegate void OnClick();
	public delegate void OnSwipe(int direction);
	public delegate void OnBack();

	public class InputHandler : Singleton<InputHandler>
	{
		private const int SWIPE_TO_SCREEN = 3;

		private OnClick ClickHandlers;
		private OnSwipe SwipeHandlers;
		private OnBack BackHandlers;
		private int SwipeLenght;
		private List<(int, Vector2)> Touches;

		#region UNITY

		protected override void Awake()
		{
			base.Awake();
			Touches = new List<(int, Vector2)>();
			SwipeLenght = Screen.width / SWIPE_TO_SCREEN;
		}

		private void Update()
		{
			HandleTouch();
			HandleClick();
			HandleBack();
		}

		#endregion

		#region PUBLIC

		/// <summary>
		/// Add Clickhandler.
		/// </summary>
		/// <param name="handler"></param>
		public void AddClickHandler(OnClick handler)
		{
			ClickHandlers = handler;
		}

		/// <summary>
		/// Add SwipeHandler. (Return 1 when swiped right and -1 when left)
		/// </summary>
		public void AddSwipeHandler(OnSwipe handler)
		{
			SwipeHandlers = handler;
		}

		/// <summary>
		/// Add BackHandler.
		/// </summary>
		public void AddBackHandler(OnBack handler)
		{
			BackHandlers = handler;
		}

		/// <summary>
		/// Remove all the handlers from Input
		/// </summary>
		public void RemoveTouchHandlers()
		{
			SwipeHandlers = null;
			ClickHandlers = null;
		}

		#endregion

		private void HandleClick()
		{
			if (!ReferenceEquals(ClickHandlers, null))
				if (Input.GetMouseButton(0))
					ClickHandlers();
		}

		private void HandleTouch()
		{
			if (!ReferenceEquals(ClickHandlers, null))
			{
				foreach (var touch in Input.touches)
					if (touch.phase == TouchPhase.Began)
					{
						Touches.Add((touch.fingerId, touch.position));
					}
					else if (touch.phase == TouchPhase.Ended)
					{
						var start = Touches.Find(t => t.Item1 == touch.fingerId);
						DoTouch(start.Item2, touch.position);
						Touches.Remove(start);
					}
			}
		}

		private void HandleBack()
		{
			if (!ReferenceEquals(BackHandlers, null))
			{
				if (Input.GetKeyDown(KeyCode.Escape))
					BackHandlers.Invoke();
			}
		}

		private void DoTouch(Vector2 start, Vector2 to)
		{
			float deltaX = start.x - to.x;
			if (Mathf.Abs(deltaX) > SwipeLenght)
				SwipeHandlers(deltaX < 0 ? 1 : -1);
			else
				ClickHandlers();
		}
	}
}