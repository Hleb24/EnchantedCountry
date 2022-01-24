using System;

namespace Core.Main.NonPlayerCharacters {
  public class NpcModel {
    public NpcModel(NpcMoralityModel moralityModel, NpcMetadataModel npcMetadataModel, NpcCombatAttributesModel npcCombatAttributesModel, NpcEquipmentsModel npcEquipmentsModel) {
      MoralityModel = moralityModel ?? throw new ArgumentNullException(nameof(moralityModel));
      MetadataModel = npcMetadataModel ?? throw new ArgumentNullException(nameof(npcMetadataModel));
      CombatAttributesModel = npcCombatAttributesModel ?? throw new ArgumentNullException(nameof(npcCombatAttributesModel));
      EquipmentsModel = npcEquipmentsModel ?? throw new ArgumentNullException(nameof(npcEquipmentsModel));
    }

    public NpcMoralityModel MoralityModel { get; }
    public NpcMetadataModel MetadataModel { get; }
    public NpcCombatAttributesModel CombatAttributesModel { get; }
    public NpcEquipmentsModel EquipmentsModel { get; }
  }
}