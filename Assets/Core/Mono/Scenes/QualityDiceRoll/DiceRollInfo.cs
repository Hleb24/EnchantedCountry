using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core.Mono.Scenes.QualityDiceRoll {
  /// <summary>
  ///   Класс для отображения инфомации о бросках кубиков для качеств персонажа.
  /// </summary>
  public class DiceRollInfo : MonoBehaviour {
    private const string ALL_DICE_ARE_ROLLED = "all dice are rolled";
    private const string VALUES_LOAD = "Info: values load.";
    private const string RESET = "Info: reset.";
    private const string SAVE = "Info: save.";
    private const string DICE_ROLL = "Info: dice roll.";
    private const string START_DICE_ROLL_VALUE = "0";
    private const string SAVES_NOT_FOUND = "Info: save not found.";
    [SerializeField]
    private TMP_Text _info;
    [SerializeField]
    private List<TMP_Text> _diceRollValuesText;

    public void RefreshLoadInfo() {
      _info.text = VALUES_LOAD;
    }

    public void SetDiceRollValuesText(List<string> diceRollValues) {
      for (var i = 0; i < _diceRollValuesText.Count; i++) {
        _diceRollValuesText[i].text = diceRollValues[i];
      }
    }

    public void SetAllDiceRolledForInfo() {
      _info.text += ALL_DICE_ARE_ROLLED;
    }

    public void ResetTexts() {
      _info.text = RESET;
      for (var i = 0; i < _diceRollValuesText.Count; i++) {
        _diceRollValuesText[i].text = START_DICE_ROLL_VALUE;
      }
    }

    public void SetSaveForInfo() {
      _info.text = SAVE;
    }

    public void SetDiceRollForInfo() {
      _info.text = DICE_ROLL;
    }

    public void SetDiceRollTextValues(int index, int diceRollValue) {
      _diceRollValuesText[index].text = diceRollValue.ToString();
    }

    public void SetMustDiceRollInfo() {
      _info.text = SAVES_NOT_FOUND;
    }
  }
}