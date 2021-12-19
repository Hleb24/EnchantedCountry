using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Armor;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.EquipmentIdConstants;
using Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Weapon;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.ScriptableObject.ProductObject;
using Core.EnchantedCountry.ScriptableObject.Storage;
using Core.EnchantedCountry.SupportSystems.Data;
using TMPro;
using UnityEngine;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CharacterList {
  public class ViewParameters : MonoBehaviour {
    #region FIELDS
    [SerializeField]
    private SpawnProductsInCharacterList _spawnProducts;
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
    #endregion
    #region MONOBEHAVIOUR_METHODS
    private void Start() {
      _equipmentUsed = ScribeDealer.Peek<EquipmentUsedScribe>();
      Invoke(nameof(SetQualities), 0.1f);
    }

    
    private void OnEnable() {
      ApplyAndTakeOffEquipment.EquipmentChanged += OnEquipmentChanged;
    }

    private void OnDisable() {
      ApplyAndTakeOffEquipment.EquipmentChanged -= OnEquipmentChanged;
    }
    #endregion
    #region SET_QUALITIES
    private void SetQualities() {
      _qualities = new Qualities(ScribeDealer.Peek<QualityPointsScribe>());
    }
    #endregion    
    #region HANDLERS
    private void OnEquipmentChanged() {
      ArmorClassParameters();
      MeleeParameters();
      RangeParameters();
      MaxAmountOfCoinsParameters();
    }
    #endregion
    #region GET_ARMOR_CLASS
    private void GetArmorClass(StorageSO storageSo, int id) {
      if (id.Equals(0)) {
        return;
      }

      ProductSO product = storageSo.GetArmorFromList(id);
      Armor armor = product;
      _classOfArmor = armor.ArmorClass.ClassOfArmor + _qualities[QualityType.Agility].Modifier;
      SetTextForParameters(_armorClassText, _classOfArmor);
    }
    #endregion
    #region GET_ATTACK
    private void GetAttackForMeleeWeapon(StorageSO storageSo, int oneHandedId, int twoHandedId) {
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

      _meleeAttack = GetAttack(storageSo, id) + _qualities[QualityType.Strength].Modifier;
      Debug.Log($"Strength = {_qualities[QualityType.Strength].ValueOfQuality}, modifier={_qualities[QualityType.Strength].Modifier}");
    }

    private void GetAttackForRangeWeapon(StorageSO storageSo, int rangeId) {
      if (rangeId.Equals(0)) {
        Debug.Log("Don't have range weapon");
        return;
      }

      _rangeAttack = GetAttack(storageSo, rangeId) + _qualities[QualityType.Agility].Modifier;
    }

    private int GetAttack(StorageSO storageSo, int id) {
      ProductSO product = storageSo.GetWeaponFromList(id);
      Weapon weapon = product;
      int attack = weapon.Attack.Accuracy;
      return attack;
    }
    #endregion
    #region GET_DAMAGE
    private void GetDamageForMeleeWeapon(StorageSO storageSo, int oneHandedId, int twoHandedId) {
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

      GetMeleeDamage(storageSo, id);
    }

    private bool IsNoWeapon(int oneHandedId, int twoHandedId) {
      return oneHandedId.Equals(0) && twoHandedId.Equals(0);
    }

    private bool IsTwoWeapons(int oneHandedId, int twoHandedId) {
      return !oneHandedId.Equals(0) && !twoHandedId.Equals(0);
    }

    private void GetMeleeDamage(StorageSO storageSo, int id) {
      ProductSO product = storageSo.GetWeaponFromList(id);
      Weapon weapon = product;
      _meleeMinDamage = weapon.Attack.MinDamage;
      _meleeMaxDamage = weapon.Attack.MaxDamage;
    }

    private void GetDamageForRangeWeapon(StorageSO storageSo, int rangeId, int projectiliesId = 0) {
      if (rangeId.Equals(0)) {
        _rangeMinDamage = 0;
        _rangeMaxDamage = 0;
        Debug.Log("Don't have range weapon");
        return;
      }

      GetRangeDamage(storageSo, rangeId, projectiliesId);
    }

    private void GetRangeDamage(StorageSO storageSo, int rangeId, int projectiliesId = 0) {
      ProductSO product = storageSo.GetWeaponFromList(rangeId);
      Weapon weapon = product;
      _rangeMinDamage = weapon.Attack.MinDamage;
      _rangeMaxDamage = weapon.Attack.MaxDamage;
      if (projectiliesId == 0) {
        return;
      }
      ProductSO projectiliesProduct = storageSo.GetProjectiliesFromList(projectiliesId);
      Weapon projectilies = projectiliesProduct;
      _rangeMinDamage += projectilies.Attack.MinDamage;
      _rangeMaxDamage += projectilies.Attack.MaxDamage;
    }
    #endregion
    #region MAX_AMOUNT_OF_COINS
    private void MaxAmountOfCoins(StorageSO storageSo) {
      int maxCoins = 0;
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.BagId) != 0) {
        ProductSO productSo = storageSo.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.BagId) );
        maxCoins += int.Parse(productSo.Property);
      }
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId) != 0) {
        ProductSO productSo = storageSo.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.AnimalId));
        maxCoins += int.Parse(productSo.Property);
      }
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId) != 0) {
        ProductSO productSo = storageSo.GetProductFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.CarriageId));
        maxCoins += int.Parse(productSo.Property);
      }

      maxCoins += int.Parse(storageSo.GetProductFromList(EquipmentIdConstants.Pockets).Property);
      _maxAmountOfCoins = maxCoins;
      _walletIn.Wallet.SetMaxCoins(_maxAmountOfCoins);
    }
    #endregion
    #region GET_PARAMETERS
    private void ArmorClassParameters() {
      GetArmorClass(_spawnProducts.StorageSo, _equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
    }

    private void MeleeParameters() {
      GetAttackForMeleeWeapon(_spawnProducts.StorageSo, _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
      SetTextForParameters(_meleeAttackText, _meleeAttack);
      GetDamageForMeleeWeapon(_spawnProducts.StorageSo, _equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId), _equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
      SetTextForParameters(_meleeDamageText, _meleeMinDamage, _meleeMaxDamage);
    }

    private void RangeParameters() {
      GetAttackForRangeWeapon(_spawnProducts.StorageSo,_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
      SetTextForParameters(_rangeAttackText, _rangeAttack);
      GetDamageForRangeWeapon(_spawnProducts.StorageSo, _equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId), _equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
      SetTextForParameters(_rangeDamageText, _rangeMinDamage, _rangeMaxDamage);
    }

    private void MaxAmountOfCoinsParameters() {
      MaxAmountOfCoins(_spawnProducts.StorageSo);
      SetTextForParameters(_maxAmountOfCoinsText, _maxAmountOfCoins);
    }
    #endregion
    #region SET_TEXT_FOR_PARAMETRS
    private void SetTextForParameters(TMP_Text text, int parameter) {
      text.text = parameter.ToString();
    }

    private void SetTextForParameters(TMP_Text text, float min, float max) {
      text.text = $"{min}-{max}";
    }
    #endregion
  }
}