using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Mlv.Editor
{
    /// <summary>
    /// スクリプト生成の名前決定時に呼ばれるクラス
    /// </summary>
    public class CreateScriptEndNameEditAction : EndNameEditAction
    {
        /// <summary>
        /// スクリプト生成時、名前の決定後に呼ばれるイベント
        /// </summary>
        public override void Action(int _instanceId, string _pathName, string _templeteDirectoryPath)
        {
            CreateScriptAsset("Model", _pathName, _templeteDirectoryPath);
            CreateScriptAsset("View", _pathName, _templeteDirectoryPath);
            CreateScriptAsset("Presenter", _pathName, _templeteDirectoryPath);
        }

        /// <summary>
        /// スクリプトを生成する
        /// </summary>
        private static void CreateScriptAsset(string _domainName, string _filePath, string _templeteDirectoryPath)
        {
            string baseClassName = Path.GetFileNameWithoutExtension(_filePath);

            if (string.IsNullOrEmpty(baseClassName))
            {
                baseClassName = baseClassName.Replace(" ", "");
            }

            string templeteRawText = Resources.Load(_templeteDirectoryPath + _domainName + ".cs").ToString();
            string replacedText = templeteRawText.Replace("#SCRIPTNAME#", baseClassName);
            var encodeing = new UTF8Encoding(true, false);

            string addDomainFilePath = _filePath.Replace(".cs", _domainName + ".cs");
            File.WriteAllText(addDomainFilePath, replacedText, encodeing);

            AssetDatabase.ImportAsset(addDomainFilePath);
            var createdScript = AssetDatabase.LoadAssetAtPath<MonoScript>(addDomainFilePath + _domainName);
            ProjectWindowUtil.ShowCreatedAsset(createdScript);
        }
    }
}