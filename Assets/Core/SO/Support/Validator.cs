using Core.Support.Attributes;
using UnityEngine;

namespace Core.SO.Support {
  /// <summary>
  ///   Класс для доступа к методу <see cref="OnValidate" />.
  /// </summary>
  [CreateAssetMenu(menuName = "Support/Validator", fileName = "Validator")]
  public class Validator : ScriptableObject {
    private void OnValidate() {
      Auditor.AttributeValidation();
    }
  }
}