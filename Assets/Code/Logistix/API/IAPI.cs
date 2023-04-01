using Networking.Core;
using Networking.Models;
using System;
using System.Collections.Generic;

namespace Logistix
{
    public interface IAPI : IEngineStatus
    {
        #region GETS

        public void GetVersion(
            Action<VersionModel> response,
            Action<string> error);

        public void GetLevelMap(
            Action<LevelRowModel[]> response,
            Action<string> error);

        public void GetPlayer(
            Action<PlayerModel> response,
            Action<string> error);

        public void GetToplist(
            int id,
            Action<List<RecordModel>> response,
            Action<string> error);

        public void GetInvites(
        Action<InviteModel> response,
        Action<string> error);


        #endregion

        #region SETS

        public void RedeemPremium(
            Action<PlayerModel> response,
            Action<string> error);

        public void SetName(
            string name,
            Action<PlayerModel> response,
            Action<string> error);

        public void UploadLevelResoult(
        ResoultModel res,
        Action<PlayerModel> response,
        Action<string> error);


        #endregion

        #region REFER

        public void OpenBugReport();

        public string GetInviteURL();

        public string GetServerURI();

        public string GetEULA();

        #endregion

    }
}