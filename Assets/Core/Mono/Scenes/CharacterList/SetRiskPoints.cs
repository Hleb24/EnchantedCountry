using System.Globalization;
using Core.Support.Data;
using Core.Support.PrefsTools;
using Core.Support.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;

namespace Core.Mono.Scenes.CharacterList {
  public class SetRiskPoints : MonoBehaviour {
    // ReSharper disable once UnusedMember.Local
    private static bool IsDiceThrownForRiskPoints(int diceRollForRiskPointsPrefs) {
      return diceRollForRiskPointsPrefs == PrefsConstants.COMPLETED;
    }

    private static bool IsDiceNotThrownForRiskPoints(int diceRollForRiskPointsPrefs) {
      return diceRollForRiskPointsPrefs == PrefsConstants.INITIAL;
    }

    [SerializeField]
    private TMP_Text _numberOfRiskPointsText;
    [SerializeField]
    private bool _useRiskPointsDataForTest;
    [SerializeField]
    private bool _useGameSave;
    private IRiskPoints _riskPoints;

    private void Start() {
      Invoke(nameof(LoadRiskPointsData), 0.1f);
    }

    private void OnEnable() {
      DiceRollForRiskPoints.DiceRollForRiskPointsCompleted += OnDiceRollForRiskPointsCompleted;
    }

    private void OnDisable() {
      DiceRollForRiskPoints.DiceRollForRiskPointsCompleted -= OnDiceRollForRiskPointsCompleted;
    }

    private void OnDiceRollForRiskPointsCompleted() {
      LoadData();
    }

    private void LoadRiskPointsData() {
      Limbo.GetOff(PrefsConstants.DICE_ROLL_FOR_RISK_POINTS, out int diceRollForRiskPointsPrefs);
      if (IsDiceNotThrownForRiskPoints(diceRollForRiskPointsPrefs) && !_useRiskPointsDataForTest) {
        return;
      }

      LoadData();
    }

    private void LoadData() {
      if (_useGameSave) {
        _riskPoints = ScribeDealer.Peek<RiskPointsScribe>();
        Invoke(nameof(SetNumberOfRiskPointsText), 0.2f);
      }
    }

    private void SetNumberOfRiskPointsText() {
      float riskPoints = _riskPoints.GetRiskPoints();
      _numberOfRiskPointsText.text = riskPoints.ToString(CultureInfo.InvariantCulture);
    }
  }
}