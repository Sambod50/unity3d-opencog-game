using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace OCCubiquity
{
    [CustomEditor(typeof(MapName))]
    public class OCMainMenuEntries : Editor
    {

        #region private data members
        private GUIContent checkMapName;
        private GUIContent MapName;
        private bool IsGUIEnabled = false;
        private string tabs = "\t\t\t\t\t\t\t\t\t";
        private string OnEmpty = "Map Name is empty";
        private string OnNotMatch = "Incorrect Map Name";
        private string OnMatch = "Correct Map Name!!";
       // private CBScriptableObject cbobject=null;
       // private Cubiquity.ColoredCubesVolumeData data = null;

        #endregion
        void OnEnable()
        {
            MapName = new GUIContent("Map Name");
        }
        public override void OnInspectorGUI()
        {

            GUILayout.Space(20);
            OCCubeVolume.MapName = EditorGUILayout.TextField(MapName, OCCubeVolume.MapName);

            if (OCCubeVolume.MapName.Length != 0)
            {
                checkMapName = new GUIContent(OnNotMatch);
                IsGUIEnabled = false;
                if (Directory.Exists(OCCubeVolume.Dir))
                {
                    checkMapName = new GUIContent(OnMatch);
                    IsGUIEnabled = true;
                }
            }
            else if (OCCubeVolume.MapName.Length == 0)
            {
                checkMapName = new GUIContent(OnEmpty);
                IsGUIEnabled = false;
            }

            GUI.enabled = IsGUIEnabled;
            GUILayout.Space(5);
            EditorGUILayout.LabelField(tabs + "" + checkMapName.text, EditorStyles.boldLabel);
            GUILayout.Space(15);
            if (GUILayout.Button("Load Map", GUILayout.Width(150)))
            {
              //  OCCreateAssets.CreateCubeVolume(ref cbobject,ref data);
                CreateCubeVolume();
                
            }

        }

        public static void CreateCubeVolume()
        {
            OCCubeVolume OCObject = ScriptableObject.CreateInstance<OCCubeVolume>();
            CBScriptableObject scLoadedObject = ScriptableObject.CreateInstance<CBScriptableObject>();
            Cubiquity.ColoredCubesVolumeData data = ScriptableObject.CreateInstance<Cubiquity.ColoredCubesVolumeData>();
            int regionwidth = OCCubeVolume.GetRegions();
            if (regionwidth > 3)
            {
                regionwidth = 3;
            }
            int width = regionwidth * 32 * 16;
            int height = 256;
            int depth = regionwidth * 32 * 16;
            data = Cubiquity.ColoredCubesVolumeData.CreateEmptyVolumeData(new Cubiquity.Region(0, 0, 0, width - 1, height - 1, depth - 1));
            Cubiquity.ColoredCubesVolume.CreateGameObject(data);
            OCObject.MCSubstrate(data);
            CreateAssets<CBScriptableObject>(SubstrateIdToCubiquityColor.CBObject);
            CreateReferenceData(data);

        }
        public static void CreateAssets<T>(CBScriptableObject cbobject) where T : CBScriptableObject
        {
            AssetDatabase.CreateAsset(cbobject, "Assets/Cubiquity/Editor/Resources/Color_Id.asset");
            AssetDatabase.SaveAssets();
        }

        public static void CreateReferenceData(Cubiquity.ColoredCubesVolumeData data)
        {
            AssetDatabase.CreateAsset(data, "Assets/Cubiquity/Editor/Resources/ColoredCubesVolumeData_Reference.asset");
            AssetDatabase.SaveAssets();
        }






    }
}