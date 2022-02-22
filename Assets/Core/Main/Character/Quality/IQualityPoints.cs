namespace Core.Main.Character.Quality {
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
}