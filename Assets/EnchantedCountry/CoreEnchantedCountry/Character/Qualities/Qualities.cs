using System.Collections.Generic;
using Core.EnchantedCountry.SupportSystems.Data;

namespace Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities {
	public class Qualities {
		#region Fields
		private Quality _strength;
		private Quality _agility;
		private Quality _constitution;
		private Quality _wisdom;
		private Quality _courage;
		private Dictionary<Quality.QualityType, Quality> _listOfQualities;
		#endregion
		#region CONSTRUCTORS
		public Qualities(int strength = 0, int agility = 0, int constitution = 0, int wisdom = 0, int courage = 0) {
			_strength = new Quality(Quality.QualityType.Strength, strength);
			_agility = new Quality(Quality.QualityType.Agility, agility);
			_constitution = new Quality(Quality.QualityType.Constitution, constitution);
			_wisdom = new Quality(Quality.QualityType.Wisdom, wisdom);
			_courage = new Quality(Quality.QualityType.Courage, courage);
			_listOfQualities = new Dictionary<Quality.QualityType, Quality>() {
				[Quality.QualityType.Strength] = _strength,
				[Quality.QualityType.Agility] = _agility,
				[Quality.QualityType.Constitution] = _constitution,
				[Quality.QualityType.Wisdom] = _wisdom,
				[Quality.QualityType.Courage] =_courage
			};
		}
		public Qualities(QualitiesData qualitiesData) {
			_strength = new Quality(Quality.QualityType.Strength, qualitiesData.strength);
			_agility = new Quality(Quality.QualityType.Agility, qualitiesData.agility);
			_constitution = new Quality(Quality.QualityType.Constitution, qualitiesData.constitution);
			_wisdom = new Quality(Quality.QualityType.Wisdom, qualitiesData.wisdom);
			_courage = new Quality(Quality.QualityType.Courage, qualitiesData.courage);
			_listOfQualities = new Dictionary<Quality.QualityType, Quality>() {
				[Quality.QualityType.Strength] = _strength,
				[Quality.QualityType.Agility] = _agility,
				[Quality.QualityType.Constitution] = _constitution,
				[Quality.QualityType.Wisdom] = _wisdom,
				[Quality.QualityType.Courage] =_courage
			};
		}
		#endregion
		#region Indexers
		public Quality this[Quality.QualityType type] {
			get {
				return _listOfQualities[type];
			}
		}
		#endregion
	}
}
