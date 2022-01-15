using System;
using Core.Rule.Character;
using Core.Rule.Character.Qualities;
using Core.Rule.Dice;
using Core.Support.Data;
using Core.Support.PrefsTools;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class DiceRollForRiskPoints : MonoBehaviour {
    public static event Action DiceRollForRiskPointsCompleted;
    [SerializeField]
    private TMP_Text _numberOfRiskPointsText;
    [SerializeField]
    private Button _diceRollForRiskPointsButton;
    [SerializeField]
    private ClassType _classType;
    [SerializeField]
    private bool _useCharacterTypeForTest;
    [SerializeField]
    private bool _useGameSave;
    private IDealer _dealer;
    private IClassType _type;
    private IRiskPoints _riskPoints;
    private Dices _dice;
    private Qualities _qualities;
    private int _numberOfRiskPoints;

    [Inject]
    private void InjectDealer(IDealer dealer) {
      _dealer = dealer;
    }

    private void Start() {
      LoadData();
    }

    private void OnEnable() {
      _diceRollForRiskPointsButton.onClick.AddListener(OnDiceRollForRiskPointsButtonClicked);
    }

    private void OnDisable() {
      _diceRollForRiskPointsButton.onClick.RemoveListener(OnDiceRollForRiskPointsButtonClicked);
    }

    private void SetCharacterType() {
      if (_useCharacterTypeForTest) {
        return;
      }

      _classType = _type.GetClassType();
    }

    private int GetDiceRollValueForCharacterType() {
      _dice = new SixSidedDice(DiceType.SixEdges);
      var riskPoints = 0;
      switch (_classType) {
        case ClassType.Warrior:
          riskPoints = _dice.RollOfDice(2) + 6;
          break;
        case ClassType.Elf:
          riskPoints = _dice.RollOfDice() + 3;
          break;
        case ClassType.Wizard:
          riskPoints = _dice.RollOfDice() + 4;
          break;
        case ClassType.Kron:
          riskPoints = _dice.RollOfDice(3) + 4;
          break;
        case ClassType.Gnom:
          riskPoints = _dice.RollOfDice(3) + 5;
          break;
      }

      return riskPoints;
    }

    private void OnDiceRollForRiskPointsButtonClicked() {
      Limbo.Enter(PrefsConstants.DICE_ROLL_FOR_RISK_POINTS, PrefsConstants.COMPLETED);
      _numberOfRiskPoints = GetDiceRollValueForCharacterType();
      _numberOfRiskPoints += _qualities[QualityType.Constitution].Modifier;
      SetRiskPointsData(_numberOfRiskPoints);
      SetNumberOfRiskPointsText(_numberOfRiskPointsText, _numberOfRiskPoints);
      DisableDiceRollButton();
      DiceRollForRiskPointsCompleted?.Invoke();
    }

    private void DisableDiceRollButton() {
      _diceRollForRiskPointsButton.gameObject.SetActive(false);
    }

    private void LoadData() {
      if (_useGameSave) {
        _type = _dealer.Peek<IClassType>();
        _riskPoints = _dealer.Peek<IRiskPoints>();
        _qualities = new Qualities(_dealer.Peek<IQualityPoints>());
        Invoke(nameof(SetCharacterType), 0.3f);
      } else {
        // SaveSystem.LoadWithInvoke(_type, SaveSystem.Constants.ClassOfCharacter, (nameInvoke, time) => Invoke(nameInvoke, time), nameof(SetCharacterType), 0.3f);
      }
    }

   

    private void SetRiskPointsData(int riskPoints) {
      _riskPoints.SetRiskPoints(riskPoints);
    }

    private void SetNumberOfRiskPointsText(TMP_Text riskPointsText, int riskPoints) {
      riskPointsText.text = riskPoints.ToString();
    }
  }
}