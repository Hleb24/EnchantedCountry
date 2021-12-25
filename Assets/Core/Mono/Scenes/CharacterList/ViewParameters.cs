using Core.Rule.Character.Qualities;
using Core.Rule.GameRule.Armor;
using Core.Rule.GameRule.EquipmentIdConstants;
using Core.Rule.GameRule.Weapon;
using Core.ScriptableObject.Products;
using Core.ScriptableObject.Storage;
using Core.SupportSystems.Data;
using Core.SupportSystems.SaveSystem.SaveManagers;
using TMPro;
using UnityEngine;

namespace Core.Mono.Scenes.CharacterList {
  public class ViewParameters : MonoBehaviour {
    [SerializeField]
    private SpawnProducts _spawnProducts;
    [SerializeField]
    private WalletInCharacterList _walletIn;
    [SerializeField]
    private TMP_Text _armorClassText;
    [SerializeField]
    private TMP_Text _meleeAttackText;
    [SerializeField]
    private TMP_Text _meleeDamageText;
    [SerializeField]
    private TMP_Text _rangeAttackText;
    [SerializeField]
    private TMP_Text _rangeDamageText;
    [SerializeField]
    private TMP_Text _maxAmountOfCoinsText;
    private IEquipmentUsed _equipmentUsed;
    private Qualities _qualities;
    private int _classOfArmor;
    private int _meleeAttack;
    private float _meleeMinDamage;
    private float _meleeMaxDamage;
    private int _rangeAttack;
    private float _rangeMinDamage;
    private float _rangeMaxDamage;
    private int _maxAmountOfCoins;
    private void Start() {
      _equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      Invoke(nameof(SetQualities), 0.1f);
    }

    
    private void OnEnable() {
      EquipmentsChoice.EquipmentChanged += OnEquipmentChanged;
    }

    private void OnDisable() {
      EquipmentsChoice.EquipmentChanged -= OnEquipmentChanged;
    }
    private void SetQualities() {
      _qualities = new Qualities(ScribeDealer.Peek<QualityPointsScribe>());
    }
    private void OnEquipmentChanged() {
      ArmorClassParameters();
      MeleeParameters();
      RangeParameters();
      MaxAmountOfCoinsParameters();
    }
    private void GetArmorClass(StorageObject storageObject, int id) {
      if (id.Equals(0)) {
        return;
      }

      ProductObject product = storageObject.GetArmorFromList(id);
      Armor armor = product;
      _classOfArmor = armor.ArmorClass.ClassOfArmor + _qualities[QualityType.Agility].Modifier;
      SetTextForParameters(_armorClassText, _classOfArmor);
    }
    private void GetAttackForMeleeWeapon(StorageObject storageObject, int oneHandedId, int twoHandedId) {
      if (IsTwoWeapons(oneHandedId, twoHandedId)) {
        Debug.Log("Two weapon");
        return;
      }

      if (IsNoWeapon(oneHandedId, twoHandedId)) {
        Debug.Log("No weapon");
        return;
      }

      int id = 0;
      if (!oneHandedId.Equals(0)) {
        id = oneHandedId;
      }

      if (!twoHandedId.Equals(0)) {
        id = twoHandedId;
      }

      _meleeAttack = GetAttack(storageObject, id) + _qualities[QualityType.Strength].Modifier;
      Debug.Log($"Strength = {_qualities[QualityType.Strength].ValueOfQuality}, modifier={_qualities[QualityType.Strength].Modifier}");
    }

    private void GetAttackForRangeWeapon(StorageObject storageObject, int rangeId) {
      if (rangeId.Equals(0)) {
        Debug.Log("Don't have range weapon");
        return;
      }

      _rangeAttack = GetAttack(storageObject, rangeId) + _qualities[QualityType.Agility].Modifier;
    }

    private int GetAttack(StorageObject storageObject, int id) {
      ProductObject product = storageObject.GetWeaponFromList(id);
      Weapon weapon = product;
      int attack = weapon.Attack.Accuracy;
      return attack;
    }
    private void GetDamageForMeleeWeapon(StorageObject storageObject, int oneHandedId, int twoHandedId) {
      if (IsTwoWeapons(oneHandedId, twoHandedId)) {
        Debug.Log("Two weapon");
        return;
      }

      if (IsNoWeapon(oneHandedId, twoHandedId)) {
        Debug.Log("No weapon");
        _meleeMinDamage = 0;
        _meleeMaxDamage = 0;
        return;
      }

      int id = 0;
      if (!oneHandedId.Equals(0)) {
        id = oneHandedId;
      }

      if (!twoHandedId.Equals(0)) {
        id = twoHandedId;
      }

      GetMeleeDamage(storageObject, id);
    }

