﻿using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class MainPanel : MenuPanel
{

	[SerializeField] private Text Name;
	[SerializeField] private Text Wood;
	[SerializeField] private Text Iron;
	[SerializeField] private GameObject PremiumButton;

	protected override void OnOpen() 
	{
		MusicPlayer.Instance.Play(Song.Menu);
		PlayerModel model = Player.Instance.GetModel();
		Name.text = model.Name;
		Name.color = model.Premium ? Color.yellow : Color.white;
		LoadResources(model);
		PremiumButton.SetActive(!model.Premium);
	}

	protected override void OnClose() {}

	#region UI

	public void OnPlayButtonClick()
	{
		Menu.Instance.Swich(Menu.Instance.Levels);
	}

	public void OnPremiumButtonClick()
	{
		Menu.Instance.Swich(Menu.Instance.Premium);
	}

	public void OnShopButtonClick()
	{ 
		//TODO
	}

	#endregion

	#region PRIAVTE

	private void LoadResources(PlayerModel model)
	{
		(int, int) upgrades = ShipUpgrade.Instance.GetSpent();
		Wood.text = ToReadableNumber(model.Wood - upgrades.Item1);
		Iron.text = ToReadableNumber(model.Iron - upgrades.Item2);
	}

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
