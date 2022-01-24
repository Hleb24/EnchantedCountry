using System;
using UnityEngine;

namespace Core.SO.Armor {
  [Serializable, CreateAssetMenu(fileName = "New Armor", menuName = "Armor", order = 53)]
  public class ArmorSO : UnityEngine.ScriptableObject {
    public Main.GameRule.Armor.ArmorType armorType = Main.GameRule.Armor.ArmorType.None;
    public string armorName = "";
    public int classOfArmor;
    public string effectName = "";
    public Main.GameRule.Armor armor;
    public int id;

    public void OnEnable() {
      InitArmor();
    }

    public void OnValidate() {
      InitArmor();
    }

    public Main.GameRule.Armor InitArmor() {
      if (armor == null) {
        armor = new Main.GameRule.Armor(armorName, classOfArmor, armorType, effectName);
      } else {
        armor.Init(armorName, classOfArmor, armorType, effectName);
      }

      return armor;
    }
  }
}