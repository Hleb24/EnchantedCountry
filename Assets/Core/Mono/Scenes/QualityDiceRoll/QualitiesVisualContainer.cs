using System.Collections.Generic;
using Core.Main.Character.Quality;
using UnityEngine;

namespace Core.Mono.Scenes.QualityDiceRoll {
  /// <summary>
  ///   Класс для работы с коллекцией <see cref="QualityVisual" />.
  /// </summary>
  public class QualitiesVisualContainer : MonoBehaviour {
    [SerializeField]
    private List<QualityVisual> _qualityVisuals;

    private Dictionary<QualityType, QualityVisual> _qualities;

    private void Awake() {
      InitVisual(_qualityVisuals);
    }

    public void EnableButtons() {
      foreach (QualityVisual qualityVisual in _qualities.Values) {
        qualityVisual.EnableButtons();
      }
    }

    public void DisableButtons() {
      foreach (QualityVisual qualityVisual in _qualities.Values) {
        qualityVisual.DisableButtons();
      }
    }

    public void RemoveListeners() {
      foreach (QualityVisual qualityVisual in _qualities.Values) {
        qualityVisual.RemoveListeners();
      }
    }

    private void InitVisual(List<QualityVisual> qualitiesList) {
      _qualities = new Dictionary<QualityType, QualityVisual>(qualitiesList.Capacity);
      for (var i = 0; i < qualitiesList.Count; i++) {
        _qualities[qualitiesList[i].GetQualityType()] = qualitiesList[i];
      }
    }

    public QualityVisual this[QualityType index] {
      get {
        return _qualities[index];
      }
    }
  }
}