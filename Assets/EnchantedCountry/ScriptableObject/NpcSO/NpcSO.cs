using System.Collections.Generic;
using Core.EnchantedCountry.CoreEnchantedCountry.Dice;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.ArmorClass;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Impact;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.NPC;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.RiskPoints;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.ScriptableObject.WeaponObjects;
using Core.EnchantedCountry.SupportSystems.Data;
using UnityEngine;

namespace Core.EnchantedCountry.ScriptableObject.NpcSO {
  [CreateAssetMenu(menuName = "NPC", fileName = "NPC", order = 58)]
  public class NpcSO : UnityEngine.ScriptableObject {
    public string Name;
    public Alignment Alignment;
    public NpcType NpcType;
    public int ClassOfArmor;
    public int LifeDice;
    public int RiskPoints;
    public int Morality;
    public List<WeaponObject> _weaponObjects;
    public List<ImpactsSO.ImpactsSO> _impactObjects;
    public int Experience;
    public List<int> EscapePossibility;
    public string Description;
    public string Property;
    public bool Immoral;
    public bool Immortal;
    public bool DeadlyAttack;
    public bool AttackEveryAtOnce;
    public int Id;
    private Npc _npc;

    public void OnValidate() {
      Name = name;
    }

    public Npc GetNpc () {
      _npc = new Npc(Name, Alignment, NpcType, new RiskPoints(ScribeDealer.Peek<RiskPointsScribe>(), GetRiskPoints()), new ArmorClass(ClassOfArmor), Morality,  Immoral, Immortal,
        DeadlyAttack, AttackEveryAtOnce,Experience, EscapePossibility, Description, Property, Id, GetListOfWeapon(), GetListOfImpacts());
      return _npc;
    }
    
    public int GetRiskPoints() {
      if (RiskPoints != 0)
        return RiskPoints;
      Dices[] dices = new Dices[LifeDice];
      for (int i = 0; i < dices.Length; i++) {
        dices[i] = new SixSidedDice(DiceType.SixEdges);
      }

      DiceBox diceBox = new DiceBox(dices);
      return diceBox.SumRollsOfDice();
    }

    private List<Weapon> GetListOfWeapon() {
      if (_weaponObjects == null) {
        return null;
      }
      List<Weapon> weapons = new List<Weapon>();
      foreach (WeaponObject weaponObject in _weaponObjects) {
        weapons.Add(weaponObject.weapon);
      }

      return weapons;
    }
    
    private  List<Impact<ImpactOnRiskPoints>> GetListOfImpacts() {
      if (_impactObjects == null) {
        return null;
      }
      List<Impact<ImpactOnRiskPoints>> impacts = new List<Impact<ImpactOnRiskPoints>>();
      foreach (ImpactsSO.ImpactsSO impactsSo in _impactObjects) {
        impacts.Add(impactsSo.GetImpact());
      }

      return impacts;
    }
  }
}