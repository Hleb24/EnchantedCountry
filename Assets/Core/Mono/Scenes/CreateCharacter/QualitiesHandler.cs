using Core.Rule.Character.Qualities;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using UnityEngine;

namespace Core.Mono.Scenes.CreateCharacter {
  public class QualitiesHandler : MonoBehaviour {
    private Qualities _qualities;
    private IQualityPoints _qualityPoints;


    private void Start() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      _qualities = new Qualities(_qualityPoints, new[] { 0, 0, 0, 0, 0 });
      AddListener();
    }

    private void OnDestroy() {
      RemoveListener();
    }

    private void AddListener() {
      QualitiesSelector.SaveQualities += OnSaveQualites;
    }

    private void RemoveListener() {
      QualitiesSelector.SaveQualities -= OnSaveQualites;
    }

    private void OnSaveQualites() {
      Invoke(nameof(LoadQualitiesAfterDistributing), 0.3f);
    }

    private void LoadQualitiesAfterDistributing() {
      SetQualities();
    }

    private void SetQualities() {
      _qualities[QualityType.Strength].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Strength);
      _qualities[QualityType.Agility].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Agility);
      _qualities[QualityType.Constitution].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Constitution);
      _qualities[QualityType.Wisdom].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Wisdom);
      _qualities[QualityType.Courage].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Courage);
    }
  }
}