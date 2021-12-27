using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core.Mono.Scenes.QualityDiceRoll {
  /// <summary>
  ///   Класс для отображения инфомации о бросках кубиков для качеств персонажа.
  /// </summary>
  public class DiceRollInfo : MonoBehaviour {
    private const string AllDiceAreRolled = "all dice are rolled";
    private const string ValuesLoad = "Info: values load.";
    private const string Reset = "Info: reset.";
    private const string Save = "Info: save.";
    private const string DiceRoll = "Info: dice roll.";
    private const string StartDiceRollValue = "0";
    [SerializeField]
    private TMP_Text _info;
    [SerializeField]
    private List<TMP_Text> _diceRollValuesText;

    public void SetValueLoadForInfo() {
      _info.text = ValuesLoad;
    }

    public void SetDiceRollValuesText(List<string> diceRollValues) {
      for (var i = 0; i < _diceRollValuesText.Count; i++) {
        _diceRollValuesText[i].text = diceRollValues[i];
      }
    }

    public void SetAllDiceRolledForInfo() {
      _info.text += AllDiceAreRolled;
    }

    public void ResetTexts() {
      _info.text = Reset;
      for (var i = 0; i < _diceRollValuesText.Count; i++) {
        _diceRollValuesText[i].text = StartDiceRollValue;
      }
    }

    public void SetSaveForInfo() {
      _info.text = Save;
    }

    public void SetDiceRollForInfo() {
      _info.text = DiceRoll;
    }

    public void SetDiceRollTextValues(int index, int diceRollValue) {
      _diceRollValuesText[index].text = diceRollValue.ToString();
    }
  }
}