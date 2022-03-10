using Audio;
using Logistix;
using Logistix.Core;
using Networking.Models;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using TMPro;

namespace UI
{
	public class MainPanel : MenuPanel
	{

		[SerializeField] private TMP_Text Name;
		[SerializeField] private TMP_Text Wood;
		[SerializeField] private TMP_Text Iron;
		[SerializeField] private GameObject PremiumButton;

		protected override void OnOpen()
		{
			MusicPlayer.Instance.Play(Song.Menu);
			PlayerModel model = Player.GetModel();
			Name.text = model.Name;
			Name.color = model.Premium ? Color.yellow : Color.white;
			LoadResources(model);
			PremiumButton.SetActive(!model.Premium);
		}

		protected override void OnClose() { }

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
			Menu.Instance.Swich(Menu.Instance.Upgrade);
		}

		public void OnBugReport()
        {
			LogisticAPI.Instance.OpenBugReport();
		}

		#endregion

		#region PRIAVTE

		private void LoadResources(PlayerModel model)
		{
			(int, int) upgrades = ShipUpgrade.GetSpent();
			Wood.text = Util.ToReadableNumber(model.Wood - upgrades.Item1);
			Iron.text = Util.ToReadableNumber(model.Iron - upgrades.Item2);
		}

		#endregion

	}
}