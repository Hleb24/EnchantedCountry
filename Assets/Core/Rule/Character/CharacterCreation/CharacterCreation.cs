using System.Collections.Generic;
using Core.Rule.Dice;
using Core.SupportSystems.Data;

namespace Core.Rule.Character.CharacterCreation {
  public class CharacterCreation {
    #region Constructors
    public CharacterCreation() {
      PlayerCharacter = new PlayerCharacter();
      PlayerCharacter.CharacterQualities = PlayerCharacter.CharacterQualities;
    }
    #endregion

    #region Properties
    public PlayerCharacter PlayerCharacter { get; set; }
    #endregion

    #region Fields
    private const int MULTIPLIER = 10;
    public QualityType qualityTypeForChoice;
    private List<int> _valuesWhiteDiceRoll;
    // ReSharper disable once NotAccessedField.Local
    private int _smallerValue;
    #endregion

    #region Methods
    public void SetQualityValue(QualityType qualityType, int qualityValue) {
      PlayerCharacter.CharacterQualities[qualityType].ValueOfQuality = qualityValue;
    }

    public int GetSumDiceRollForQuality() {
      _valuesWhiteDiceRoll = new List<int>();
      DiceBox diceBox = KitOfDice.diceKit[KitOfDice.SetWithFourSixSidedDice];
      for (var i = 0; i < diceBox.GetCountSetOfDice(); i++) {
        _valuesWhiteDiceRoll.Add(diceBox[i].RollOfDice());
      }

      return SortRemoveAndSumValuesWhiteDiceRoll(_valuesWhiteDiceRoll);
    }

    public int GetSumDiceRollForCoins() {
      _valuesWhiteDiceRoll = new List<int>();
      DiceBox diceBox = KitOfDice.diceKit[KitOfDice.SetWithFourSixSidedDice];
      for (var i = 0; i < diceBox.GetCountSetOfDice(); i++) {
        _valuesWhiteDiceRoll.Add(diceBox[i].RollOfDice());
      }

      return SortRemoveAndSumValuesWhiteDiceRollForNumberOfCoins(_valuesWhiteDiceRoll);
    }

    private int SortRemoveAndSumValuesWhiteDiceRoll(List<int> valuesDiceRoll) {
      var sum = 0;
      valuesDiceRoll.Sort();
      int smaller = valuesDiceRoll[0];
      SmallerValueInWhiteDiceRoll(smaller);
      var tempValues = new List<int>();
      tempValues.AddRange(valuesDiceRoll);
      tempValues.RemoveAt(0);
      for (var i = 0; i < tempValues.Count; i++) {
        sum += tempValues[i];
      }

      return sum;
    }

    private int SortRemoveAndSumValuesWhiteDiceRollForNumberOfCoins(List<int> valuesDiceRoll) {
      var sum = 0;
      valuesDiceRoll.Sort();
      int smaller = valuesDiceRoll[0];
      SmallerValueInWhiteDiceRoll(smaller);
      var tempValues = new List<int>();
      tempValues.AddRange(valuesDiceRoll);
      tempValues.RemoveAt(0);
      for (var i = 0; i < tempValues.Count; i++) {
        sum += tempValues[i];
      }

      sum *= MULTIPLIER;
      return sum;
    }

    private void SmallerValueInWhiteDiceRoll(int smaller) {
      _smallerValue = smaller;
    }
    #endregion
  }
}