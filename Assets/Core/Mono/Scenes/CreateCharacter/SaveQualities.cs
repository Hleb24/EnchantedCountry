using Core.Rule.Character.Qualities;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.CreateCharacter {
  public class SaveQualities : MonoBehaviour {
    [SerializeField]
    private bool _usedQualitiesData;
    [Inject]
    private QualitiesAfterDistributing _qualitiesAfterDistributing;
    private Qualities _qualities;
    private IQualityPoints _qualityPoints;

    private void Start() {
      _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
      _qualities = new Qualities(_qualityPoints, new[] { 0, 0, 0, 0, 0 });
      _qualitiesAfterDistributing.Values = new int[5];
      AddListener();
    }

    private void OnDestroy() {
      RemoveListener();
    }

    private void AddListener() {
      ValuesSelectionForQualities.SaveQualities += OnSaveQualites;
    }

    private void RemoveListener() {
      ValuesSelectionForQualities.SaveQualities -= OnSaveQualites;
    }

    private void OnSaveQualites() {
      Invoke(nameof(LoadQualitiesAfterDistributing), 0.3f);
    }

    private void LoadQualitiesAfterDistributing() {
      if (_usedQualitiesData) {
        _qualityPoints = ScribeDealer.Peek<QualityPointsScribe>();
        SetQualitiesInScriptableObject();
      } else {
        // SaveSystem.LoadWithInvoke(_qualitiesAfterDistributing, SaveSystem.Constants.QualitiesAfterDistributing, (nameInvoke, time) => Invoke(nameInvoke, time),
        //   nameof(SetQualitiesInScriptableObject), 0.3f);
      }
    }

    private void SetQualitiesInScriptableObject() {
      Debug.Log("After Load");
      Debug.Log($"{_qualityPoints.GetQualityPoints(QualityType.Strength)} \t" + $"{_qualityPoints.GetQualityPoints(QualityType.Agility)} \t" +
                $"{_qualityPoints.GetQualityPoints(QualityType.Constitution)} \t" + $"{_qualityPoints.GetQualityPoints(QualityType.Wisdom)} \t" +
                $"{_qualityPoints.GetQualityPoints(QualityType.Courage)} \t");
      if (_usedQualitiesData) {
        _qualities[QualityType.Strength].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Strength);
        _qualities[QualityType.Agility].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Agility);
        _qualities[QualityType.Constitution].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Constitution);
        _qualities[QualityType.Wisdom].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Wisdom);
        _qualities[QualityType.Courage].ValueOfQuality = _qualityPoints.GetQualityPoints(QualityType.Courage);
      } else {
        _qualities[QualityType.Strength].ValueOfQuality = _qualitiesAfterDistributing.Values[0];
        _qualities[QualityType.Agility].ValueOfQuality = _qualitiesAfterDistributing.Values[1];
        _qualities[QualityType.Constitution].ValueOfQuality = _qualitiesAfterDistributing.Values[2];
        _qualities[QualityType.Wisdom].ValueOfQuality = _qualitiesAfterDistributing.Values[3];
        _qualities[QualityType.Courage].ValueOfQuality = _qualitiesAfterDistributing.Values[4];
      }
    }
  }
}