﻿using DSoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class LogisticAPI : WebRequestEngine<LogisticAPI>
{
    #region GETS

    public void GetVersion(
        Action<VersionModel> response, 
        Action<string> error)
    {
        Send("version", response, error);
    }

    public void GetLevelMap(
        Action<LevelRowModel[]> response, 
        Action<string> error)
    {
        Send("levels", response, error);
    }

    public void GetPlayer(
        Action<PlayerModel> response, 
        Action<string> error)
    {
        Send("player", JsonConvert.SerializeObject(CreateAuthObject()), response, error);
    }

    public void GetToplist(
        int id,
        Action<List<RecordModel>> response,
        Action<string> error)
    {
        Send($"toplist/{id}", response, error);
    }

    public void GetInvites(
    Action<InviteModel> response,
    Action<string> error)
    {
        Send($"invite", JsonConvert.SerializeObject(CreateAuthObject()), response, error);
    }


    #endregion

    #region SETS

    public void RedeemPremium(
        Action<PlayerModel> response,
        Action<string> error)
    {
        Send("premium", JsonConvert.SerializeObject(CreateAuthObject()), response, error);
    }

    public void SetName(
        string name,
        Action<PlayerModel> response, 
        Action<string> error)
    {
        JObject obj = CreateAuthObject();
        obj.Add("name", name);
        Send("name", JsonConvert.SerializeObject(obj), response, error);
    }

    public void UploadLevelResoult(
    ResoultModel res,
    Action<PlayerModel> response,
    Action<string> error)
    {
        JObject obj = CreateAuthObject();
        obj.Add("resoult", res);
        Send("resoult", JsonConvert.SerializeObject(obj), response, error);
    }


    #endregion

    private JObject CreateAuthObject()
    {
        JObject obj = new JObject();
        obj.Add("email", Player.Instance.GetUserID());
        return obj;
    }
}
