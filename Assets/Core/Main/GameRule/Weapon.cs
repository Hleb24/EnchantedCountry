using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using Core.Main.Dice;

namespace Core.Main.GameRule {
  public class Weapon {
    public static bool Is(WeaponType conditionalWeaponType, WeaponType targetWeaponType) {
      return (conditionalWeaponType & targetWeaponType) == targetWeaponType;
    }

    private static bool IsEdgesEqualOne(int edges) {
      return edges == 1;
    }

    private static bool IsEdgesAtMostSix(int edges) {
      return edges <= 6;
    }

    private static bool IsEdgesEqualToNine(int edges) {
      return edges == 9;
    }

    private static bool IsEdgesEqualToEighteen(int edges) {
      return edges == 18;
    }

    public const string DefalultName = "Rock";

    

    public string NameOfWeapon;
    private string _effectName = string.Empty;

    public Weapon(float maxDamage = 0, WeaponType weaponType = WeaponType.None, string name = DefalultName, float minDamage = 0, int accurancy = 0, string effectName = "",
      int id = 100) {
      NameOfWeapon = name;
      EffectName = effectName;
      this.weaponType = weaponType;
      Attack = new Attack(maxDamage, minDamage, accurancy);
      Id = id;
    }

   

    public void Init(float maxDamage = 1, WeaponType weaponType = WeaponType.None, string name = DefalultName, float minDamage = 0, int accurancy = 0, string effectName = "normal",
      int id = 100) {
      NameOfWeapon = name;
      EffectName = effectName;
      this.weaponType = weaponType;
      Attack = new Attack(maxDamage, minDamage, accurancy);
      Id = id;
    }

    public void Init(List<float> damageList, WeaponType weaponType = WeaponType.None, string name = DefalultName, int accurancy = 0, string effectName = "", int id = 100) {
      NameOfWeapon = name;
      EffectName = effectName;
      this.weaponType = weaponType;
      Attack = new Attack(damageList, accurancy);
      Id = id;
    }

    public float ToDamage() {
      int edges = Attack.DiceEdges;
      if (edges == 0) {
        return 0;
      }

      if (IsEdgesEqualOne(edges)) {
        return Attack.GetMaxDamage();
      }

      if (IsEdgesAtMostSix(edges)) {
        var sixSidedDice = new SixSidedDice();
        int edge = sixSidedDice.GetDiceRollAccordingToEdges(edges);
        return Attack.GetDamage(edge);
      }

      if (IsEdgesEqualToNine(edges)) {
        int edge = KitOfDice.DicesKit[KitOfDice.SetWithOneThreeSidedAndOneSixSidedDice].GetSumRollOfBoxDices();
        return Attack.GetDamage(edge);
      }

      if (IsEdgesEqualToEighteen(edges)) {
        int edge = KitOfDice.DicesKit[KitOfDice.SetWithOneTwelveSidedAndOneSixSidedDice].GetSumRollOfBoxDices();
        return Attack.GetDamage(edge);
      }

      throw new InvalidOperationException("Edges is invalid");
    }

    

   

    public Attack Attack { get; private set; }
    public WeaponType weaponType { get; set; }

    public string EffectName {
      get {
        return _effectName;
      }
      set {
        if (value.NotNull()) {
          _effectName = value;
        } else {
          throw new InvalidOperationException("Value is invalid");
        }
      }
    }

    public int Id { get; private set; }
  }
}