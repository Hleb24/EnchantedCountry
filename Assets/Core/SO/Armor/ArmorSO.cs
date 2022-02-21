using System;
using Core.Main.GameRule;
using UnityEngine;

namespace Core.SO.Armor {
  [Serializable, CreateAssetMenu(fileName = "New Armor", menuName = "Armor", order = 53)]
  public class ArmorSO : ScriptableObject {
    public ArmorType armorType = ArmorType.None;
    public string armorName = "";
    public int classOfArmor;
    public string effectName = "";
    public int id;

    public Main.GameRule.Armor GetArmor() {
      var armorClass = new ArmorClass(classOfArmor);
      return new Main.GameRule.Armor(armorName, armorClass, armorType, effectName);
    }
  }
}