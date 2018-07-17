using UnityEditor;
using UnityEngine;

namespace MVP4U.Editor
{
    public static class SettingsScriptableObjectCreator
    {
        public static void Create(string createAssetPath)
        {
            var asset = ScriptableObject.CreateInstance<Settings>();
            AssetDatabase.CreateAsset(asset, $"Assets/{createAssetPath}MVP4USettings.asset");
            AssetDatabase.Refresh();
        }
    }
}