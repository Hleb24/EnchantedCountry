namespace Core.Main.Dice {
  /// <summary>
  ///   Интерфейс для работы с бросками характеристик.
  /// </summary>
  public interface IDiceRoll {
    /// <summary>
    ///   Получить бросок характеристики.
    /// </summary>
    /// <param name="qualityRolls">Номер броска харасктеристики.</param>
    /// <returns>Значение броска характеристики.</returns>
    public int GetQualitiesRoll(QualityRolls qualityRolls);

    /// <summary>
    ///   Установить значение броска характеристики.
    /// </summary>
    /// <param name="qualityRolls">Номер броска харасктеристики.</param>
    /// <param name="value">Значение броска характеристики.</param>
    public void SetStatsRoll(QualityRolls qualityRolls, int value);

    /// <summary>
    ///   Изменить  значение броска характеристики.
    /// </summary>
    /// <param name="qualityRolls">Номер броска харасктеристики.</param>
    /// <param name="value">Значение на сколько надо изменить бросок характеристики.</param>
    public void ChangeStatsRoll(QualityRolls qualityRolls, int value);

    /// <summary>
    ///   Получить все броски характеристики.
    /// </summary>
    /// <returns>Броски характеристи.</returns>
    public int[] GetDiceRollValues();

    /// <summary>
    ///   Получить количество бросков характеристики.
    /// </summary>
    /// <returns>Количество бросков характеристики.</returns>
    public int GetNumberOfDiceRolls();

    /// <summary>
    ///   Установить новые значение бросков характеристики.
    /// </summary>
    /// <param name="diceRollValues">Новые значение бросков характеристики.</param>
    public void SetDiceRollValues(int[] diceRollValues);
  }
}