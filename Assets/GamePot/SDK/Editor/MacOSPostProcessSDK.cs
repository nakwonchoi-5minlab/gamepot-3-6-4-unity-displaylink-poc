using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

namespace GamePotUnity.Standalone.Editor
{
    public class MacOSPostProcessSDK : IPostprocessBuildWithReport
    {
        public int callbackOrder { get { return 999; } }

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.StandaloneOSX)
                return;

            string buildPath = report.summary.outputPath;
            string appName = Path.GetFileNameWithoutExtension(buildPath);
            string contentsPath = Path.Combine(buildPath, "Contents");
            string infoPlistPath = Path.Combine(contentsPath, "Info.plist");
            
            string sourceEntitlementsPath = null;
            
            string pluginsEntitlementsPath = Path.Combine(Application.dataPath, "Plugins", "macOS", "GamePotMac.entitlements");
            if (File.Exists(pluginsEntitlementsPath))
            {
                sourceEntitlementsPath = pluginsEntitlementsPath;
            }
            else
            {
                string sdkEntitlementsPath = Path.Combine(Application.dataPath, "GamePot", "macOS", "GamePotMac.entitlements");
                if (File.Exists(sdkEntitlementsPath))
                {
                    sourceEntitlementsPath = sdkEntitlementsPath;
                }
                else
                {
                    string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
                    string externalEntitlementsPath = Path.Combine(projectRoot, "macOS", "GamePotMac.entitlements");
                    if (File.Exists(externalEntitlementsPath))
                    {
                        sourceEntitlementsPath = externalEntitlementsPath;
                    }
                }
            }
            
            if (sourceEntitlementsPath == null)
            {
                //Debug.LogError($"[GamePot] Entitlements 파일을 찾을 수 없습니다!");
            }

            string buildDir = Path.GetDirectoryName(buildPath);
            string destEntitlementsPath = Path.Combine(buildDir, "GamePotMac.entitlements");
            bool entitlementsCopied = false;
            
            if (sourceEntitlementsPath != null && File.Exists(sourceEntitlementsPath))
            {
                try
                {
                    File.Copy(sourceEntitlementsPath, destEntitlementsPath, true);
                    entitlementsCopied = true;
                }
                catch (System.Exception ex)
                {
                    //Debug.LogError($"[GamePot] Entitlements 파일 복사 실패: {ex.Message}");
                }
            }

            string sourceConfigPath = Path.Combine(Application.dataPath, "GamePotStandalone_Config.json");
            
            Debug.Log($"[GamePot] Config 소스 경로: {sourceConfigPath}");
            
            if (File.Exists(sourceConfigPath))
            {
                string destConfigPath = Path.Combine(contentsPath, "Resources", "Data", "GamePotStandalone_Config.json");
                
                try
                {
                    File.Copy(sourceConfigPath, destConfigPath, true);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"[GamePot] Config 파일 복사 실패: {ex.Message}");
                }
            }
            else
            {
                Debug.LogWarning($"[GamePot] Config 파일을 찾을 수 없습니다: {sourceConfigPath}");
            }
        }
    }
}
