using UnityEngine;

class MainPanel : MenuPanel
{

	protected override void OnOpen() {}

	protected override void OnClose() {}

	#region UI

	public void OnPlayButtonClick()
	{
		Menu.Instance.Swich(Menu.Instance.Levels);
	}


	#endregion

}
