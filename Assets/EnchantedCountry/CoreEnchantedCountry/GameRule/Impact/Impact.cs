using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact {
  public interface ImpactOnRiskPoints {
    public void SetRiskPoints(ImpactType impactType, int damage, int protectiveThrow);
  }
  
  [Flags]
  public enum ImpactType {
    None = 0,
    Poison = 1 << 0,
    MagicianSpell = 1 << 1,
    MagesStaff = 1 << 2,
    DragonBreath = 1 << 3,
    Paralysis = 1 << 4,
    Petrification = 1 << 5,
    Fire = 1 << 6,
    Gas = 1 << 7,
    Acid = 1 << 8,
    Healing = 1 << 9,
    Light = 1 << 10,
    Lightning = 1 << 11,
    Positive = Healing,
    Negative = Poison | MagicianSpell | MagesStaff | DragonBreath | Paralysis | Petrification | Fire | Gas | Acid | Lightning,
    Neutral = Light
  }

  [Serializable]
  public abstract class Impact<T> {
    #region Constructors
    public Impact(ImpactType impactType, string nameOfImpact, int diceRollValueForInvokeImpact, int protectiveThrow) {
      typeOfImpact = impactType;
      impactName = nameOfImpact;
      DiceRollValueForInvokeImpact = diceRollValueForInvokeImpact;
      ProtectiveThrow = protectiveThrow;
    }
    #endregion

    #region Methods
    public abstract void ImpactAction(T impactAction);
    #endregion

    #region Fields
    public ImpactType typeOfImpact;
    public string impactName;
    public int DiceRollValueForInvokeImpact;
    public int ProtectiveThrow;
    #endregion
  }
}