using System;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.NonPlayerCharacters {
  public class NpcModel {
    public NpcModel([NotNull] NpcMoralityModel moralityModel, [NotNull] NpcMetadataModel npcMetadataModel, [NotNull] NpcCombatAttributesModel npcCombatAttributesModel,
      [NotNull] NpcEquipmentsModel npcEquipmentsModel) {
      Assert.IsNotNull(moralityModel, nameof(moralityModel));
      Assert.IsNotNull(npcMetadataModel, nameof(npcMetadataModel));
      Assert.IsNotNull(npcCombatAttributesModel, nameof(npcCombatAttributesModel));
      Assert.IsNotNull(npcEquipmentsModel, nameof(npcEquipmentsModel));
      MoralityModel = moralityModel;
      MetadataModel = npcMetadataModel;
      CombatAttributesModel = npcCombatAttributesModel;
      EquipmentsModel = npcEquipmentsModel;
    }

    public NpcMoralityModel MoralityModel { get; }
    public NpcMetadataModel MetadataModel { get; }
    public NpcCombatAttributesModel CombatAttributesModel { get; }
    public NpcEquipmentsModel EquipmentsModel { get; }
  }
}