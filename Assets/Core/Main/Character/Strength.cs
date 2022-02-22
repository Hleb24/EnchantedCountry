using JetBrains.Annotations;

namespace Core.Main.Character {
  public class Strength : Quality {
    public Strength([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    protected override QualityType QualityType {
      get {
        return QualityType.Strength;
      }
    }
  }
}