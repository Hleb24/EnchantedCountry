using System.Collections.Generic;
using Core.Rule.Dice;
using Core.Rule.GameRule;
using Core.Rule.GameRule.Impact;
using Core.Rule.GameRule.NPC;
using Core.Rule.GameRule.RiskPoints;
using Core.ScriptableObject.Impacts;
using Core.ScriptableObject.Weapon;
using Core.Support.Data;
using Core.Support.SaveSystem.SaveManagers;
using UnityEngine;
using Zenject;

namespace Core.ScriptableObject.Npc {
  [CreateAssetMenu(menuName = "NPC", fileName = "NPC", order = 58)]
  public class NonPlayerCharacter : UnityEngine.ScriptableObject {
    public string Name;
    public Alignment Alignment;
    public NpcType NpcType;
    public int ClassOfArmor;
    public int LifeDice;
    public int RiskPoints;
    public int Morality;
    public List<WeaponObject> _weaponObjects;
    public List<ImpactsObject> _impactObjects;
    public int Experience;
    public List<int> EscapePossibility;
    public string Description;
    public string Property;
    public bool Immoral;
    public bool Immortal;
    public bool DeadlyAttack;
    public bool AttackEveryAtOnce;
    public int Id;
    private Rule.GameRule.NPC.Npc _npc;
    private IRiskPoints _npcRiskPoints;

    public void OnValidate() {
      Name = name;
    }

    public Rule.GameRule.NPC.Npc GetNpc () {
      _npcRiskPoints = new NpcRiskPoints();
      _npc = new Rule.GameRule.NPC.Npc(Name, Alignment, NpcType, new RiskPoints(_npcRiskPoints, GetRiskPoints()), new ArmorClass(ClassOfArmor), Morality,  Immoral, Immortal,
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

    private List<Rule.GameRule.Weapon> GetListOfWeapon() {
      if (_weaponObjects == null) {
        return null;
      }
      List<Rule.GameRule.Weapon> weapons = new List<Rule.GameRule.Weapon>();
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
      foreach (ImpactsObject impactsSo in _impactObjects) {
        impacts.Add(impactsSo.GetImpact());
      }

      return impacts;
    }
  }

  public class NpcRiskPoints : IRiskPoints {
    private float _riskPoints;
    public float GetRiskPoints() {
      return _riskPoints;
    }

    public void SetRiskPoints(float riskPoints) {
      _riskPoints = riskPoints;
    }

    public void ChangeRiskPoints(float riskPoints) {
      _riskPoints += riskPoints;
    }

    public bool EnoughRiskPoints(float riskPoints) {
      return _riskPoints > riskPoints;
    }
  }
}