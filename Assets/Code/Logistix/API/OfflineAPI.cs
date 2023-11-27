
using Networking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Logistix
{
    public class OfflineAPI : MonoBehaviour,  IAPI
    {
        public const string KEY_NAME = "pn";
        public const string KEY_WOOD = "pw";
        public const string KEY_IRON = "pi";
        public const string KEY_LVL = "pl";
        public const string KEY_TOP = "pt";

        public const string NOT_ENABLED = "Offline mode does not support this function";
        public const string GITHUB = "https://github.com/imdonix/logistix";
        public const string PLAY = "https://play.google.com/store/apps/details?id=com.donix.logistix";
        public const string EULA = "https://raw.githubusercontent.com/imdonix/logistix/offline/backend/public/privacy.txt";

        #region GETS

        public string GetServerURI()
        {
            return GITHUB;
        }

        public string GetEULA()
        {
            return EULA;
        }

        public int GetPendingCount()
        {
            return 0;
        }

        public bool IsRequestPendig()
        {
            return false;
        }

        public void GetVersion(
            Action<VersionModel> response,
            Action<string> error)
        {
            response.Invoke(new VersionModel("offline", Application.version));
        }

        public void GetLevelMap(
            Action<LevelRowModel[]> response,
            Action<string> error)
        {
            response.Invoke(GameManager.LoadOfflineMap());
        }

        public void GetPlayer(
        Action<PlayerModel> response,
        Action<string> error)
        {
            response.Invoke(Player());
        }

        public void GetToplist(
            string id,
            Action<List<RecordModel>> response,
            Action<string> error)
        {
            response.Invoke(GetToplist(id));
        }

        public void GetInvites(
        Action<InviteModel> response,
        Action<string> error)
        {
            error.Invoke(NOT_ENABLED);
        }


        #endregion

        #region SETS

        public void SetName(
            string name,
            Action<PlayerModel> response,
            Action<string> error)
        {
            PlayerPrefs.SetString(KEY_NAME, name);
            response.Invoke(Player());
        }

        public void UploadLevelResoult(
        ResoultModel res,
        Action<PlayerModel> response,
        Action<string> error)
        {
            if(res.IsWin)
            {
                PlayerPrefs.SetInt(KEY_WOOD, PlayerPrefs.GetInt(KEY_WOOD) + res.Wood);
                PlayerPrefs.SetInt(KEY_IRON, PlayerPrefs.GetInt(KEY_IRON) + res.Iron);
                AppendLevelMemory(res.ID);
                AppendRecordMemory(res.ID, res.Score);

                response.Invoke(Player());
            }
            else
            {
                response.Invoke(Player());
            }
        }

        public void RedeemPremium(
        Action<PlayerModel> response,
        Action<string> error)
        {
            response.Invoke(Player());
        }

        #endregion

        #region REFER

        public void OpenBugReport()
        {
            Application.OpenURL($"{GITHUB}/issues");
        }

        public string GetInviteURL()
        {
            return PLAY;
        }

        #endregion

        private PlayerModel Player()
        {
            PlayerModel model = new PlayerModel();

            if (PlayerPrefs.HasKey(KEY_NAME)) model.Name = PlayerPrefs.GetString(KEY_NAME);
            if (PlayerPrefs.HasKey(KEY_WOOD)) model.Wood = PlayerPrefs.GetInt(KEY_WOOD);
            if (PlayerPrefs.HasKey(KEY_IRON)) model.Iron = PlayerPrefs.GetInt(KEY_IRON);

            model.CompletedLeves = ReadLevelMemory();
            model.Premium = true; // Premium by default.

            return model;
        }

        private string ReadLevelMemory()
        {
            return PlayerPrefs.HasKey(KEY_LVL) ? PlayerPrefs.GetString(KEY_LVL) : string.Empty;
        }

        private void AppendLevelMemory(string level)
        {
            string old = ReadLevelMemory();
            PlayerPrefs.SetString(KEY_LVL, $"{old}|{level}");
        }

        private List<RecordModel> GetToplist(string map)
        {
            List<RecordModel> res = new List<RecordModel>();
            List<(string, string, int)> records = new List<(string, string, int)>();

            string[] tmp = PlayerPrefs.HasKey(KEY_TOP) ? PlayerPrefs.GetString(KEY_TOP).Split(' ') : new string[0];
            for (int i = 0; i < tmp.Length; i += 3)
            {
                records.Add((tmp[i], tmp[i + 1], int.Parse(tmp[i + 2])));
            }

            foreach (var rec in records)
            {
                if(rec.Item1 == map)
                {
                    res.Add(new RecordModel() { Name = rec.Item2, Premium = true, Score = rec.Item3 });
                }
            }

            return res;
        }

        private void AppendRecordMemory(string map, int score)
        {
            string[] old = PlayerPrefs.HasKey(KEY_TOP) ? PlayerPrefs.GetString(KEY_TOP).Split('|') : new string[0];

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < old.Length; i++)
            {
                builder.Append($"{old[i]} ");
            }
            builder.Append($"{map} ");
            builder.Append($"{PlayerPrefs.GetString(KEY_NAME)} ");
            builder.Append($"{score}");

            PlayerPrefs.SetString(KEY_TOP, builder.ToString());
        }
    }
}