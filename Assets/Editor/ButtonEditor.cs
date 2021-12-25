using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.SupportSystems.Attributes;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Editor {
  [CustomEditor(typeof(object), true, isFallback = false), CanEditMultipleObjects]
  public class ButtonEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      base.OnInspectorGUI();
      foreach (Object currentTarget in targets) {
        IEnumerable<MethodInfo> methodInfos = currentTarget.GetType().GetMethods().Where(m => m.GetCustomAttributes().Any(a => a.GetType() == typeof(ButtonAttribute)));
        foreach (MethodInfo methodInfo in methodInfos) {
          {
            var attribute = methodInfo.GetCustomAttribute(typeof(ButtonAttribute)) as ButtonAttribute;
            if (attribute == null) {
              continue;
            }

            attribute.Name ??= methodInfo.Name;
            string buttonName = ObjectNames.NicifyVariableName(attribute.Name);
            if (GUILayout.Button(buttonName)) {
              methodInfo.Invoke(currentTarget, null);
            }
          }
        }
      }
    }
  }
}