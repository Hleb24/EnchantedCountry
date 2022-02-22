using Aberrance.Extensions;
using UnityEngine.Assertions;

namespace Core.Main.GameRule.Impact {
  public abstract class Impact<T> {
    protected readonly ImpactType _typeOfImpact;
    protected readonly int _protectiveThrow;
    private string _impactName;
    private int _diceRollValueForInvokeImpact;

    protected Impact(ImpactType impactType, string nameOfImpact, int diceRollValueForInvokeImpact, int protectiveThrow) {
      Assert.IsTrue(string.IsNullOrEmpty(nameOfImpact).False(), nameof(nameOfImpact));
      _typeOfImpact = impactType;
      _impactName = nameOfImpact;
      _diceRollValueForInvokeImpact = diceRollValueForInvokeImpact;
      _protectiveThrow = protectiveThrow;
    }

    public abstract void ImpactAction(T impactAction);
  }
}