using System.Threading.Tasks;
using Core.Main.Character;
using Core.Main.Character.Class;
using Core.Main.Character.Item;
using Core.Main.Character.Level;
using Core.Main.Character.Quality;
using Core.Main.GameRule.Equipment;
using Core.Main.GameRule.Item;
using Core.Main.GameRule.Point;
using Core.Mono.MainManagers;
using Core.SO.ProductObjects;
using Core.SO.Storage;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Core.Mono.Scenes.QualityDiceRoll {
  public class PlayerBuilder : MonoBehaviour {
    [FormerlySerializedAs("_storageObject"), FormerlySerializedAs("_storageSo"), SerializeField]
    private StorageSO _storageSO;
    [SerializeField]
    private Button _createPlayer;
    [SerializeField]
    private bool _buildOnStart;
    private ILauncher _launcher;
    private IEquipment _equipments;
    private IEquipmentUsed _equipmentUsed;
    private IGamePoints _gamePoints;
    private IRiskPoints _riskPoints;
    private IQualityPoints _qualityPoints;
    private IClassType _classType;
    private IWallet _wallet;

    private void Start() {
      WaitLoad();
    }

    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }

    [Inject]
    public void Constructor(ILauncher launcher, IEquipment equipment, IEquipmentUsed equipmentUsed, IGamePoints gamePoints, IRiskPoints riskPoints, IQualityPoints qualityPoints,
      IClassType classType, IWallet wallet) {
      _launcher = launcher;
      _equipments = equipment;
      _equipmentUsed = equipmentUsed;
      _gamePoints = gamePoints;
      _riskPoints = riskPoints;
      _qualityPoints = qualityPoints;
      _classType = classType;
      _wallet = wallet;
    }

    private void BuildPlayer() {
      BaseCharacter = new BaseCharacter(GetQualities(), GetCharacterType(), GetLevels(), GetGamePoints(), GetRiskPoints(), GetWallet(), GetEquipmentsOfCharacter(),
        GetEquipmentsUsed(), GetArmor(), GetShield(), GetRangeWeapon(), GetMeleeWeapon(), GetProjectiles());
      Debug.LogWarning("Player create " + (BaseCharacter != null));
    }

    private async void WaitLoad() {
      while (_launcher.StillInitializing()) {
        await Task.Yield();
      }

      if (_buildOnStart) {
        BuildPlayer();
      }
    }

    private IGamePoints GetGamePoints() {
      return _gamePoints;
    }

    private BaseLevel GetLevels() {
      var levels = new BaseLevel(_gamePoints.GetPoints());
      return levels;
    }

    private RiskPoints GetRiskPoints() {
      return new RiskPoints(_riskPoints);
    }

    private IWallet GetWallet() {
      return _wallet;
    }

    private CharacterEquipments GetEquipmentsOfCharacter() {
      var equipmentsOfCharacter = new CharacterEquipments(_equipments.GetEquipmentCards());
      return equipmentsOfCharacter;
    }

    private IEquipmentUsed GetEquipmentsUsed() {
      return _equipmentUsed;
    }

    private Armor GetArmor() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId) != 0) {
        ProductSO armorSO = _storageSO.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ArmorId));
        Armor armor = armorSO.GetArmor();
        return armor;
      }

      ProductSO noArmorProduct = _storageSO.GetArmorFromList(EquipmentIdConstants.NO_ARMOR_ID);
      Armor noArmor = noArmorProduct.GetArmor();
      return noArmor;
    }

    private Armor GetShield() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId) != 0) {
        ProductSO shieldSO = _storageSO.GetArmorFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ShieldId));
        Armor shield = shieldSO.GetArmor();
        return shield;
      }

      return null;
    }

    private Weapon GetMeleeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId) != 0) {
        ProductSO weaponSO = _storageSO.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.OneHandedId));
        Weapon weapon = weaponSO.GetWeapon();
        return weapon;
      }

      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId) != 0) {
        ProductSO weaponSO = _storageSO.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.TwoHandedId));
        Weapon weapon = weaponSO.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetRangeWeapon() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId) != 0) {
        ProductSO weaponSO = _storageSO.GetWeaponFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.RangeId));
        Weapon weapon = weaponSO.GetWeapon();
        return weapon;
      }

      return null;
    }

    private Weapon GetProjectiles() {
      if (_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId) != 0) {
        ProductSO weaponSO = _storageSO.GetProjectilesFromList(_equipmentUsed.GetEquipment(EquipmentsUsedId.ProjectilesId));
        Weapon weapon = weaponSO.GetWeapon();
        return weapon;
      }

      return null;
    }

    private void AddListeners() {
      _createPlayer.onClick.AddListener(BuildPlayer);
    }

    private void RemoveListeners() {
      _createPlayer.onClick.RemoveListener(BuildPlayer);
    }

    private ClassType GetCharacterType() {
      return _classType.GetClassType();
    }

    private Qualities GetQualities() {
      var characterQualities = new Qualities(_qualityPoints);
      return characterQualities;
    }

    public BaseCharacter BaseCharacter { get; private set; }
  }
}