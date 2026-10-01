using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR && UNITY_STANDALONE
using UnityEditor;

namespace GamePotUnity.Standalone.Editor
{
    public class CustomPostProcessor : AssetPostprocessor
    {
        /// <summary>
        /// Handles when ANY asset is imported, deleted, or moved.  Each parameter is the full path of the asset, including filename and extension.
        /// </summary>
        /// <param name="importedAssets">The array of assets that were imported.</param>
        /// <param name="deletedAssets">The array of assets that were deleted.</param>
        /// <param name="movedAssets">The array of assets that were moved.  These are the new file paths.</param>
        /// <param name="movedFromPath">The array of assets that were moved.  These are the old file paths.</param>

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
        {
            SetScriptingDefineSymbols();
        }

        private static BuildTargetGroup[] GetBuildTargets()
        {
            ArrayList _targetGroupList = new ArrayList();
            _targetGroupList.Add(BuildTargetGroup.Standalone);
            _targetGroupList.Add(BuildTargetGroup.Android);
            _targetGroupList.Add(BuildTargetGroup.iOS);
            _targetGroupList.Add(BuildTargetGroup.WSA);
            return (BuildTargetGroup[])_targetGroupList.ToArray(typeof(BuildTargetGroup));
        }

        static void SetScriptingDefineSymbols()
        {
            BuildTargetGroup[] _buildTargets = GetBuildTargets();
            if (!EditorPrefs.GetBool(Application.dataPath + "Project_opened"))
            {
                foreach (BuildTargetGroup _target in _buildTargets)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(_target, "");
                }
                EditorPrefs.SetBool(Application.dataPath + "Project_opened", true);
            }
            foreach (BuildTargetGroup _target in _buildTargets)
            {
                string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(_target);
                CheckDefines(ref defines, "Assets/Vuplex", "VUPLEX");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(_target, defines);
            }
        }

        private static bool CheckDefines(ref string defines, string path, string symbols)
        {
            if (Directory.Exists(path))
            {
                if (!defines.Contains(symbols))
                {
                    defines = defines + "; " + symbols;
                }
                return true;
            }
            var replace = defines.Replace(symbols, "");
            defines = replace;
            return false;
        }
    }

}
#endif