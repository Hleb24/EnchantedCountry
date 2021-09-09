using UnityEngine;

namespace Core.EnchantedCountry.SupportSystems {
	public static class PlayerPrefsTools {
		#region WRITTE_IN_PLAYER_PREFS
		public static void WritteInPlayerPrefs(string key, int value, bool addValues = false) {
			if (addValues) {
				PlayerPrefs.SetInt(key, value += PlayerPrefs.GetInt(key, default));
				PlayerPrefs.Save();
				return;
			} else {
				PlayerPrefs.SetInt(key, value);
				PlayerPrefs.Save();
				return;
			}
		}

		public static void WritteInPlayerPrefs(string key, float value, bool addValues = false) {
			if (addValues) {
				PlayerPrefs.SetFloat(key, value += PlayerPrefs.GetFloat(key, default));
				PlayerPrefs.Save();
				return;
			} else {
				PlayerPrefs.SetFloat(key, value);
				PlayerPrefs.Save();
				return;
			}
		}

		public static void WritteInPlayerPrefs(string key, string value, bool addValues = false, bool withSpace = false) {
			if (addValues) {
				string prefValue = PlayerPrefs.GetString(key, string.Empty);
				if (withSpace) {
					PlayerPrefs.SetString(key, prefValue += " " + value);
					PlayerPrefs.Save();
					return;
				}
				PlayerPrefs.SetString(key, prefValue += value);
				PlayerPrefs.Save();
				return;
			} else {
				PlayerPrefs.SetString(key, value);
				PlayerPrefs.Save();
				return;
			}
		}
		#endregion
		#region READ_FROM_PLAYER_PREFS
		#region OUT
		public static void ReadFromPlayerPrefs(string key, out int value) {
			value = PlayerPrefs.GetInt(key, default);
		}

		public static void ReadFromPlayerPrefs(string key, out float value) {
			value = PlayerPrefs.GetFloat(key, default);
		}
		public static void ReadFromPlayerPrefs(string key, out string value) {
			value = PlayerPrefs.GetString(key, string.Empty);
		}
		#endregion
		#region REF
		public static void ReadFromPlayerPrefsRef(string key, ref int value) {
			value = PlayerPrefs.GetInt(key, default);
		}

		public static void ReadFromPlayerPrefsRef(string key, ref float value) {
			value = PlayerPrefs.GetFloat(key, default);
		}

		public static void ReadFromPlayerPrefsRef(string key, ref string value) {
			value = PlayerPrefs.GetString(key, string.Empty);
		}
		#endregion
		#endregion
	}
}
