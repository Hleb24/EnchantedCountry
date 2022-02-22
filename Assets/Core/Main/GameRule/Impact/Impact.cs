using System;
using UnityEngine;

namespace Core.Main.GameRule.Impact {
  [Serializable]
  public abstract class Impact<T> {
    [SerializeField]
    public ImpactType typeOfImpact;
    [SerializeField]
    public string impactName;
    [SerializeField]
    public int DiceRollValueForInvokeImpact;
    [SerializeField]
    public int ProtectiveThrow;

    protected Impact(ImpactType impactType, string nameOfImpact, int diceRollValueForInvokeImpact, int protectiveThrow) {
      typeOfImpact = impactType;
      impactName = nameOfImpact;
      DiceRollValueForInvokeImpact = diceRollValueForInvokeImpact;
      ProtectiveThrow = protectiveThrow;
    }

    public abstract void ImpactAction(T impactAction);
  }
}