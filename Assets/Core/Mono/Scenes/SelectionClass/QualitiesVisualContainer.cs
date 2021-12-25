using System.Collections.Generic;
using Core.SupportSystems.Data;

namespace Core.Mono.Scenes.SelectionClass {
  public class QualitiesVisualContainer {
    private readonly Dictionary<QualityType, QualityVisual> _qualities;

    public QualitiesVisualContainer(List<QualityVisual> qualitiesList) {
      _qualities = new Dictionary<QualityType, QualityVisual>(qualitiesList.Capacity);
      for (var i = 0; i < qualitiesList.Count; i++) {
        _qualities[qualitiesList[i].GetQualityType()] = qualitiesList[i];
      }
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

    public QualityVisual this[QualityType index] {
      get {
        return _qualities[index];
      }
      set {
        _qualities[index] = value;
      }
    }
  }
}