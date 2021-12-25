namespace Core.Mono.Scenes.QualitiesImprovement {
  public class Wizard : Improvement {
    protected override void DiceRollsQualityIncrease() {
      QualityIncrease.DiceRollQualityIncreaseForWizard();
    }
  }
}
