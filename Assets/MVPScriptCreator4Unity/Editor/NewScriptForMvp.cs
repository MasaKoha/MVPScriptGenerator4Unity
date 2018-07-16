using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Mlv.Editor
{
    /// <summary>
    /// MVPパターン用のスクリプトの生成
    /// </summary>
    public class NewScriptForMvp
    {
        /// <summary>
        /// スクリプトのテンプレートがある Path
        /// </summary>
        private const string resourcePathOfScriptTemplete = "Editor/ScriptTemplete/";

        /// <summary>
        /// スクリプトを作成したときのデフォルトの名前
        /// </summary>
        private const string defaultAssetName = "NewScript.cs";

        /// <summary>
        /// C#スクリプト用のアイコンの名前
        /// </summary>
        private const string cSharpScriptIconName = "cs Script Icon";

        /// <summary>
        /// MVPパターン用のC#スクリプトを生成する
        /// </summary>
        [MenuItem("Assets/Create/C# Script(MVP Pattern)", false, 81)]
        public static void CreateCSharpScriptForMvp()
        {
            var icon = EditorGUIUtility.IconContent(cSharpScriptIconName).image as Texture2D;

            var endNameEditAction =
                ScriptableObject.CreateInstance<CreateScriptEndNameEditAction>() as EndNameEditAction;

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, endNameEditAction, defaultAssetName, icon,
                resourcePathOfScriptTemplete);
        }
    }
}