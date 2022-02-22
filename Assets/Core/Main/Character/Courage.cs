using JetBrains.Annotations;

namespace Core.Main.Character {
  public class Courage : Quality {
    public Courage([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    public override int GetModifier() {
      return 0;
    }

    protected override QualityType QualityType {
      get {
        return QualityType.Courage;
      }
    }
  }
}