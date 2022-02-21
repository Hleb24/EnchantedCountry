using Core.Support.Data;
using JetBrains.Annotations;

namespace Core.Main.Character {
  public class Agility : Quality {
    public Agility([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    protected override QualityType QualityType {
      get {
        return QualityType.Agility;
      }
    }
  }
}