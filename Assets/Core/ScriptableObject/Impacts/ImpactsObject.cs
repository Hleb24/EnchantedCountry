using Core.Rule.GameRule.Impact;
using Core.Rule.GameRule.Impact.Variants;
using UnityEngine;

namespace Core.ScriptableObject.Impacts {
  [CreateAssetMenu(menuName = "ImpactSO", fileName = "ImpactSO", order = 60)]
  public class ImpactsObject : UnityEngine.ScriptableObject {
    public ImpactType typeOfImpact;
    public string impactName;
    public int DiceRollValueForInvokeImpact;
    public int ProtectiveThrow;
    public Impact<ImpactOnRiskPoints> Impact;

    public Impact<ImpactOnRiskPoints> GetImpact() {
      if ((typeOfImpact & ImpactType.MagesStaff) == ImpactType.MagesStaff) {
        Impact = new MagesStaff(typeOfImpact, impactName, DiceRollValueForInvokeImpact, ProtectiveThrow);
      }

      if ((typeOfImpact & ImpactType.Paralysis) == ImpactType.Paralysis) {
        Impact = new Paralysis(typeOfImpact, impactName, DiceRollValueForInvokeImpact, ProtectiveThrow);
      }

      if ((typeOfImpact & ImpactType.Petrification) == ImpactType.Petrification) {
        Impact = new Petrification(typeOfImpact, impactName, DiceRollValueForInvokeImpact, ProtectiveThrow);
      }

      if ((typeOfImpact & ImpactType.DragonBreath) == ImpactType.DragonBreath) {
        Impact = new DragonBreath(typeOfImpact, impactName, DiceRollValueForInvokeImpact, ProtectiveThrow);
      }

      if ((typeOfImpact & ImpactType.Poison) == ImpactType.Poison) {
        Impact = new Poison(typeOfImpact, impactName, DiceRollValueForInvokeImpact, ProtectiveThrow);
      }

      if ((typeOfImpact & ImpactType.MagicianSpell) == ImpactType.MagicianSpell) {
        Impact = new MagicianSpell(typeOfImpact, impactName, DiceRollValueForInvokeImpact, ProtectiveThrow);
      } 
      
      if ((typeOfImpact & ImpactType.Lightning) == ImpactType.Lightning) {
        Impact = new Lightning(typeOfImpact, impactName, DiceRollValueForInvokeImpact, ProtectiveThrow);
      }

      if (Impact == null) {
        Debug.LogWarning("Impact null!");
      }

      return Impact;
    }

    private void OnValidate() {
      impactName = name;
    }
  }
}