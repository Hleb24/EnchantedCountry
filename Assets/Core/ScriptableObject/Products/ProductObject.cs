using Core.ScriptableObject.Armor;
using Core.ScriptableObject.Weapon;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

namespace Core.ScriptableObject.Products {
  [CreateAssetMenu(fileName = "New Products", menuName = "Product", order = 54)]
  public class ProductObject : UnityEngine.ScriptableObject {
    public static implicit operator Rule.GameRule.Armor(ProductObject productObject) {
      return productObject.GetArmor();
    }

    public static implicit operator Rule.GameRule.Weapon(ProductObject productObject) {
      return productObject.GetWeapon();
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
    private UnityEngine.ScriptableObject _item;
    [FormerlySerializedAs("productName"), SerializeField]
    private string _productName = "";
    [FormerlySerializedAs("description"), SerializeField]
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

    public string GetDescription() {
      return _description;
    }

    public string GetProductName() {
      return _productName;
    }

    public ProductType GetProductType() {
      return _productType;
    }

    public UnityEngine.ScriptableObject GetItem() {
      return _item;
    }

    public void SetId(int id) {
      _id = id;
    }

    public void OnValidate() {
      GetNameWithItem();
    }

    public Rule.GameRule.Weapon.WeaponType GetWeaponType() {
      var weapon = _item as WeaponObject;
      Debug.Assert(weapon != null, nameof(weapon) + " != null");
      return weapon.weaponType;
    }

    public Rule.GameRule.Armor.ArmorType GetArmorType() {
      var armor = _item as ArmorObject;
      Debug.Assert(armor != null, nameof(armor) + " != null");
      return armor.armorType;
    }

    public Rule.GameRule.Armor GetArmor() {
      var armorObject = _item as ArmorObject;
      Debug.Assert(armorObject != null, nameof(armorObject) + " != null");
      Rule.GameRule.Armor armor = armorObject.InitArmor();
      return armor;
    }

    public Rule.GameRule.Weapon GetWeapon() {
      var weaponObject = _item as WeaponObject;
      Debug.Assert(weaponObject != null, nameof(weaponObject) + " != null");
      Rule.GameRule.Weapon weapon = weaponObject.InitWeapon();
      return weapon;
    }

    public void SetPrice(int price) {
      _price = price;
    }

    public void SetProductType(ProductType productType) {
      _productType = productType;
    }

    public void SetItem(UnityEngine.ScriptableObject item) {
      _item = item;
    }

    

    private void GetNameWithItem() {
      IsWeaponObject();
      IsArmorObject();
    }

    private void IsWeaponObject() {
      if (_item is WeaponObject w) {
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
      if (_item is ArmorObject a) {
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