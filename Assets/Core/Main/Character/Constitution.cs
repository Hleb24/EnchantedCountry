using JetBrains.Annotations;

namespace Core.Main.Character {
  public class Constitution : Quality {
    public Constitution([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    protected override QualityType QualityType {
      get {
        return QualityType.Constitution;
      }
    }
  }
}