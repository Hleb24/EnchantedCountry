using System;
using Aberrance.Extensions;
using Core.Main.Character.Quality;
using Core.Support.Data.Equipment;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;
using UnityEngine;

namespace Core.Support.Data.QualityPoints {
  /// <summary>
  ///   Класс для хранения данных о очках качеств персонажа.
  /// </summary>
  [Serializable]
  public class QualityPointsScribe : IScribe, IQualityPoints {
    private static QualityPointsScribe _originQualityPointsScribe;
    private QualityPointsDataScroll _qualityPointsDataScroll;

    int IQualityPoints.GetQualityPoints(in QualityType qualityType) {
      return qualityType switch {
               QualityType.Strength => _qualityPointsDataScroll.Strength,
               QualityType.Agility => _qualityPointsDataScroll.Agility,
               QualityType.Constitution => _qualityPointsDataScroll.Constitution,
               QualityType.Wisdom => _qualityPointsDataScroll.Wisdom,
               QualityType.Courage => _qualityPointsDataScroll.Courage,
               _ => default
             };
    }

    void IQualityPoints.SetQualityPoints(in QualityType qualityType, in int qualityPoints) {
      UpdateLastChanged();
      switch (qualityType) {
        case QualityType.Strength:
          _qualityPointsDataScroll.Strength = qualityPoints;
          break;
        case QualityType.Agility:
          _qualityPointsDataScroll.Agility = qualityPoints;
          break;
        case QualityType.Constitution:
          _qualityPointsDataScroll.Constitution = qualityPoints;
          break;
        case QualityType.Wisdom:
          _qualityPointsDataScroll.Wisdom = qualityPoints;
          break;
        case QualityType.Courage:
          _qualityPointsDataScroll.Courage = qualityPoints;
          break;
        default:
          Debug.LogWarning("Типа качества не существует!");
          break;
      }
    }

    void IQualityPoints.ChangeQualityPoints(in QualityType qualityType, in int qualityPoints) {
      UpdateLastChanged();
      switch (qualityType) {
        case QualityType.Strength:
          _qualityPointsDataScroll.Strength += qualityPoints;
          break;
        case QualityType.Agility:
          _qualityPointsDataScroll.Agility += qualityPoints;
          break;
        case QualityType.Constitution:
          _qualityPointsDataScroll.Constitution += qualityPoints;
          break;
        case QualityType.Wisdom:
          _qualityPointsDataScroll.Wisdom += qualityPoints;
          break;
        case QualityType.Courage:
          _qualityPointsDataScroll.Courage += qualityPoints;
          break;
        default:
          Debug.LogWarning("Типа качества не существует!");
          break;
      }
    }

    bool IQualityPoints.EnoughQualityPoints(in QualityType qualityType, in int qualityPoints) {
      return qualityType switch {
               QualityType.Strength => _qualityPointsDataScroll.Strength - qualityPoints >= 0,
               QualityType.Agility => _qualityPointsDataScroll.Agility >= 0,
               QualityType.Constitution => _qualityPointsDataScroll.Constitution >= 0,
               QualityType.Wisdom => _qualityPointsDataScroll.Wisdom >= 0,
               QualityType.Courage => _qualityPointsDataScroll.Courage >= 0,
               _ => default
             };
    }

    public T Clone<T>() {
      return (T)MemberwiseClone();
    }

    public T CloneWithTracking<T>() {
      IsTracking = true;
      return Clone<T>();
    }

    public void ReplaceOriginal<T>(T newOriginValue) {
      if (newOriginValue is QualityPointsScribe qualityPointsScribe) {
        _originQualityPointsScribe = qualityPointsScribe;
      }
    }

    public void ReplaceOriginal() {
      _originQualityPointsScribe = this;
    }

    void IScribe.Init(Scrolls scrolls) {
      _qualityPointsDataScroll = new QualityPointsDataScroll();
      UpdateLastChanged();
      _originQualityPointsScribe = this;
      if (scrolls.IsNull()) {
        return;
      }

      scrolls.QualityPointsDataScroll = _originQualityPointsScribe._qualityPointsDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.QualityPointsDataScroll = _originQualityPointsScribe._qualityPointsDataScroll;
    }

    void IScribe.SaveOnQuit(Scrolls scrolls) {
      bool changeOrigin = ScribeHandler.ChangeOrigin(this, this, _originQualityPointsScribe);
      if (changeOrigin) {
        _originQualityPointsScribe = this;
      }

      IsTracking = false;
      scrolls.QualityPointsDataScroll = _originQualityPointsScribe._qualityPointsDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _qualityPointsDataScroll.Strength = scrolls.QualityPointsDataScroll.Strength;
      _qualityPointsDataScroll.Agility = scrolls.QualityPointsDataScroll.Agility;
      _qualityPointsDataScroll.Constitution = scrolls.QualityPointsDataScroll.Constitution;
      _qualityPointsDataScroll.Wisdom = scrolls.QualityPointsDataScroll.Wisdom;
      _qualityPointsDataScroll.Courage = scrolls.QualityPointsDataScroll.Courage;
      _originQualityPointsScribe = this;
    }

    private void UpdateLastChanged() {
      LastChanged = DateTime.Now;
    }

    public bool IsTracking { get; private set; }

    public DateTime LastChanged { get; private set; }
  }
}