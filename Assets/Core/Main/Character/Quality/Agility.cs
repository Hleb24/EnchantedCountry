using JetBrains.Annotations;

namespace Core.Main.Character.Quality {
  public class Agility : BaseQuality {
    public Agility([NotNull] IQualityPoints qualityPoints) : base(qualityPoints) { }

    protected override QualityType QualityType {
      get {
        return QualityType.Agility;
      }
    }
  }
}