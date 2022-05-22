using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.Support.Attributes {
  /// <summary>
  ///   Класс для проверки работы атрибутов требующих точку входа.
  /// </summary>
  public class Auditor {
    /// <summary>
    ///   Проверка работу атрибутов.
    /// </summary>
    public static void AttributeValidation() {
      ValidatePrefsKeysCheck();
    }

    private static void ValidatePrefsKeysCheck() {
      var names = new List<string>();
      Type[] types = Assembly.GetExecutingAssembly().GetTypes();

      foreach (Type type in types) {
        IEnumerable<FieldInfo> methodInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public)
          .Where(f => f.GetCustomAttributes().Any(a => a.GetType() == typeof(PrefsKeysAttribute)));
        foreach (FieldInfo fieldInfo in methodInfos) {
          var attribute = fieldInfo.GetCustomAttribute(typeof(PrefsKeysAttribute)) as PrefsKeysAttribute;
          if (attribute == null) {
            continue;
          }

          attribute.PrefsKey = (string)fieldInfo.GetValue(null);
          if (names.Contains(attribute.PrefsKey)) {
            Debug.LogError($"Повторения ключа в префс: ключ = {attribute.PrefsKey}, константа = {fieldInfo.Name}, класс = {type.Name}.");
            continue;
          }

          names.Add(attribute.PrefsKey);
        }
      }
    }

    
  }
}