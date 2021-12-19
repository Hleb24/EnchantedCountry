using System;
using UnityEngine;

namespace Core.EnchantedCountry.SupportSystems.Data {
  /// <summary>
  ///   Перечисление качеств персонажа.
  /// </summary>
  public enum QualityType {
    Strength,
    Agility,
    Constitution,
    Wisdom,
    Courage
  }

  /// <summary>
  ///   Интерфейс для работы с очками качеств.
  /// </summary>
  public interface IQualityPoints {
    /// <summary>
    ///   Получить очки качества.
    /// </summary>
    /// <param name="qualityType">Тип качества.</param>
    /// <returns>Очки качества.</returns>
    public int GetQualityPoints(in QualityType qualityType);

    /// <summary>
    ///   Присвоить значение очков качеству.
    /// </summary>
    /// <param name="qualityType">Тип качества.</param>
    /// <param name="qualityPoints">Новое значение очков качеста.</param>
    public void SetQualityPoints(in QualityType qualityType, in int qualityPoints);

    /// <summary>
    ///   Изменить количество очков для качества.
    /// </summary>
    /// <param name="qualityType">Тип качества.</param>
    /// <param name="qualityPoints">Значение на которое надо изменить очки качества.</param>
    public void ChangeQualityPoints(in QualityType qualityType, in int qualityPoints);

    /// <summary>
    ///   Хватает ли очков качества.
    /// </summary>
    /// <param name="qualityType">Тип качества.</param>
    /// <param name="qualityPoints">Контрольное значение очков качества, которое должно быть положительным.</param>
    /// <returns>Истина - очков качества хватает, ложь - очков качества не хватает.</returns>
    public bool EnoughQualityPoints(in QualityType qualityType, in int qualityPoints);
  }

  /// <summary>
  ///   Класс для хранения данных о очках качеств персонажа.
  /// </summary>
  [Serializable]
  public class QualityPointsScribe : IScribe, IQualityPoints {
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

    void IScribe.Init(Scrolls scrolls) {
      _qualityPointsDataScroll = new QualityPointsDataScroll();
      if (scrolls is null) {
        return;
      }

      scrolls.QualityPointsDataScroll = _qualityPointsDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.QualityPointsDataScroll = _qualityPointsDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _qualityPointsDataScroll.Strength = scrolls.QualityPointsDataScroll.Strength;
      _qualityPointsDataScroll.Agility = scrolls.QualityPointsDataScroll.Agility;
      _qualityPointsDataScroll.Constitution = scrolls.QualityPointsDataScroll.Constitution;
      _qualityPointsDataScroll.Wisdom = scrolls.QualityPointsDataScroll.Wisdom;
      _qualityPointsDataScroll.Courage = scrolls.QualityPointsDataScroll.Courage;
    }
  }

  [Serializable]
  public struct QualityPointsDataScroll {
    public int Strength;
    public int Agility;
    public int Constitution;
    public int Wisdom;
    public int Courage;

    public QualityPointsDataScroll(int strength = 0, int agility = 0, int constitution = 0, int wisdom = 0, int courage = 0) {
      Strength = strength;
      Agility = agility;
      Constitution = constitution;
      Wisdom = wisdom;
      Courage = courage;
    }
  }
}