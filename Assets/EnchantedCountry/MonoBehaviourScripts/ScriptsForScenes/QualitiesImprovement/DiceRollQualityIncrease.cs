using Core.EnchantedCountry.CoreEnchantedCountry.Dice;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.QualitiesImprovement {
  public class DiceRollQualityIncrease {
    #region FIELDS
    private Dices _dice;
    private int[] _diceRollValueForQualitiesIncrease;
    private int _pointer;
    public int this[int index] => _diceRollValueForQualitiesIncrease[index];
    #endregion

    #region INITIALIZATION
    public void Initialization() {
      SetLengthForArray(5);
      SetDiceBox();
    }

    private void SetLengthForArray(int length) {
      _diceRollValueForQualitiesIncrease = new int[length];
    }

    private void SetDiceBox() {
      _dice = new SixSidedDice(DiceType.SixEdges);
    }
    #endregion

    #region DICE_ROLL_QUALITY_INCREASE_FOR_WIZARD
    public void DiceRollQualityIncreaseForWizard() {
      if (LengthOfArrayIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      }

      int diceRoll = DiceRoll();
      int valueForWizard = GetValueForWizardQualitiesImprovement(diceRoll);
      SetValueByPointer(valueForWizard);
      MovePoihterForward();
      if (LengthOfArrayIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      } else {
        DiceRollQualityIncreaseForWizard();
      }
    }

    private int GetValueForWizardQualitiesImprovement(int diceRollValue) {
      switch (diceRollValue) {
        case 1:
        case 2:
        case 3:
          return 0;
        case 4:
          return 1;
        case 5:
          return 2;
        case 6:
          return 3;
      }

      return -1;
    }
    #endregion

    #region DICE_ROLL_QUALITY_INCREASE_FOR_KRON
    public void DiceRollQualityIncreaseForKron() {
      if (LengthOfArrayIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      }

      int diceRoll = DiceRoll();
      int valueForWizard = GetValueForKronQualitiesImprovement(diceRoll);
      SetValueByPointer(valueForWizard);
      MovePoihterForward();
      if (LengthOfArrayIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      } else {
        DiceRollQualityIncreaseForKron();
      }
    }

    private int GetValueForKronQualitiesImprovement(int diceRollValue) {
      switch (diceRollValue) {
        case 1:
        case 2:
        case 3:
        case 4:
          return 0;
        case 5:
        case 6:
          return 1;
      }

      return -1;
    }
    #endregion

    #region METHODS_FOR_DICE_ROLL_QUALITY_INCREASE
    private bool LengthOfArrayIsEqualToPointer(int[] array) {
      return _pointer == array.Length;
    }

    private void SetValueByPointer(int value) {
      _diceRollValueForQualitiesIncrease[_pointer] = value;
    }

    private int DiceRoll() {
      return _dice.RollOfDice();
    }

    private void MovePoihterForward() {
      _pointer++;
    }
    #endregion
  }
}