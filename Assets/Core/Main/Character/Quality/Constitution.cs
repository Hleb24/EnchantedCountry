using JetBrains.Annotations;

namespace Core.Main.Character.Quality {
  public class Constitution : BaseQuality {
    public Constitution([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    protected override QualityType QualityType {
      get {
        return QualityType.Constitution;
      }
    }
  }
}