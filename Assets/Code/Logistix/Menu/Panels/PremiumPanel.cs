﻿using Logistix;
using Logistix.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PremiumPanel : MenuPanel
    {
        [SerializeField] private GameObject Panel;
        [SerializeField] private Text Text;
        [SerializeField] private Slider Progress;
        [SerializeField] private Text Resoult;

        protected override void OnClose() { }

        protected override void OnOpen()
        {
            UpdatePremiumInfo();
        }
        public override void Back()
        {
            Menu.Instance.Swich(Menu.Instance.Main);
        }

        #region UI

        public void OnBackClick()
        {
            Back();
        }

        public void OnShareClick()
        {
            string refer = LogisticAPI.Instance.GetInviteURL();
            NativeShare share = new NativeShare();
            share.SetTitle("Play logistix with me!");
            share.SetText($"Try out this new game: {refer}");
            share.SetUrl(refer);
            share.Share();
        }

        #endregion


        #region PRIVATE

        private void UpdatePremiumInfo()
        {
            Panel.SetActive(false);
            Resoult.text = "Loading...";

            LogisticAPI.Instance.GetInvites(res =>
            {

                Text.text = $"{res.count} of {res.unlocks}";
                Progress.value = res.count / ((float)res.unlocks);
                if (res.count < res.unlocks)
                {
                    Panel.SetActive(true);
                    Resoult.text = String.Empty;
                }
                else
                    RedeemPremium();
            },
            err =>
            {
                Resoult.text = "Premium progress cannot be loaded! Try again later.";
            });
        }

        private void RedeemPremium()
        {
            void WriteError() { Resoult.text = "Something went wrong while trying to active your account."; }
            Resoult.text = "Redeem premium in progress...";
            LogisticAPI.Instance.RedeemPremium(res =>
            {
                if (res.Premium)
                    Player.Refresh(res);
                else
                    WriteError();
            },
            err => WriteError());
        }

        #endregion


    }
}