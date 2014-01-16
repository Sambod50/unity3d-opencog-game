using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
namespace OCCubiquity
{
    [CustomEditor(typeof(Cubiquity.ColoredCubesVolume))]
    public class OCColoredCubesInspector : Cubiquity.ColoredCubesVolumeInspector
    {

        private Color32 ForAllColors;
       
        private CBScriptableObject scLoadedObject;

        void OnEnable()
        {
            base.OnEnable();
            ForAllColors = Color.white;
           
            scLoadedObject = ScriptableObject.CreateInstance<CBScriptableObject>();
            scLoadedObject = loadObject();
            
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(20);
            if (scLoadedObject != null)
            {               
                FillAll();               
                FillColorById();
            }
            if (GUI.changed)
            {
                EditorUtility.SetDirty(scLoadedObject);
               // EditorUtility.SetDirty(target);
            }
        }

        public  void FillColorById()
        {
            for (int x = 0; x < scLoadedObject._cbColors.Count; x++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Color Id :" + scLoadedObject._cbColorIds[x]);
                scLoadedObject._cbColors[x] = EditorGUILayout.ColorField(scLoadedObject._cbColors[x]);
                EditorGUILayout.EndHorizontal();
                paintVolume(scLoadedObject._cbColorIds[x], scLoadedObject._cbColors[x], scLoadedObject);
            }
        }

        public  void FillAll()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            ForAllColors = EditorGUILayout.ColorField(ForAllColors, GUILayout.Width(200));
            if (GUILayout.Button("Fill All"))
            {
                scLoadedObject._cbColors = scLoadedObject._cbColors.Select(c => c = ForAllColors).ToList();
                SubstrateIdToCubiquityColor.paintVolume(LoadDataObject(), ForAllColors, scLoadedObject);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

        }

        public void paintVolume(int id, Color32 color, CBScriptableObject cbobject)
        {
            SubstrateIdToCubiquityColor.paintVolume(id, LoadDataObject(), color, cbobject);
        }
        public Cubiquity.ColoredCubesVolumeData LoadDataObject()
        {
            object _myobject = Resources.Load("ColoredCubesVolumeData_Reference", typeof(Cubiquity.ColoredCubesVolumeData));
            Cubiquity.ColoredCubesVolumeData myobject = (Cubiquity.ColoredCubesVolumeData)_myobject;
            return myobject;
        }
        public CBScriptableObject loadObject()
        {
            object _cbObject = Resources.Load("Color_Id", typeof(CBScriptableObject));
            CBScriptableObject cbObject = (CBScriptableObject)_cbObject;
            return cbObject;
        }

       
    }
}
