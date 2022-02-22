namespace Core.Main.Character.Class {
  /// <summary>
  ///   Интерфейс для работы с классом персонажа.
  /// </summary>
  public interface IClassType {
    /// <summary>
    ///   Получить название класса персонажа;
    /// </summary>
    /// <returns></returns>
    public ClassType GetClassType();

    /// <summary>
    ///   Присвоить новый класс персонажу.
    /// </summary>
    /// <param name="classType">Новый класс персонажа.</param>
    public void SetClassType(ClassType classType);
  }
}