using Logistix;
using Logistix.Core;
using Networking.Models;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class UpgradePanel : MenuPanel
    {
        [Header("Dependecies")]
        [SerializeField] private UpgradeComponent ship;
        [SerializeField] private UpgradeComponent tower;
        [SerializeField] private UpgradeComponent life;
        [SerializeField] private Text WoodBalance;
        [SerializeField] private Text IronBalance;


        protected override void OnClose()
        { }

        protected override void OnOpen()
        {
            Refresh();
        }

        public override void Back()
        {
            Menu.Instance.Swich(Menu.Instance.Main);
        }

        #region PRIVATE

        private void Refresh()
        {
            SetBalance();
            Refresh(ship, Upgrade.Body);
            Refresh(tower, Upgrade.Tower);
            Refresh(life, Upgrade.Life);
        }

        private void Refresh(UpgradeComponent comp, Upgrade upgrade)
        {
            int level = ShipUpgrade.GetUpgrade(upgrade);
            int max = ShipUpgrade.GetUpgradeMax(upgrade);
            if (level < max)
            {
                (int, int) res = ShipUpgrade.GetNextLevelCost(upgrade);
                comp.Set(res.Item1, res.Item2, level, max);
            }
            else
                comp.Set(max);
        }

        private void SetBalance()
        {
            PlayerModel model = Player.GetModel();
            (int, int) spent = ShipUpgrade.GetSpent();
            WoodBalance.text = Util.ToReadableNumber(model.Wood - spent.Item1);
            IronBalance.text = Util.ToReadableNumber(model.Iron - spent.Item2);
        }

        private void HandleClick(Upgrade upgrade)
        {
            if (CanUpgrade(upgrade))
            {
                ShipUpgrade.UpgradeOne(upgrade);
                GameManager.Instance.GetShip().Send();
                Refresh();
            }
            else
                Menu.Instance.Pop("Out of material", "Do quest to earn materials");
        }

        private bool CanUpgrade(Upgrade upgrade)
        {
            PlayerModel model = Player.GetModel();
            (int, int) spent = ShipUpgrade.GetSpent();
            (int, int) price = ShipUpgrade.GetNextLevelCost(upgrade);
            return model.Wood - spent.Item1 >= price.Item1 && model.Iron - spent.Item2 >= price.Item2;
        }

        #endregion

        #region UI

        public void OnBody() { HandleClick(Upgrade.Body); }

        public void OnTower() { HandleClick(Upgrade.Tower); }

        public void OnLife() { HandleClick(Upgrade.Life); }

        #endregion

    }
}