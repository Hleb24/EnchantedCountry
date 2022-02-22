namespace Core.Main.GameRule {
  /// <summary>
  ///   Интерфейс для работы с игровыми очками персонажа.
  /// </summary>
  public interface IGamePoints {
    /// <summary>
    ///   Получить игровые очки.
    /// </summary>
    /// <returns>Игровые очки.</returns>
    public int GetPoints();

    /// <summary>
    ///   Присвоить новое значение игровым очкам.
    /// </summary>
    /// <param name="gamePoints">Новое значение игровых очков.</param>
    public void SetPoints(int gamePoints);

    /// <summary>
    ///   Изменить количестов игровых очков.
    /// </summary>
    /// <param name="gamePoints">Значение на которое поменяються игровые очки.</param>
    public void ChangePoints(int gamePoints);

    /// <summary>
    ///   Хватает ли игровых очков.
    /// </summary>
    /// <param name="gamePoints">Контрольное значения игровых очков на проверку.Для проверки оно должно быть положительным.</param>
    /// <returns>Истина - игровых очков хватает, ложь - игровых очков не хватает.</returns>
    public bool EnoughGamePoints(int gamePoints);
  }
}