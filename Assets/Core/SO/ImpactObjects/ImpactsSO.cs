using Core.Main.GameRule.Impact;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.ImpactObjects {
  [CreateAssetMenu(menuName = "ImpactSO", fileName = "ImpactSO", order = 60)]
  public class ImpactsSO : ScriptableObject {
    [FormerlySerializedAs("typeOfImpact"), SerializeField]
    private ImpactType _typeOfImpact;
    [FormerlySerializedAs("impactName"), SerializeField]
    private string _impactName;
    [FormerlySerializedAs("DiceRollValueForInvokeImpact"), SerializeField]
    private int _diceRollValueForInvokeImpact;
    [FormerlySerializedAs("ProtectiveThrow"), SerializeField]
    private int _protectiveThrow;
    [FormerlySerializedAs("Id"), SerializeField]
    private int _id;
    private Impact<IImpactOnRiskPoints> _impact;

    public Impact<IImpactOnRiskPoints> GetImpact() {
      if ((_typeOfImpact & ImpactType.MagesStaff) == ImpactType.MagesStaff) {
        _impact = new MagesStaff(_typeOfImpact, _impactName, _diceRollValueForInvokeImpact, _protectiveThrow);
      }

      if ((_typeOfImpact & ImpactType.Paralysis) == ImpactType.Paralysis) {
        _impact = new Paralysis(_typeOfImpact, _impactName, _diceRollValueForInvokeImpact, _protectiveThrow);
      }

      if ((_typeOfImpact & ImpactType.Petrification) == ImpactType.Petrification) {
        _impact = new Petrification(_typeOfImpact, _impactName, _diceRollValueForInvokeImpact, _protectiveThrow);
      }

      if ((_typeOfImpact & ImpactType.DragonBreath) == ImpactType.DragonBreath) {
        _impact = new DragonBreath(_typeOfImpact, _impactName, _diceRollValueForInvokeImpact, _protectiveThrow);
      }

      if ((_typeOfImpact & ImpactType.Poison) == ImpactType.Poison) {
        _impact = new Poison(_typeOfImpact, _impactName, _diceRollValueForInvokeImpact, _protectiveThrow);
      }

      if ((_typeOfImpact & ImpactType.MagicianSpell) == ImpactType.MagicianSpell) {
        _impact = new MagicianSpell(_typeOfImpact, _impactName, _diceRollValueForInvokeImpact, _protectiveThrow);
      }

      if ((_typeOfImpact & ImpactType.Lightning) == ImpactType.Lightning) {
        _impact = new Lightning(_typeOfImpact, _impactName, _diceRollValueForInvokeImpact, _protectiveThrow);
      }

      if (_impact == null) {
        Debug.LogWarning("Impact null!");
      }

      return _impact;
    }

    public int GetId() {
      return _id;
    }

    private void OnValidate() {
      _impactName = name;
    }
  }
}