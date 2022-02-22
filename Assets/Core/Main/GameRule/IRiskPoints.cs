namespace Core.Main.GameRule {
  /// <summary>
  ///   Интерфейс для работы с очками риска персонажа.
  /// </summary>
  public interface IRiskPoints {
    /// <summary>
    ///   Получить очки риска персонажа.
    /// </summary>
    /// <returns>Очки риска.</returns>
    public float GetRiskPoints();

    /// <summary>
    ///   Присвоить значение очкам риска.
    /// </summary>
    /// <param name="riskPoints">Новое значение очков риска.</param>
    public void SetRiskPoints(float riskPoints);

    /// <summary>
    ///   Изменить значение очков риска.
    /// </summary>
    /// <param name="riskPoints">Значение, на которое изменяется количество очков риска.</param>
    public void ChangeRiskPoints(float riskPoints);

    /// <summary>
    ///   Хватает ли очков риска.
    /// </summary>
    /// <param name="riskPoints">Котрольное значение очков риска, которое должно быть положительным.</param>
    /// <returns>Истина - очков риска хватает, ложь - очков риска не хватает.</returns>
    public bool EnoughRiskPoints(float riskPoints);
  }
}