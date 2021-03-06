using System.IO;
using UnityEditor;
using UnityEngine;

namespace EnchantedCountry.Editor {
  public class ScriptableObjectUtility : MonoBehaviour {
    /// <summary>
    ///   This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void CreateAsset<T>() where T : ScriptableObject {
      var asset = ScriptableObject.CreateInstance<T>();

      string path = AssetDatabase.GetAssetPath(Selection.activeObject);
      if (path == "") {
        path = "Assets";
      } else if (Path.GetExtension(path) != "") {
        path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
      }

      string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T) + ".asset");

      AssetDatabase.CreateAsset(asset, assetPathAndName);

      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
      EditorUtility.FocusProjectWindow();
      Selection.activeObject = asset;
    }
  }
}