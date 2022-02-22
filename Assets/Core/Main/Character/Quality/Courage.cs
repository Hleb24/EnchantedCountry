using JetBrains.Annotations;

namespace Core.Main.Character.Quality {
  public class Courage : BaseQuality {
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