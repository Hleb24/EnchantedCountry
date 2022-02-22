using System;
using Core.Main.GameRule;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.ArmorObjects {
  [Serializable, CreateAssetMenu(fileName = "New Armor", menuName = "Armor", order = 53)]
  public class ArmorSO : ScriptableObject {
    [FormerlySerializedAs("armorType"), SerializeField]
    private ArmorType _armorType;
    [FormerlySerializedAs("armorName"), SerializeField]
    private string _armorName;
    [FormerlySerializedAs("_classOfArmor"), FormerlySerializedAs("classOfArmor"), SerializeField]
    private int _armorClass;
    [FormerlySerializedAs("effectName"), SerializeField]
    private string _effectName;
    [FormerlySerializedAs("id"), SerializeField]
    private int _id;

    public Armor GetArmor() {
      var armorClass = new ArmorClass(_armorClass);
      return new Armor(_armorName, armorClass, _armorType, _effectName);
    }

    [MustUseReturnValue]
    public string GetArmorName() {
      return _armorName;
    }

    [MustUseReturnValue]
    public ArmorType GetArmorType() {
      return _armorType;
    }

    [MustUseReturnValue]
    public string GetEffectName() {
      return _effectName;
    }

    public int GetId() {
      return _id;
    }

    public int GetArmorClass() {
      return _armorClass;
    }
  }
}