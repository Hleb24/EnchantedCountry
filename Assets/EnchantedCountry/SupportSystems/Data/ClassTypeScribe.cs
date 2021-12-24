using System;
using Core.EnchantedCountry.CoreEnchantedCountry.Character;
using Core.EnchantedCountry.SupportSystems.SaveSystem.SaveManagers;
using Core.EnchantedCountry.SupportSystems.SaveSystem.Scribe;

namespace Core.EnchantedCountry.SupportSystems.Data {
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

  /// <summary>
  ///   Класс для хранение данных о классе персонажа.
  /// </summary>
  [Serializable]
  public class ClassTypeScribe : IScribe, IClassType {
    private const ClassType StartClass = ClassType.Human;
    private ClassTypeDataScroll _classTypeDataScroll;

    ClassType IClassType.GetClassType() {
      return Enum.TryParse(_classTypeDataScroll.ClassType, out ClassType classType) ? classType : StartClass;
    }

    void IClassType.SetClassType(ClassType classType) {
      _classTypeDataScroll.SetClassType(classType);
    }

    void IScribe.Init(Scrolls scrolls) {
      _classTypeDataScroll = new ClassTypeDataScroll(StartClass);
      if (scrolls is null) {
        return;
      }

      scrolls.ClassTypeDataScroll = _classTypeDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.ClassTypeDataScroll = _classTypeDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _classTypeDataScroll.ClassType = scrolls.ClassTypeDataScroll.ClassType;
    }
  }

  [Serializable]
  public struct ClassTypeDataScroll {
    public string ClassType;

    public ClassTypeDataScroll(ClassType classType) {
      ClassType = classType.ToString();
    }

    internal void SetClassType(ClassType classType) {
      ClassType = classType.ToString();
    }
  }
}