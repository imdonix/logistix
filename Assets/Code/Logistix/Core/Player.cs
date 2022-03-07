using Networking.Core;
using Networking.Models;
using System;
using System.Text.RegularExpressions;
using UI;
using UnityEngine;

namespace Logistix.Core
{
    public static class Player
    {
        private const string ID_KEY = "key";

        private static PlayerModel Model;
        private static string UserID;
        private static bool IsLoggedIn;

        #region PUBLIC

        public static bool IsLogged()
        {
            return IsLoggedIn;
        }

        public static string GetUserID()
        {
            return UserID;
        }

        public static PlayerModel GetModel()
        {
            return Model;
        }

        /// <summary>
        /// Load the palyer data from email
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="error">callback when error</param>
        public static void Load(string email, Action error)
        {
            PlayerPrefs.SetString(ID_KEY, email);
            LoadUser();

            LogisticAPI.Instance.GetPlayer(model => Refresh(model),
            err =>
            {
                Debug.LogError(err);
                error.Invoke();
            });
        }

        public static void SetName(string name, Action<string> error)
        {
            string errorText;
            if (IsNameValid(name, out errorText))
                LogisticAPI.Instance.SetName(name,
                model => Refresh(model),
                err => error.Invoke(err));
            else
                error.Invoke(errorText);
        }

        /// <summary>
        /// Refresh the player model.
        /// </summary>
        /// <param name="model"></param>
        public static void Refresh(PlayerModel model)
        {
            if (!ReferenceEquals(Model, null))
                if (model.Premium && !Model.Premium)
                    OnPremiumAccountActivated();

            Model = model;

            if (ReferenceEquals(model.Name, null))
                Menu.Instance.Swich(Menu.Instance.AddNamePanel);
            else
                Menu.Instance.Swich(Menu.Instance.Main);
        }

        /// <summary>
        /// Call this when you have no new playermodel to Refresh
        /// </summary>
        public static void Refresh()
        {
            Refresh(Model);
        }

        #endregion

        public static void LoadUser()
        {
            IsLoggedIn = PlayerPrefs.HasKey(ID_KEY);
            if (IsLoggedIn)
                UserID = PlayerPrefs.GetString(ID_KEY);
        }

        private static bool IsNameValid(string name, out string error)
        {
            if (name.Length < 4)
            { error = "short"; return false; }
            if (name.Length > 8)
            { error = "long"; return false; }
            if (!Regex.IsMatch(name, "^[a-zA-Z]*$"))
            { error = "special characters"; return false; }
            error = "ok";
            return true;
        }

        private static void OnPremiumAccountActivated()
        {
            Menu.Instance.Pop("Premium account", "Congratulation! Your account is premium now.");
        }
    }
}