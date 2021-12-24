using Core.EnchantedCountry.SupportSystems.Attributes;
using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.Valid {
  /// <summary>
  ///   Класс для доступа к методу <see cref="OnValidate" />.
  /// </summary>
  [CreateAssetMenu(menuName = "Support/Validator", fileName = "Validator")]
  public class Validator : UnityEngine.ScriptableObject {
    private void OnValidate() {
      Auditor.AttributeValidation();
    }
  }
}