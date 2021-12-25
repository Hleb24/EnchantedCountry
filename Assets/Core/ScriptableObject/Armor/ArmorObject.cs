using System;
using UnityEngine;

namespace Core.ScriptableObject.Armor {
  [Serializable, CreateAssetMenu(fileName = "New Armor", menuName = "Armor", order = 53)]
  public class ArmorObject : UnityEngine.ScriptableObject {
    public Rule.GameRule.Armor.Armor.ArmorType armorType = Rule.GameRule.Armor.Armor.ArmorType.None;
    public string armorName = "";
    public int classOfArmor;
    public string effectName = "";
    public Rule.GameRule.Armor.Armor armor;
    public int id;

    public void OnEnable() {
      InitArmor();
    }

    public void OnValidate() {
      InitArmor();
    }

    public Rule.GameRule.Armor.Armor InitArmor() {
      if (armor == null) {
        armor = new Rule.GameRule.Armor.Armor(armorName, classOfArmor, armorType, effectName);
      } else {
        armor.Init(armorName, classOfArmor, armorType, effectName);
      }

      return armor;
    }
  }
}