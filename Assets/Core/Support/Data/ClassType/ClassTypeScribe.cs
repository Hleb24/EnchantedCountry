using System;
using Aberrance.Extensions;
using Core.Main.Character;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;

namespace Core.Support.Data.ClassType {
  /// <summary>
  ///   Класс для хранение данных о классе персонажа.
  /// </summary>
  [Serializable]
  public class ClassTypeScribe : IScribe, IClassType {
    private const Main.Character.ClassType START_CLASS = Main.Character.ClassType.Human;
    private ClassTypeDataScroll _classTypeDataScroll;

    Main.Character.ClassType IClassType.GetClassType() {
      return Enum.TryParse(_classTypeDataScroll.ClassType, out Main.Character.ClassType classType) ? classType : START_CLASS;
    }

    void IClassType.SetClassType(Main.Character.ClassType classType) {
      _classTypeDataScroll.SetClassType(classType);
    }

    void IScribe.Init(Scrolls scrolls) {
      _classTypeDataScroll = new ClassTypeDataScroll(START_CLASS);
      if (scrolls.Null()) {
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
}