using System;
using System.IO;
using System.Linq;
using System.Text;
using EnchantedCountry.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnchantedCountry.Editor {
  public class GenerateSceneInBuild {
    [MenuItem("Tools/Create SceneInBuild.cs")]
    public static void CreateSceneInBuild() {
      int sceneInBuildCount = SceneManager.sceneCountInBuildSettings;
      var sceneNames = new string[sceneInBuildCount];
      for (var i = 0; i < sceneInBuildCount; i++) {
        sceneNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
      }

      string[] enums = Enum.GetNames(typeof(SceneInBuild));
      bool skip = sceneNames.Length == enums.Length && sceneNames.All(s => enums.Contains(s));
      if (skip) {
        Debug.LogWarning("Skip");
        return;
      }

      var sourceText = new StringBuilder();
      sourceText.Append(@"
namespace EnchantedCountry.Utils{
public enum SceneInBuild{

");
      for (var i = 0; i < sceneNames.Length; i++) {
        if (i < sceneNames.Length - 1) {
          sourceText.AppendLine($"{sceneNames[i]} = {i},");
        } else {
          sourceText.AppendLine($"{sceneNames[i]} = {i}");
        }
      }

      sourceText.Append(@"
  }
}");
      var sceneDirectory = $"{Application.dataPath}/{UTILS}/";
      if (string.IsNullOrEmpty(sceneDirectory)) {
        Debug.LogWarning("Scene directory is null");
        return;
      }

      string pathToFile = Path.Combine(sceneDirectory, $"{SCENE_IN_BUILD_ENUM}.cs");
      File.WriteAllText(pathToFile, sourceText.ToString());
    }

    private const string SCENE_IN_BUILD_ENUM = "SceneInBuild";
    private const string UTILS = "Utils";
  }
}