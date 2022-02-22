using JetBrains.Annotations;

namespace Core.Main.Character.Quality {
  public class Strength : BaseQuality {
    public Strength([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    protected override QualityType QualityType {
      get {
        return QualityType.Strength;
      }
    }
  }
}