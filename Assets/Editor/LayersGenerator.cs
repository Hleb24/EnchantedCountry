using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EnchantedCountry.Editor {
  public class LayersGenerator {
    private const string UTILS = "Utils";
    private const string LAYERS = "Layers";

    [MenuItem("Tools/Create Layers.cs")]
    public static void Generate() {
      var path = $"{Application.dataPath}/{UTILS}/{LAYERS}.cs";
      var code = $@"
namespace EnchantedCountry.Utils{{
public static class Layers {{

                {
                  string.Join("\n",
                    GetLayerNames()
                      .Where(layerName => layerName != "")
                      .Select(layerName => $"const string {layerName.Replace(" ", "_").ToUpper()} = \"{layerName}\";")
                  )
                }

            }}
}}";

      // Записываем код в файл
      File.WriteAllText(path, code);
    }

    // Записываем
    private static string[] GetLayerNames() {
      var layers = new string[32];
      for (var i = 0; i < 32; i++) {
        layers[i] = LayerMask.LayerToName(i);
      }

      return layers;
    }
  }
}