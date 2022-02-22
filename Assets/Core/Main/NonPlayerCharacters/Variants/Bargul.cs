using Core.Main.GameRule.Impact;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters.Variants {
  public class Bargul : NonPlayerCharacter {
    public Bargul([NotNull] NpcMetadata npcMetadata, [NotNull] NpcMorality npcMorality, [NotNull] NpcCombatAttributes npcCombatAttributes, [NotNull] NpcEquipments npcEquipments) :
      base(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments) { }

    protected override void ToDamagedOfImpact(int diceRoll, IImpactOnRiskPoints character, int indexOfImpact) {
      if (_npcCombatAttributes.CanUseImpact(diceRoll, indexOfImpact)) {
        _npcCombatAttributes.UseImpact(character, indexOfImpact);
      }
    }
  }
}