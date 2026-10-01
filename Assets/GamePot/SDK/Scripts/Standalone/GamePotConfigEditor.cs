using UnityEngine;
using System.IO;

#if UNITY_EDITOR && UNITY_STANDALONE
using GamePotUnity.Standalone;
using GamePotUnity.Standalone.Networking;
using UnityEditor;

//namespace GamePotUnity.Standalone.Editor
//{

/// <summary>
/// GamePot SDK Unity Standalone Tool
/// </summary>
[CustomEditor(typeof(GamePotConfig))]
class GamePotConfigEditor : UnityEditor.Editor
{
    private static GamePotConfigEditor s_instance = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (GamePotConfig)target;

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUIStyle guiStyle = new GUIStyle(EditorStyles.miniButton);
            guiStyle.fontSize = 13;
            if (GUILayout.Button("Confirm", guiStyle, GUILayout.Width(120), GUILayout.Height(30)))
            {
                GamePotConfig.Instance.PROJECT_ID = script.PROJECT_ID;
                GamePotConfig.Instance.STORE = script.STORE;
                GamePotConfig.Instance.API_URL = script.API_URL;
                GamePotConfig.Instance.REGION = script.REGION;

                var filePath = Path.Combine(Application.dataPath, "GamePotStandalone_Config.json");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var json = JsonUtility.ToJson(GamePotConfig.Instance);
                File.AppendAllText(filePath, json);

                Debug.LogFormat("[GamePotConfigEditor] Save the GamePotConfig Data : {0}", json.ToString());
                Debug.LogFormat("[GamePotConfigEditor] Save the GamePotConfig Path : {0}", filePath);
            }
        }
        GUILayout.EndHorizontal();

    }

    // WINDOW SETUP
    [MenuItem("GAMEPOT/GAMEPOT STANDALONE CONFIG")]
    static void OpenGamePotConfig()
    {
        string path = AssetDatabase.GetAssetPath(GamePotConfig.Instance);
        //Debug.LogFormat("Click GamePotConfig File {0}", path);
        if (string.IsNullOrEmpty(path))
        {
            path = "Assets/GamePot/SDK/Scripts/Standalone/GamePotStandalone_Config.asset";
            AssetDatabase.CreateAsset(CreateInstance<GamePotConfig>(), path);
            //Debug.LogFormat("Create GamePotConfig File {0}", path);
        }

        Object obj = AssetDatabase.LoadAssetAtPath<Object>(path);
        Selection.activeObject = obj;
        EditorGUIUtility.PingObject(obj);
    }




}
//}



#endif


//namespace GamePotUnity.Standalone
//{
//    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
//    {
//        static T _instance = null;
//        public static T Instance
//        {
//            get
//            {
//                if (!_instance)
//                    _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
//                return _instance;
//            }
//        }
//    }

//}

