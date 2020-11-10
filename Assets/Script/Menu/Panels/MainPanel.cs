using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class MainPanel : MenuPanel
{

	[SerializeField] private Text Name;
	[SerializeField] private Text Wood;
	[SerializeField] private Text Iron;

	protected override void OnOpen() 
	{
		PlayerModel model = Player.Instance.GetModel();
		Name.text = model.Name;
		Wood.text = ToReadableNumber(model.Wood);
		Iron.text = ToReadableNumber(model.Iron);
	}

	protected override void OnClose() {}

	#region UI

	public void OnPlayButtonClick()
	{
		Menu.Instance.Swich(Menu.Instance.Levels);
	}

	public void OnPremiumButtonClick()
	{
		//TODO
	}

	public void OnShopButtonClick()
	{ 
		//TODO
	}

	#endregion

	#region PRIAVTE

	private static string ToReadableNumber(int number)
	{
		string Reverse(string str) { return new string(str.Reverse().ToArray()); }

		StringBuilder tmp = new StringBuilder();

		int c = 0;
		foreach(char chr in Reverse(number.ToString()))
		{
			if (c % 3 == 0 && c > 0)
				tmp.Append('.');
			tmp.Append(chr);
			c++;
		}

		return Reverse(tmp.ToString());
	}

	#endregion

}
