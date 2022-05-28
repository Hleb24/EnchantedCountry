using System.Collections.Generic;
using Core.Main.Character.Quality;
using Core.Mono.MainManagers;
using Core.SO.Mock;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Mono.Scenes.QualitiesImprovement {
  public class QualitiesTexts : MonoBehaviour {
    [SerializeField]
    private List<TMP_Text> _listOfValues;
    private ILauncher _launcher;
    private IQualityPoints _qualityPoints;
    private IQualityPoints _mockQualitiesPoints;

    private void Awake() {
      _mockQualitiesPoints = Resources.Load<MockQualitiesPoints>(MockQualitiesPoints.PATH);
    }

    private void Start() {
      SetQualitiesText();
    }

    [Inject]
    public void Constructor(ILauncher launcher, IQualityPoints qualityPoints) {
      _launcher = launcher;
      _qualityPoints = qualityPoints;
    }

    public void SetQualitiesText() {
      if (_launcher.UseGameSave()) {
        _listOfValues[0].text = _qualityPoints.GetQualityPoints(QualityType.Strength).ToString();
        _listOfValues[1].text = _qualityPoints.GetQualityPoints(QualityType.Agility).ToString();
        _listOfValues[2].text = _qualityPoints.GetQualityPoints(QualityType.Constitution).ToString();
        _listOfValues[3].text = _qualityPoints.GetQualityPoints(QualityType.Wisdom).ToString();
        _listOfValues[4].text = _qualityPoints.GetQualityPoints(QualityType.Courage).ToString();
      } else {
        _listOfValues[0].text = _mockQualitiesPoints.GetQualityPoints(QualityType.Strength).ToString();
        _listOfValues[1].text = _mockQualitiesPoints.GetQualityPoints(QualityType.Agility).ToString();
        _listOfValues[2].text = _mockQualitiesPoints.GetQualityPoints(QualityType.Constitution).ToString();
        _listOfValues[3].text = _mockQualitiesPoints.GetQualityPoints(QualityType.Wisdom).ToString();
        _listOfValues[4].text = _mockQualitiesPoints.GetQualityPoints(QualityType.Courage).ToString();
      }
    }
  }
}