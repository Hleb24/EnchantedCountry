using System.Globalization;
using Core.Main.GameRule;
using Core.Mono.MainManagers;
using Core.Support.PrefsTools;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.CharacterList {
  public class SetRiskPoints : MonoBehaviour {
    private static bool IsDiceNotThrownForRiskPoints(int diceRollForRiskPointsPrefs) {
      return diceRollForRiskPointsPrefs == PrefsConstants.INITIAL;
    }

    [SerializeField]
    private TMP_Text _numberOfRiskPointsText;
    [SerializeField]
    private bool _useRiskPointsDataForTest;
    private IStartGame _startGame;
    private IRiskPoints _riskPoints;

    private void Start() {
      LoadRiskPointsData();
    }

    private void OnEnable() {
      DiceRollForRiskPoints.DiceRollForRiskPointsCompleted += OnDiceRollForRiskPointsCompleted;
    }

    private void OnDisable() {
      DiceRollForRiskPoints.DiceRollForRiskPointsCompleted -= OnDiceRollForRiskPointsCompleted;
    }

    [Inject]
    public void Constructor(IStartGame startGame, IRiskPoints riskPoints) {
      _startGame = startGame;
      _riskPoints = riskPoints;
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
      if (_startGame.UseGameSave()) {
        SetNumberOfRiskPointsText();
      }
    }

    private void SetNumberOfRiskPointsText() {
      float riskPoints = _riskPoints.GetRiskPoints();
      _numberOfRiskPointsText.text = riskPoints.ToString(CultureInfo.InvariantCulture);
    }
  }
}