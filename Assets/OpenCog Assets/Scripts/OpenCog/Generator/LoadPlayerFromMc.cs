using UnityEngine;
using System.Collections;
using Substrate;
using Substrate.Core;
using System.IO;

[System.Serializable]
public class LoadPlayerFromMc
{

    [SerializeField]
    public static UnityEngine.Vector3 position;
    /// <summary>
    /// the path for the minecraft .dat file
    /// </summary>
    public string path = @"D:/temp/TestScene2";
    public string playerName = "OCPlayer";

    /// <summary>
    /// checks the existance of the directory
    /// </summary>
    /// <returns> true if exist false otherwise</returns>
    public bool IsExist()
    {
        return Directory.Exists(path) ? true : false;

    }

    public static UnityEngine.Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    /// <summary>
    /// delete the available player and create a new one and terieve its info spc positon for this demo
    /// </summary>
    /// <returns> the position of the player</returns>
    /// <remarks> u can cancel the deletePlayer it only usefull for demo</remarks>
    public UnityEngine.Vector3 GetPlayerPositionFromMC()
    {

        AnvilWorld world = AnvilWorld.Open(path);
        IPlayerManager pm = world.GetPlayerManager();

        pm.DeletePlayer(playerName);
        Player CreatePlayer = new Player();
        Player FetchPlayer = new Player();
        CreatePlayer.Position.X = 10;
        CreatePlayer.Position.Y = 146;
        CreatePlayer.Position.Z = 10;
        pm.SetPlayer(playerName, CreatePlayer);

        FetchPlayer = pm.GetPlayer(playerName);
        position.x = (float)FetchPlayer.Position.X;
        position.y = (float)FetchPlayer.Position.Y;
        position.z = (float)FetchPlayer.Position.Z;

        return position;


    }

}
