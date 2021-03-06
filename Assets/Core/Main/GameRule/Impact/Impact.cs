using Aberrance.Extensions;
using UnityEngine.Assertions;

namespace Core.Main.GameRule.Impact {
  public abstract class Impact<T> {
    protected readonly ImpactType _typeOfImpact;
    protected readonly int _protectiveThrow;
    private readonly int _diceRollValueForInvokeImpact;
    private string _impactName;

    protected Impact(ImpactType impactType, string nameOfImpact, int diceRollValueForInvokeImpact, int protectiveThrow) {
      Assert.IsTrue(string.IsNullOrEmpty(nameOfImpact).IsFalse(), nameof(nameOfImpact));
      _typeOfImpact = impactType;
      _impactName = nameOfImpact;
      _diceRollValueForInvokeImpact = diceRollValueForInvokeImpact;
      _protectiveThrow = protectiveThrow;
    }

    public int GetDiceRollValueForInvokeImpact() {
      return _diceRollValueForInvokeImpact;
    }

    public abstract void ImpactAction(T impactAction);
  }
}