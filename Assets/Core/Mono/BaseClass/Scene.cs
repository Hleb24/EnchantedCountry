namespace Core.Mono.BaseClass {
  public enum Scene {
    /// <summary>
    ///   Начальная сцена для загрузки данных игровой сессии.
    /// </summary>
    Intro,
    /// <summary>
    ///   Сцена бросков кубиков для качеств.
    /// </summary>
    DiceRolls,
    /// <summary>
    ///   Сцена выбора класса персонажа.
    /// </summary>
    SelectClass,
    /// <summary>
    ///   Сцена повышение качеств для класса - Волшебник.
    /// </summary>
    WizardImprovement,
    /// <summary>
    ///   Сцена повышение качеств для класса - Крон.
    /// </summary>
    KronImprovement,
    /// <summary>
    ///   Сцена стартового магазина Трурля.
    /// </summary>
    TrurlsShop,
    /// <summary>
    ///   Сцена листа персонажа.
    /// </summary>
    CharacterList,
    /// <summary>
    ///   Сцена битвы.
    /// </summary>
    Fight
  }
}