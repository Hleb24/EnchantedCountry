using System.Globalization;
using Core.Mono.MainManagers;
using Core.Support.Data;
using Core.Support.PrefsTools;
using TMPro;
using UnityEngine;
using Zenject;

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
    private IStartGame _startGame;
    private IRiskPoints _riskPoints;

    [Inject]
    public void Constructor(IStartGame  startGame, IRiskPoints riskPoints) {
      _startGame = startGame;
      _riskPoints = riskPoints;
    }
   

    private void Start() {
      LoadRiskPointsData();
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