using System;

namespace Core.EnchantedCountry.SupportSystems.Attributes {
  [AttributeUsage(AttributeTargets.Method)]
  public class ButtonAttribute : Attribute {
    public string Name;

    public ButtonAttribute() { }

    public ButtonAttribute(string name) {
      Name = name;
    }
  }
}