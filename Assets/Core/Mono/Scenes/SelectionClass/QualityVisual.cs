using System;
using Core.SupportSystems.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Core.Mono.Scenes.SelectionClass {
  public class QualityVisual : MonoBehaviour {
    [SerializeField]
    private QualityType _qualityType;
    [SerializeField]
    private TMP_Text _qualityValue;
    [SerializeField]
    private Button _nextValue;
    [SerializeField]
    private Button _previousValue;
    [SerializeField]
    private Button _accept;

    public QualityType GetQualityType() {
      return _qualityType;
    }

    public TMP_Text GetQualityValueText() {
      Assert.IsNotNull(_qualityValue);
      return _qualityValue;
    }

    public void AddNextPreviousListener(Action nextAction, Action previousAction) {
      Assert.IsNotNull(nextAction);
      Assert.IsNotNull(previousAction);
      AddNextListener(nextAction);
      AddPreviousListener(previousAction);
    }

    public bool IsAcceptEnable() {
      return _accept.interactable;
    }

    public void EnableButtons() {
      _nextValue.interactable = true;
      _previousValue.interactable = true;
      _accept.interactable = true;
    }

    public void DisableButtons() {
      _nextValue.interactable = false;
      _previousValue.interactable = false;
      _accept.interactable = false;
    }

    public void AddAcceptListener(Action action) {
      Assert.IsNotNull(action);
      _accept.onClick.AddListener(DisableNextPreviousButtons);
      _accept.onClick.AddListener(() => action());
    }

    public void RemoveListeners() {
      _nextValue.onClick.RemoveAllListeners();
      _previousValue.onClick.RemoveAllListeners();
      _accept.onClick.RemoveAllListeners();
    }

    public Button GetAcceptButton() {
      return _accept;
    }

    private void DisableNextPreviousButtons() {
      _nextValue.interactable = false;
      _previousValue.interactable = false;
    }

    private void AddNextListener(Action action) {
      Assert.IsNotNull(action);
      _nextValue.onClick.AddListener(() => action());
    }

    private void AddPreviousListener(Action action) {
      Assert.IsNotNull(action);
      _previousValue.onClick.AddListener(() => action());
    }
  }
}