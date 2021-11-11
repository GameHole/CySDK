using System.IO;

namespace UnityEditor.Android
{
    public class CopyJsonPostBuildProcessor : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder
        {
            get
            {
                return 999;
            }
        }
        public static string[] FindFilePaths(string[] fileNames)
        {
            var ret = new string[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                var assets = AssetDatabase.FindAssets(fileNames[i]);
                if (assets.Length > 0)
                {
                    ret[i] = AssetDatabase.GUIDToAssetPath(assets[0]);
                }
            }
            return ret;
        }
        static string[] needFile = new string[]
        {
            "google-services","game_sdk_config"
        };
        void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
        {
            var files = FindFilePaths(needFile);
            path = path.Replace("unityLibrary", "launcher");
            var targetPaths = new string[files.Length];
            targetPaths[0] = path + "/google-services.json";
            var directoryPath = path + "/src/main/assets";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            targetPaths[1] = directoryPath + "/game_sdk_config.json";
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(targetPaths[i]);
                File.Copy(files[i], targetPaths[i]);
            }
        }
    }
}
