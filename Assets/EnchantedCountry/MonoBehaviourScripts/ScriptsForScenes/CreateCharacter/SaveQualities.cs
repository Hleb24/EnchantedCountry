using Core.EnchantedCountry.CoreEnchantedCountry.Character.Qualities;
using Core.EnchantedCountry.MonoBehaviourScripts.GameSaveSystem;
using Core.EnchantedCountry.SupportSystems.Data;
using Core.EnchantedCountry.SupportSystems.SaveSystem;
using UnityEngine;
using Zenject;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.CreateCharacter {
	public class SaveQualities : MonoBehaviour {
		private Qualities _qualities;
		[SerializeField]
		private bool _usedQualitiesData;
		private QualitiesData _qualitiesData;
		[Inject]
		private QualitiesAfterDistributing _qualitiesAfterDistributing;
		private void Start() {
			_qualitiesData = new QualitiesData();
			_qualities = new Qualities();
			_qualitiesAfterDistributing.Values = new int[5];
			AddListener();
		}

		private void OnDestroy() {
			RemoveListener();
		}

		private void AddListener() {
			ValuesSelectionForQualities.SaveQualities += OnSaveQualites;
		}

		private void RemoveListener() {
			ValuesSelectionForQualities.SaveQualities -= OnSaveQualites;
		}

		private void OnSaveQualites() {
			Invoke(nameof(LoadQualitiesAfterDistributing), 0.3f);
		}

		private void LoadQualitiesAfterDistributing() {
			if (_usedQualitiesData) {
				_qualitiesData = GSSSingleton.Singleton.GetQualitiesData();
				SetQualitiesInScriptableObject();
			} else {
				SaveSystem.LoadWithInvoke(_qualitiesAfterDistributing, SaveSystem.Constants.QUALITIES_AFTER_DISTRIBUTING,
					(nameInvoke, time) => Invoke(nameInvoke, time	), nameof(SetQualitiesInScriptableObject), 0.3f);
			}
		}

		private void SetQualitiesInScriptableObject() {
			Debug.Log("After Load");
			Debug.Log($"{_qualitiesData.strength} \t" +
				$"{_qualitiesData.agility} \t" +
				$"{_qualitiesData.constitution} \t" +
				$"{_qualitiesData.wisdom} \t" +
				$"{_qualitiesData.courage} \t");
			if (_usedQualitiesData) {
				_qualities[Quality.QualityType.Strength].ValueOfQuality = _qualitiesData.strength;
				_qualities[Quality.QualityType.Agility].ValueOfQuality = _qualitiesData.agility;
				_qualities[Quality.QualityType.Constitution].ValueOfQuality = _qualitiesData.constitution;
				_qualities[Quality.QualityType.Wisdom].ValueOfQuality = _qualitiesData.wisdom;
				_qualities[Quality.QualityType.Courage].ValueOfQuality = _qualitiesData.courage;
			} else {
				_qualities[Quality.QualityType.Strength].ValueOfQuality = _qualitiesAfterDistributing.Values[0];
				_qualities[Quality.QualityType.Agility].ValueOfQuality = _qualitiesAfterDistributing.Values[1];
				_qualities[Quality.QualityType.Constitution].ValueOfQuality = _qualitiesAfterDistributing.Values[2];
				_qualities[Quality.QualityType.Wisdom].ValueOfQuality = _qualitiesAfterDistributing.Values[3];
				_qualities[Quality.QualityType.Courage].ValueOfQuality = _qualitiesAfterDistributing.Values[4];
			}
		}
	}

	
}