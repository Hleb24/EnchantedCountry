using System;
using Aberrance.Extensions;
using Core.Main.Character.Class;
using Core.Support.Data.Equipment;
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
    private ClassTypeScribe _originClassTypeScribe;

    Main.Character.Class.ClassType IClassType.GetClassType() {
      return Enum.TryParse(_classTypeDataScroll.ClassType, out Main.Character.Class.ClassType classType) ? classType : START_CLASS;
    }

    void IClassType.SetClassType(Main.Character.Class.ClassType classType) {
      UpdateLastChanged();
      _classTypeDataScroll.SetClassType(classType);
    }

    public T Clone<T>() {
      return (T)MemberwiseClone();
    }

    public T CloneWithTracking<T>() {
      IsTracking = true;
      return Clone<T>();
    }

    public void ReplaceOriginal<T>(T newOriginValue) {
      if (newOriginValue is ClassTypeScribe classTypeScribe) {
        _originClassTypeScribe = classTypeScribe;
      }
    }

    public void ReplaceOriginal() {
      _originClassTypeScribe = this;
    }

    void IScribe.Init(Scrolls scrolls) {
      _classTypeDataScroll = new ClassTypeDataScroll(START_CLASS);
      UpdateLastChanged();
      _originClassTypeScribe = this;
      if (scrolls.IsNull()) {
        return;
      }

      scrolls.ClassTypeDataScroll = _originClassTypeScribe._classTypeDataScroll;
    }

    void IScribe.Save(Scrolls scrolls) {
      scrolls.ClassTypeDataScroll = _originClassTypeScribe._classTypeDataScroll;
    }

    void IScribe.SaveOnQuit(Scrolls scrolls) {
      bool changeOrigin = ScribeHandler.ChangeOrigin(this, this, _originClassTypeScribe);
      if (changeOrigin) {
        _originClassTypeScribe = this;
      }

      IsTracking = false;
      scrolls.ClassTypeDataScroll = _originClassTypeScribe._classTypeDataScroll;
    }

    void IScribe.Loaded(Scrolls scrolls) {
      _classTypeDataScroll.ClassType = scrolls.ClassTypeDataScroll.ClassType;
      _originClassTypeScribe = this;
    }

    private void UpdateLastChanged() {
      LastChanged = DateTime.Now;
    }

    public DateTime LastChanged { get; private set; }

    public bool IsTracking { get; private set; }
  }
}