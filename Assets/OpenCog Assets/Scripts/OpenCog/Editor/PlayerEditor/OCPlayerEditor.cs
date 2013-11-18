using UnityEngine;
using System.Collections;
using UnityEditor;
/// <summary>
/// window editor that loads player position from minecraft world 
/// and use this position for the unity player.
/// </summary>
public class OCPlayerEditor : EditorWindow 
{
    [MenuItem("LoadPlayer/load")]
    public static void init()
    {
        GetWindow<OCPlayerEditor>();
    }

    public Vector3 PlayerPosition;

    void OnEnable()
    {
        PlayerPosition = LoadPlayerFromMc.Position;
       
    }
    void OnDisable()
    {
        LoadPlayerFromMc.Position = PlayerPosition;
    }

    public void OnGUI()
    {

        if (GUI.Button(new Rect(10,10,150,20),"LoadPlayer Position"))
        {

            LoadPlayerFromMc lpfm = new LoadPlayerFromMc();
            ///checks the existance of the Director.
            /// and loads the player position
            bool alive = lpfm.IsExist();
            if (alive)
            {
                PlayerPosition = lpfm.GetPlayerPositionFromMC();
            }


        }
        EditorGUI.Vector3Field(new Rect(10,40,200,40),"Playe Position", PlayerPosition);
        if (GUI.Button(new Rect(10,80,150,20),"CreatePlayer"))
        {
            LoadPlayerFromMc lp = new LoadPlayerFromMc();

            OCGetPlayer.Create(PlayerPosition);
            if (lp.IsExist())
            {
                Debug.Log(lp.GetPlayerPositionFromMC());
            }

        }




    }
	
}
