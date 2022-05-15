namespace Core.Main.GameRule.Equipment {
  public interface ICloneable {
    public T Clone<T>();
    public T CloneWithTracking<T>();
    public void ReplaceOriginal<T>(T newOriginValue);
    public void ReplaceOriginal();
    public bool IsTracking { get; }
  }

  /// <summary>
  ///   Интерфейс для работы с экипированым снаряжение.
  /// </summary>
  public interface IEquipmentUsed : ICloneable {
    /// <summary>
    ///   Присвоить новое значение индефикатора эикипрированого снаяржения.
    /// </summary>
    /// <param name="type">Тип экиприованого снаряжения.</param>
    /// <param name="newId">Новый индефикатор для экипированого снаряжения.</param>
    public void SetEquipment(EquipmentsUsedId type, int newId);

    /// <summary>
    ///   Получить значение индефикатора экипрированого снаяржения.
    /// </summary>
    /// <param name="type">Тип экиприованого снаряжения.</param>
    /// <returns>Индефикатора эикипрированого снаяржения.</returns>
    public int GetEquipment(EquipmentsUsedId type);
  }
}