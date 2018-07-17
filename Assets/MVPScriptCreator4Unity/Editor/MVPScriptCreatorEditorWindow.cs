using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace MVP4U.Editor
{
    public class MvpScriptCreatorEditorWindow : EditorWindow
    {
        private string _baseClassName = string.Empty;

        private string _nameSpace = string.Empty;

        private Settings _settings;

        [MenuItem("Window/MVPScriptCreator4Unity")]
        private static void Open()
        {
            EditorWindow.GetWindow<MvpScriptCreatorEditorWindow>("MVP4U");
        }

        private void OnEnable()
        {
            _settings = Resources.Load<Settings>("MVP4USettings");

            if (!string.IsNullOrEmpty(_settings.Namespace))
            {
                _nameSpace = _settings.Namespace;
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Create Base Class Name");
            _baseClassName = EditorGUILayout.TextField(_baseClassName);
            EditorGUILayout.LabelField("NameSpace");
            _nameSpace = GUILayout.TextField(_nameSpace);

            if (GUILayout.Button("CreateScript"))
            {
                string path = AssetDatabase.GetAssetPath(Selection.activeObject);

                // if unselect asset, Scripts creates under the "Assets/".
                if (string.IsNullOrEmpty(path))
                {
                    path = Application.dataPath;
                }

                CreateScriptAsset(_nameSpace, _baseClassName, "Model", path);
                CreateScriptAsset(_nameSpace, _baseClassName, "Presenter", path);
                CreateScriptAsset(_nameSpace, _baseClassName, "View", path);
                Debug.Log($"Create Script Path : {path}");
            }

            if (GUILayout.Button("Save Namespace"))
            {
                _settings.Namespace = _nameSpace;
                EditorUtility.SetDirty(_settings);
                AssetDatabase.SaveAssets();
                Debug.Log($"Save NameSpace : {_nameSpace}");
            }
        }

        private const string TempleteScriptFilePath = "ScriptTemplete/";

        private static void CreateScriptAsset(string nameSpace, string baseClassName, string domainName, string filePath)
        {
            string templeteRawText = Resources.Load($"{TempleteScriptFilePath}{domainName}.cs").ToString();
            string replacedText = templeteRawText.Replace("#SCRIPTNAME#", baseClassName).Replace("#NAMESPACE", nameSpace);
            var encodeing = new UTF8Encoding(true, false);

            if (Path.GetExtension(filePath) != "")
            {
                // If you select Non directory, then get parent directory.
                filePath = Directory.GetParent(filePath).FullName + "/";
            }

            filePath += "/";

            string fileName = $"{baseClassName}{domainName}.cs";
            File.WriteAllText(filePath + fileName, replacedText, encodeing);

            var createdScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath + fileName);
            ProjectWindowUtil.ShowCreatedAsset(createdScript);
            AssetDatabase.Refresh();
        }
    }
}