using System;

namespace Core.Support.Data.ClassType {
  [Serializable]
  public struct ClassTypeDataScroll {
    public string ClassType;

    public ClassTypeDataScroll(Main.Character.ClassType classType) {
      ClassType = classType.ToString();
    }

    internal void SetClassType(Main.Character.ClassType classType) {
      ClassType = classType.ToString();
    }
  }
}