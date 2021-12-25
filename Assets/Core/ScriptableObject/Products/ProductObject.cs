using Core.ScriptableObject.Armor;
using Core.ScriptableObject.Weapon;
using UnityEngine;
using static Core.Rule.GameRule.Armor.Armor;
using static Core.Rule.GameRule.Weapon.Weapon;

namespace Core.ScriptableObject.Products {
  [CreateAssetMenu(fileName = "New Products", menuName = "Product", order = 54)]
  public class ProductObject : UnityEngine.ScriptableObject {
    public static implicit operator Rule.GameRule.Armor.Armor(ProductObject productObject) {
      return productObject.GetArmor();
    }

    public static implicit operator Rule.GameRule.Weapon.Weapon(ProductObject productObject) {
      return productObject.GetWeapon();
    }

    public enum ProductType {
      None,
      Weapon,
      Armor,
      Item
    }

    public ProductType productType;
    public UnityEngine.ScriptableObject item;
    public string productName = "";
    public string description = "";
    public string Property;
    public int price;
    public int id;
    public Sprite icon;

    public void OnEnable() {
      GetNameWithItem();
    }

    public void OnValidate() {
      GetNameWithItem();
    }

    public WeaponType GetWeaponType() {
      var weapon = item as WeaponObject;
      return weapon.weaponType;
    }

    public ArmorType GetArmorType() {
      var armor = item as ArmorObject;
      return armor.armorType;
    }

    public Rule.GameRule.Armor.Armor GetArmor() {
      var armorObject = item as ArmorObject;
      Rule.GameRule.Armor.Armor armor = armorObject.InitArmor();
      return armor;
    }

    public Rule.GameRule.Weapon.Weapon GetWeapon() {
      var weaponObject = item as WeaponObject;
      Rule.GameRule.Weapon.Weapon weapon = weaponObject.InitWeapon();
      return weapon;
    }

    private void GetNameWithItem() {
      IsWeaponObject();
      IsArmorObject();
    }

    private void IsWeaponObject() {
      if (item is WeaponObject w) {
        if (w.effectName != string.Empty) {
          string tempName = w.effectName + " " + w.weaponName;
          productName = tempName;
        } else {
          productName = w.weaponName;
        }

        Property = w.minDamage + " - " + w.maxDamage;
        id = w.id;
      }
    }

    private void IsArmorObject() {
      if (item is ArmorObject a) {
        if (a.effectName != string.Empty) {
          string tempName = a.effectName + " " + a.armorName;
          productName = tempName;
        } else {
          productName = a.armorName;
        }

        Property = a.classOfArmor.ToString();
        id = a.id;
      }
    }
  }
}