using Logistix.Core;
using Networking.Models;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
	public class MainPanel : MenuPanel
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

		#endregion

		#region PRIAVTE

		private void LoadResources(PlayerModel model)
		{
			(int, int) upgrades = ShipUpgrade.Instance.GetSpent();
			Wood.text = Util.ToReadableNumber(model.Wood - upgrades.Item1);
			Iron.text = Util.ToReadableNumber(model.Iron - upgrades.Item2);
		}

		#endregion

	}
}