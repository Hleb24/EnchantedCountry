using Core.Main.NonPlayerCharacters;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Mono.Scenes.Fight {
  public class NpcBuilder {
    private INpcModelSet _npcModelSet;

    public NpcBuilder([NotNull] INpcModelSet npcModelSet) {
      Assert.IsNotNull(npcModelSet, nameof(npcModelSet));
      _npcModelSet = npcModelSet;
    }

    public void ChangeNpcModelSet([NotNull] INpcModelSet npcModelSet) {
      Assert.IsNotNull(npcModelSet, nameof(npcModelSet));
      _npcModelSet = npcModelSet;
    }

    [NotNull]
    public NonPlayerCharacter Build(int id) {
      INpcModel model = _npcModelSet.GetNpcModel(id);
      Assert.IsNotNull(model, nameof(model));

      NpcMetadataModel npcMetadataModel = model.GetNpcMetadataModel();
      NpcMoralityModel npcMoralityModel = model.GetNpcMoralityModel();
      NpcEquipmentsModel npcEquipmentsModel = model.GetNpcEquipmentModel();
      NpcCombatAttributesModel npcCombatAttributesModel = model.GetNpcCombatAttributesModel();

      var npcMetadata = new NpcMetadata(npcMetadataModel);
      var npcMorality = new NpcMorality(npcMoralityModel);
      var npcCombatAttributes = new NpcCombatAttributes(npcCombatAttributesModel);
      var npcEquipments = new NpcEquipments(npcEquipmentsModel);

      var npc = new NonPlayerCharacter(npcMetadata, npcMorality, npcCombatAttributes, npcEquipments);

      return npc;
    }
  }
}