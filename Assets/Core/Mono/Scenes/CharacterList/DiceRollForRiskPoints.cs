using System;
using Core.Main.Character.Class;
using Core.Main.Character.Quality;
using Core.Main.GameRule.Point;
using Core.Mono.MainManagers;
using Core.Support.PrefsTools;
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
    private ClassType _classTypeEnum;
    [SerializeField]
    private bool _useCharacterTypeForTest;
    private ILauncher _launcher;
    private IClassType _classType;
    private IRiskPoints _riskPoints;
    private Qualities _qualities;
    private CharacterClass _characterClass;
    private int _numberOfRiskPoints;

    private void Start() {
      LoadData();
    }

    private void OnEnable() {
      _diceRollForRiskPointsButton.onClick.AddListener(OnDiceRollForRiskPointsButtonClicked);
    }

    private void OnDisable() {
      _diceRollForRiskPointsButton.onClick.RemoveListener(OnDiceRollForRiskPointsButtonClicked);
    }

    [Inject]
    public void Constructor(ILauncher launcher, IClassType classType, IRiskPoints riskPoints, Qualities qualities) {
      _launcher = launcher;
      _classType = classType;
      _riskPoints = riskPoints;
      _qualities = qualities;
    }

    private void SetCharacterType() {
      if (_useCharacterTypeForTest) {
        _characterClass = CharacterClassBuilder.GetCharacterClass(_classTypeEnum);
        return;
      }

      _characterClass = CharacterClassBuilder.GetCharacterClass(_classType);
    }

    private int GetDiceRollValueForCharacterType() {
      return _characterClass.GetFirstRiskPoint();
    }

    private void OnDiceRollForRiskPointsButtonClicked() {
      Limbo.Enter(PrefsConstants.DICE_ROLL_FOR_RISK_POINTS, PrefsConstants.COMPLETED);
      _numberOfRiskPoints = GetDiceRollValueForCharacterType();
      _numberOfRiskPoints += _qualities.GetModifierOf(QualityType.Constitution);
      SetRiskPointsData(_numberOfRiskPoints);
      SetNumberOfRiskPointsText(_numberOfRiskPointsText, _numberOfRiskPoints);
      DisableDiceRollButton();
      DiceRollForRiskPointsCompleted?.Invoke();
    }

    private void DisableDiceRollButton() {
      _diceRollForRiskPointsButton.gameObject.SetActive(false);
    }

    private void LoadData() {
      if (_launcher.UseGameSave()) {
        SetCharacterType();
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