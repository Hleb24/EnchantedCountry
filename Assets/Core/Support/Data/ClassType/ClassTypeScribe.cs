using System;
using Aberrance.Extensions;
using Core.Main.Character.Class;
using Core.Support.SaveSystem.SaveManagers;
using Core.Support.SaveSystem.Scribe;

namespace Core.Support.Data.ClassType {
  /// <summary>
  ///   Класс для хранение данных о классе персонажа.
  /// </summary>
  [Serializable]
  public class ClassTypeScribe : IScribe, IClassType {
    private const Main.Character.Class.ClassType START_CLASS = Main.Character.Class.ClassType.Human;
    private ClassTypeDataScroll _classTypeDataScroll;

    Main.Character.Class.ClassType IClassType.GetClassType() {
      return Enum.TryParse(_classTypeDataScroll.ClassType, out Main.Character.Class.ClassType classType) ? classType : START_CLASS;
    }

    void IClassType.SetClassType(Main.Character.Class.ClassType classType) {
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