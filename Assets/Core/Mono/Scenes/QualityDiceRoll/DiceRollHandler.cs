using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.QualityDiceRoll {
  /// <summary>
  ///   Класс отвечает за броски костей для качества.
  /// </summary>
  public class DiceRollHandler : MonoBehaviour {
    [SerializeField]
    private QualitiesSelector _qualitiesSelector;
    [SerializeField]
    private Button _diceRollButton;
    [SerializeField]
    private Button _reset;
    [SerializeField]
    private Button _save;
    [SerializeField]
    private Button _load;

    private QualityDiceRoll _qualityDiceRoll;

    private void OnEnable() {
      AddListener();
    }

    private void OnDisable() {
      RemoveListener();
    }

    [Inject, UsedImplicitly]
    public void Constructor(QualityDiceRoll qualityDiceRoll) {
      _qualityDiceRoll = qualityDiceRoll;
    }

    private void AddListener() {
      _diceRollButton.onClick.AddListener(OnDiceRollButtonClicked);
      _load.onClick.AddListener(OnLoadClicked);
      _reset.onClick.AddListener(OnResetClicked);
      _save.onClick.AddListener(OnSaveClicked);
      _qualityDiceRoll.UseDiceRollWithSave += _qualitiesSelector.EnableDistribute;
      _qualitiesSelector.AllValuesSelected += OnAllValuesSelected;
      _qualitiesSelector.DistributeValues += OnDistributeValues;
    }

    private void RemoveListener() {
      _diceRollButton.onClick.RemoveListener(OnDiceRollButtonClicked);
      _load.onClick.RemoveListener(OnLoadClicked);
      _reset.onClick.RemoveListener(OnResetClicked);
      _save.onClick.RemoveListener(OnSaveClicked);
      _qualityDiceRoll.UseDiceRollWithSave -= _qualitiesSelector.EnableDistribute;
      _qualitiesSelector.AllValuesSelected -= OnAllValuesSelected;
      _qualitiesSelector.DistributeValues -= OnDistributeValues;
    }

    private void OnResetClicked() {
      _qualityDiceRoll.ResetValuesOfDiceRoll();
      _qualitiesSelector.DisableUI();
    }

    private void OnLoadClicked() {
      _qualityDiceRoll.LoadAndSetDiceRollData();
    }

    private void OnSaveClicked() {
      _qualityDiceRoll.Save();
      _qualitiesSelector.EnableDistribute();
    }

    private void OnDiceRollButtonClicked() {
      _qualityDiceRoll.SetDiceRollValuesAndIncreaseCount();
    }

    private void OnDistributeValues() {
      DisableButtonInteractable();
    }

    private void OnAllValuesSelected() {
      DisableButtonInteractable();
    }

    private void DisableButtonInteractable() {
      for (var i = 0; i < Buttons.Length; i++) {
        Buttons[i].interactable = false;
      }
    }

    private Button[] Buttons {
      get {
        return new[] { _diceRollButton, _load, _save, _reset };
      }
    }
  }
}