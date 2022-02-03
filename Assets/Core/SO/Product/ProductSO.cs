using Core.SO.Armor;
using Core.SO.WeaponObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SO.Product {
  [CreateAssetMenu(fileName = "New Products", menuName = "Product", order = 54)]
  public class ProductSO : ScriptableObject {
    public static implicit operator Main.GameRule.Armor(ProductSO productSO) {
      return productSO.GetArmor();
    }

    public static implicit operator Main.GameRule.Weapon(ProductSO productSO) {
      return productSO.GetWeapon();
    }

    public enum ProductType {
      None,
      Weapon,
      Armor,
      Item
    }

    [FormerlySerializedAs("productType"), SerializeField]
    private ProductType _productType;
    [FormerlySerializedAs("item"), SerializeField]
    private ScriptableObject _item;
    [FormerlySerializedAs("productName"), SerializeField]
    private string _productName = "";
    [FormerlySerializedAs("description"), SerializeField]
    // ReSharper disable once NotAccessedField.Local
    private string _description = "";
    [FormerlySerializedAs("Property"), SerializeField]
    private string _property;
    [FormerlySerializedAs("price"), SerializeField]
    private int _price;
    [FormerlySerializedAs("id"), SerializeField]
    private int _id;
    [FormerlySerializedAs("icon"), SerializeField]
    private Sprite _icon;

    public void OnEnable() {
      GetNameWithItem();
    }

    public int GetId() {
      return _id;
    }

    public Sprite GetIcon() {
      return _icon;
    }

    public int GetPrice() {
      return _price;
    }

    public string GetProperty() {
      return _property;
    }

    public string GetProductName() {
      return _productName;
    }

    public ProductType GetProductType() {
      return _productType;
    }

    public ScriptableObject GetItem() {
      return _item;
    }

    public void OnValidate() {
      GetNameWithItem();
    }

    public Main.GameRule.Weapon.WeaponType GetWeaponType() {
      var weapon = _item as WeaponSO;
      Debug.Assert(weapon != null, nameof(weapon) + " != null");
      return weapon.weaponType;
    }

    public Main.GameRule.Armor.ArmorType GetArmorType() {
      var armor = _item as ArmorSO;
      Debug.Assert(armor != null, nameof(armor) + " != null");
      return armor.armorType;
    }

    public Main.GameRule.Armor GetArmor() {
      var armorObject = _item as ArmorSO;
      Debug.Assert(armorObject != null, nameof(armorObject) + " != null");
      Main.GameRule.Armor armor = armorObject.InitArmor();
      return armor;
    }

    public Main.GameRule.Weapon GetWeapon() {
      var weaponObject = _item as WeaponSO;
      Debug.Assert(weaponObject != null, nameof(weaponObject) + " != null");
      Main.GameRule.Weapon weapon = weaponObject.InitWeapon();
      return weapon;
    }

    public void SetPrice(int price) {
      _price = price;
    }

    public void SetProductType(ProductType productType) {
      _productType = productType;
    }

    public void SetItem(ScriptableObject item) {
      _item = item;
    }

    private void GetNameWithItem() {
      IsWeaponObject();
      IsArmorObject();
    }

    private void IsWeaponObject() {
      if (_item is WeaponSO w) {
        if (w.effectName != string.Empty) {
          string tempName = w.effectName + " " + w.weaponName;
          _productName = tempName;
        } else {
          _productName = w.weaponName;
        }

        _property = w.minDamage + " - " + w.maxDamage;
        _id = w.id;
      }
    }

    private void IsArmorObject() {
      if (_item is ArmorSO a) {
        if (a.effectName != string.Empty) {
          string tempName = a.effectName + " " + a.armorName;
          _productName = tempName;
        } else {
          _productName = a.armorName;
        }

        _property = a.classOfArmor.ToString();
        _id = a.id;
      }
    }
  }
}