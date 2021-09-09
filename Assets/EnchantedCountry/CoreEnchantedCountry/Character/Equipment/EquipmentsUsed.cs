using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Equipment {
  [Serializable]
  public class EquipmentsUsed {
    #region FIELDS
    private int _armorId;
    private int _shieldId;
    private int _oneHandedId;
    private int _twoHandedId;
    private int _rangeId;
    private int _projectiliesId;
    private int _bagId;
    private int _animalId;
    private int _carriageId;
    #endregion
    #region CONSTRUCTORS
    public EquipmentsUsed(int armorId, int shieldId, int oneHandedId, int twoHandedId, int rangeId, int projectiliesId, int bagId, int animalId, int carriageId) {
      _armorId = armorId;
      _shieldId = shieldId;
      _oneHandedId = oneHandedId;
      _twoHandedId = twoHandedId;
      _rangeId = rangeId;
      _projectiliesId = projectiliesId;
      _bagId = bagId;
      _animalId = animalId;
      _carriageId = carriageId;
    }
    #endregion
    #region PROPERTIES
    public int ArmorId {
      get {
        return _armorId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _armorId = value;
      }
    }

    public int ShieldId {
      get {
        return _shieldId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _shieldId = value;
      }
    }

    public int OneHandedId {
      get {
        return _oneHandedId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _oneHandedId = value;
      }
    }

    public int TwoHandedId {
      get {
        return _twoHandedId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _twoHandedId = value;
      }
    }

    public int RangeId {
      get {
        return _rangeId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _rangeId = value;
      }
    }

    public int ProjectiliesId {
      get {
        return _projectiliesId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _projectiliesId = value;
      }
    }

    public int BagId {
      get {
        return _bagId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _bagId = value;
      }
    }

    public int AnimalId {
      get {
        return _animalId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _animalId = value;
      }
    }

    public int CarriageId {
      get {
        return _carriageId;
      }
      set {
        if (value < 0) {
          throw new InvalidOperationException("Id less than 0");
        }
        _carriageId = value;
      }
    }
    #endregion
  }
}