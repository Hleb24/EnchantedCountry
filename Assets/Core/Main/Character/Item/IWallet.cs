namespace Core.Main.Character.Item {
  /// <summary>
  ///   Интерфейс для работы с монетами в кошельке.
  /// </summary>
  public interface IWallet {
    /// <summary>
    ///   Получить монеты, которые в кошельке.
    /// </summary>
    /// <returns>Монеты персонажа.</returns>
    public int GetCoins();

    /// <summary>
    ///   Присвоить количество монет в кошельке.
    /// </summary>
    /// <param name="coins">Новое значение монет.</param>
    public void SetCoins(int coins);

    /// <summary>
    ///   Изменить количество монет в кошельке.
    /// </summary>
    /// <param name="coins">Количество монет, на которое изменятеся значение монет в кошельке.</param>
    public void ChangeCoins(int coins);

    /// <summary>
    ///   Получить максимальное количество монет, которое может носить персонаж.
    /// </summary>
    /// <returns>Максимальное количество монет</returns>
    public int GetMaxCoins();

    /// <summary>
    ///   Установить новое максимальное количество для персонажа.
    /// </summary>
    /// <param name="maxCoins">Новое максимальное количество монет.</param>
    public void SetMaxCoins(int maxCoins);

    /// <summary>
    ///   Хватает ли монет у персонажа.
    /// </summary>
    /// <param name="coins">Нужное количество монет.</param>
    /// <returns>Истина - монет хватает, ложь - монет не хватает.</returns>
    public bool IsCoinsEnough(int coins);
  }
}