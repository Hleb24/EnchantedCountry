using Core.Support.SaveSystem.Scribe;

namespace Core.Support.Data.Equipment {
  public class ScribeHandler {
    public static bool ChangeOrigin<T>(T type, T value, T origin) where T : IScribe {
      return type.IsTracking && value.LastChanged > origin.LastChanged;
    }
  }
}