    private bool IsNoWeapon(int oneHandedId, int twoHandedId) {
      return oneHandedId.Equals(0) && twoHandedId.Equals(0);
    }

    private bool IsTwoWeapons(int oneHandedId, int twoHandedId) {
      return !oneHandedId.Equals(0) && !twoHandedId.Equals(0);
    }

    private void GetMeleeDamage(StorageObject storageObject, int id) {
      ProductObject product = storageObject.GetWeaponFromList(id);
      Weapon weapon = product;
      _meleeMinDamage = weapon.Attack.MinDamage;
      _meleeMaxDamage = weapon.Attack.MaxDamage;
    }

    private void GetDamageForRangeWeapon(StorageObject storageObject, int rangeId, int projectiliesId = 0) {
      if (rangeId.Equals(0)) {
        _rangeMinDamage = 0;
        _rangeMaxDamage = 0;
        Debug.Log("Don't have range weapon");
        return;
      }

      GetRangeDamage(storageObject, rangeId, projectiliesId);
    }

    private void GetRangeDamage(StorageObject storageObject, int rangeId, int projectiliesId = 0) {
      ProductObject product = storageObject.GetWeaponFromList(rangeId);
      Weapon weapon = product;
      _rangeMinDamage = weapon.Attack.MinDamage;
      _rangeMaxDamage = weapon.Attack.MaxDamage;
      if (projectiliesId == 0) {
        return;
      }
      ProductObject projectiliesProduct = storageObject.GetProjectiliesFromList(projectiliesId);
      Weapon projectilies = projectiliesProduct;
      _rangeMinDamage += projectilies.Attack.MinDamage;
      _rangeMaxDamage += projectilies.Attack.MaxDamage;
    }
    private void MaxAmountOfCoins(StorageObject storageObject) {
      int maxCoins = 0;
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.BagId) != 0) {
        ProductObject productObject = storageObject.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.BagId) );
        maxCoins += int.Parse(productObject.Property);
      }
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId) != 0) {
        ProductObject productObject = storageObject.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId));
        maxCoins += int.Parse(productObject.Property);
      }
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId) != 0) {
        ProductObject productObject = storageObject.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId));
        maxCoins += int.Parse(productObject.Property);
      }

      maxCoins += int.Parse(storageObject.GetProductFromList(EquipmentIdConstants.Pockets).Property);
      _maxAmountOfCoins = maxCoins;
      _walletIn.Wallet.SetMaxCoins(_maxAmountOfCoins);
    }
    private void ArmorClassParameters() {
      GetArmorClass(_spawnProducts.StorageObject, _equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
    }

    private void MeleeParameters() {
      GetAttackForMeleeWeapon(_spawnProducts.StorageObject, _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
      SetTextForParameters(_meleeAttackText, _meleeAttack);
      GetDamageForMeleeWeapon(_spawnProducts.StorageObject, _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
      SetTextForParameters(_meleeDamageText, _meleeMinDamage, _meleeMaxDamage);
    }

    private void RangeParameters() {
      GetAttackForRangeWeapon(_spawnProducts.StorageObject,_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
      SetTextForParameters(_rangeAttackText, _rangeAttack);
      GetDamageForRangeWeapon(_spawnProducts.StorageObject, _equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId), _equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
      SetTextForParameters(_rangeDamageText, _rangeMinDamage, _rangeMaxDamage);
    }

    private void MaxAmountOfCoinsParameters() {
      MaxAmountOfCoins(_spawnProducts.StorageObject);
      SetTextForParameters(_maxAmountOfCoinsText, _maxAmountOfCoins);
    }
    private void SetTextForParameters(TMP_Text text, int parameter) {
      text.text = parameter.ToString();
    }

    private void SetTextForParameters(TMP_Text text, float min, float max) {
      text.text = $"{min}-{max}";
    }
  }
}