using UnityEngine;

class MainPanel : MenuPanel
{

	protected override void OnOpen()
	{}

    #region UI

    public void OnPlayButtonClick()
	{
		Menu.Instance.Swich(Menu.Instance.Levels);
	}


	#endregion

}
