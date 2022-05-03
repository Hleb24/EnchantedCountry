using Core.Main.GameRule.Equipment;
using Core.Support.Attributes;
using Core.Support.Data.Equipment;
using UnityEngine;
using Zenject;

namespace Aberrance {
  public class TestClone : MonoBehaviour {
    public IEquipmentUsed _equipmentUsedClone;
    public IEquipmentUsed _equipmentUsedOrigin;
    public int newId = 120;

    [Inject]
    public void Constructor(IEquipmentUsed equipmentUsed) {
      _equipmentUsedOrigin = equipmentUsed;
      _equipmentUsedClone = equipmentUsed.Clone<EquipmentUsedScribe>();
    }

    [Button]
    public void ChangeArmor() {
      int armorId = _equipmentUsedOrigin.GetEquipment(EquipmentsUsedId.ArmorId);
      Debug.LogWarning($"Origin armor {_equipmentUsedOrigin.GetEquipment(EquipmentsUsedId.ArmorId)}");
      _equipmentUsedClone.SetEquipment(EquipmentsUsedId.ArmorId, newId);
      _equipmentUsedOrigin.SetEquipment(EquipmentsUsedId.ArmorId, 0);
      _equipmentUsedOrigin.ReplaceOriginal();
      Debug.LogWarning($"Origin armor {_equipmentUsedOrigin.GetEquipment(EquipmentsUsedId.ArmorId)}");
      Debug.LogWarning($"Clone armor {_equipmentUsedClone.GetEquipment(EquipmentsUsedId.ArmorId)}");
    }

    [Button]
    public void ChangeArmorClone() {
      _equipmentUsedOrigin.SetEquipment(EquipmentsUsedId.ArmorId, 200);
    }
  }
}