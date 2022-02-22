using Core.Main.Character;
using Core.Main.Dice;

namespace Core.Mono.Scenes.QualitiesImprovement {
  public class QualityIncrease {
    private Dice _die;
    private int[] _diceRollValueForQualitiesIncrease;
    private int _pointer;

    public QualityIncrease() {
      SetLengthForArray(QualityTypeHandler.NUMBER_OF_QUALITY);
      SetDiceBox();
    }
    
    private void SetDiceBox() {
      _die = new SixSidedDice();
    }

    public void IncreaseQualitiesForWizard() {
      if (LengthIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      }

      int diceRoll = GetDiceRollValue();
      int valueForWizard = GetValueForWizard(diceRoll);
      SetValueByPointer(valueForWizard);
      MovePointerForward();
      if (LengthIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      }

      IncreaseQualitiesForWizard();
    }

    public void IncreaseQualitiesForKron() {
      if (LengthIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      }

      int diceRoll = GetDiceRollValue();
      int valueForWizard = GetValueForKron(diceRoll);
      SetValueByPointer(valueForWizard);
      MovePointerForward();
      if (LengthIsEqualToPointer(_diceRollValueForQualitiesIncrease)) {
        return;
      }

      IncreaseQualitiesForKron();
    }

    private void SetLengthForArray(int length) {
      _diceRollValueForQualitiesIncrease = new int[length];
    }

    

    private int GetValueForWizard(int diceRollValue) {
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

    private int GetValueForKron(int diceRollValue) {
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

    private bool LengthIsEqualToPointer(int[] array) {
      return _pointer == array.Length;
    }

    private void SetValueByPointer(int value) {
      _diceRollValueForQualitiesIncrease[_pointer] = value;
    }

    private int GetDiceRollValue() {
      return _die.GetDiceRoll();
    }

    private void MovePointerForward() {
      _pointer++;
    }

    public int this[int index] {
      get {
        return _diceRollValueForQualitiesIncrease[index];
      }
    }
  }
}