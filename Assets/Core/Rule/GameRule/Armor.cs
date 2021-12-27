using System;

namespace Core.Rule.GameRule {
    public class Armor {
        [Flags]
        public enum ArmorType {
            None = 0,
            No = 1 << 0,
            Leather = 1 << 1,
            LeatherSilver = 1 << 2,
            IronChainMail = 1 << 3,
            SilverChainMail = 1 << 4,
            Carapace = 1 << 5,
            PlateArmor = 1 << 6,
            Shield = 1 << 7,
            All = No | Leather | LeatherSilver | IronChainMail | SilverChainMail | Carapace | PlateArmor | Shield,
            WarriorArmorKit = All,
            ElfArmorKit = All ^ PlateArmor,
            WizardArmorKit = No | Leather | LeatherSilver,
            KronArmorKit = All ^ PlateArmor,
            GnomArmorKit = All ^ PlateArmor,
            OnlyArmor = All ^ Shield,
        }

        public const string DefaultNameForArmor = "No";
        private Armor.ArmorType _armorType = Armor.ArmorType.None;
        private  string _armorName;
        private string _effectName = String.Empty;

        public Armor(string armorName = DefaultNameForArmor, int classOfArmor = 8, Armor.ArmorType armorType = Armor.ArmorType.None, string effectName = "") {
            this.ArmorName = armorName;
            ArmorClass = new ArmorClass(classOfArmor);
            this.EffectName = effectName;
            this.armorType = armorType;
        }

        public Armor.ArmorType armorType {
            get {
                return _armorType;
            }
            private set {
                _armorType = value;
            }
        }

        public string ArmorName {
            get {
                return _armorName;
            }
            set {
                if (value != null) {
                    _armorName = value;
                } else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }

        public ArmorClass ArmorClass { get; private set; }

        public string EffectName {
            get {
                return _effectName;
            }
            set {
                if (value != null) {
                    _effectName = value;
                } else {
                    throw new InvalidOperationException("Value is invalid");
                }
            }
        }

        public void Init(string armorName = DefaultNameForArmor, int classOfArmor = 8, Armor.ArmorType armorType = Armor.ArmorType.None, string effectName = "") {
            this.ArmorName = armorName;
            ArmorClass = new ArmorClass(classOfArmor);
            this.EffectName = effectName;
            this.armorType = armorType;
        }
        public void AddEffectOnArmor(string nameOfEffect, int value) {
            EffectName = nameOfEffect;
            ArmorClass.ClassOfArmor += value;
        }
    }
}