using System.Collections.Generic;
using UnityEngine;
namespace CySDK
{
    #if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(AdmobID))]
    public class AdmobIdEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button(new GUIContent("Apply")))
            {
                SetAdId(target as AdmobID);
                AssetDatabase.Refresh();
            }
        }
        static void SetAdId(AdmobID admob)
        {
            var id = admob.Id.Trim();
            var main = XmlHelper.GetAndroidManifest();

            var meta = main.FindNode("/manifest/application/meta-data", "android:name", "com.google.android.gms.ads.APPLICATION_ID");
            if (meta != null)
            {
                meta.Attributes["android:value"].Value = id;
            }
            else
            {
                var app = main.SelectSingleNode("/manifest/application");
                var m = main.CreateElement("meta-data");
                m.AppendAttribute("name", "com.google.android.gms.ads.APPLICATION_ID");
                m.AppendAttribute("value", id);
                m.AppendAttribute("replace", "android:value", "tools");
                app.AppendChild(m);
            }
            main.Save();
        }
    }
#endif

    public class AdmobID : AScriptableObject
    {
        public string Id;
        public override string filePath => "CySDK/AdmobID";
    }
}
