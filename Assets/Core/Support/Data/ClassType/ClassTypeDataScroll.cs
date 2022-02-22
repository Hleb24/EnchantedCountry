using System;

namespace Core.Support.Data.ClassType {
  [Serializable]
  public struct ClassTypeDataScroll {
    public string ClassType;

    public ClassTypeDataScroll(Main.Character.Class.ClassType classType) {
      ClassType = classType.ToString();
    }

    internal void SetClassType(Main.Character.Class.ClassType classType) {
      ClassType = classType.ToString();
    }
  }
}