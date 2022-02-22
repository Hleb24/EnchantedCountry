using Core.Main.Dice;
using UnityEngine.Assertions;

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

    private readonly int _id;
    private readonly Attack _attack;
    private string _nameOfWeapon;
    private string _effectName;

    public Weapon(Attack attack, WeaponType weaponType, string name, string effectName, int id) {
      Assert.IsNotNull(name, nameof(name));
      Assert.IsNotNull(attack, nameof(attack));
      Assert.IsNotNull(effectName, nameof(effectName));
      _nameOfWeapon = name;
      _effectName = effectName;
      _attack = attack;
      _id = id;
      WeaponType = weaponType;
    }

    public float ToDamage() {
      int edges = _attack.GetDiceEdges();

      if (IsEdgesEqualOne(edges)) {
        return _attack.GetMaxDamage();
      }

      if (IsEdgesAtMostSix(edges)) {
        var sixSidedDice = new SixSidedDice();
        int edge = sixSidedDice.GetDiceRollAccordingToEdges(edges);
        return _attack.GetDamage(edge);
      }

      if (IsEdgesEqualToNine(edges)) {
        DiceBox diceBox = KitOfDice.DicesKit[KitOfDice.SET_WITH_ONE_THREE_SIDED_AND_ONE_SIX_SIDED_DICE];
        int edge = diceBox.GetSumRollOfBoxDices();
        return _attack.GetDamage(edge);
      }

      if (IsEdgesEqualToEighteen(edges)) {
        int edge = KitOfDice.DicesKit[KitOfDice.SET_WITH_ONE_TWELVE_SIDED_AND_ONE_SIX_SIDED_DICE].GetSumRollOfBoxDices();
        return _attack.GetDamage(edge);
      }

      return 0;
    }

    public int GetAccuracy() {
      return _attack.GetAccuracy();
    }

    public float GetMaxDamage() {
      return _attack.GetMaxDamage();
    }

    public float GetMinDamage() {
      return _attack.GetMinDamage();
    }

    public int GetWeaponId() {
      return _id;
    }

    public WeaponType WeaponType { get; }
  }
}