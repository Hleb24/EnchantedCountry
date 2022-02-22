namespace Core.Mono.Scenes.QualitiesImprovement {
  public class Kron : Improvement {
    protected override void DiceRollsQualityIncrease() {
      QualityIncrease.IncreaseQualitiesForKron();
    }
  }
}