using System;

namespace Core.Support.Attributes {
  [AttributeUsage(AttributeTargets.Method)]
  public class ButtonAttribute : Attribute {
    public string Name;

    public ButtonAttribute() { }

    public ButtonAttribute(string name) {
      Name = name;
    }
  }
}