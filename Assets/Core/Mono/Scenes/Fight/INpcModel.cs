using Core.Main.NonPlayerCharacters.Model;
using JetBrains.Annotations;

namespace Core.Mono.Scenes.Fight {
  public interface INpcModel {
    [MustUseReturnValue]
    public NpcMetadataModel GetNpcMetadataModel();

    [MustUseReturnValue]
    public NpcMoralityModel GetNpcMoralityModel();

    [MustUseReturnValue]
    public NpcCombatAttributesModel GetNpcCombatAttributesModel();

    [MustUseReturnValue]
    public NpcEquipmentsModel GetNpcEquipmentModel();
  }
}