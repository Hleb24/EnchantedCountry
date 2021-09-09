using System;
using System.IO;
using UnityEngine;

namespace Core.EnchantedCountry.SupportSystems.SaveSystem {
	public static class SaveSystem {

		public static void Save<T>(T type, string path) {
			string json = JsonUtility.ToJson(type, true);
			// Debug.Log(json);
			try {
				using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + path, false, System.Text.Encoding.Default)) {
					sw.Write(json);
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
		}
		
		
		public static async void Load<T>(T type, string path) {
			if (File.Exists(Application.persistentDataPath + path)) {
				try {
					using (StreamReader sr = new StreamReader(Application.persistentDataPath + path)) {
						string json = await sr.ReadToEndAsync();
						// Debug.Log(json);
						JsonUtility.FromJsonOverwrite(json, type);
					}
				} catch (Exception e) {
					Console.WriteLine(e.Message);
				}
			} else {
				Debug.Log($"The path {Application.persistentDataPath + path} not exists.");
			}
		}
		
		public static async void LoadWithInvoke<T>(T type, string path, Action<string, float> invoke, string methodName, float invokeTime) {
			if (File.Exists(Application.persistentDataPath + path)) {
				try {
					using (StreamReader sr = new StreamReader(Application.persistentDataPath + path)) {
						string json = await sr.ReadToEndAsync();
						Debug.Log(json);
						JsonUtility.FromJsonOverwrite(json, type);
					}
				} catch (Exception e) {
					Console.WriteLine(e.Message);
				}
			} else {
				Debug.Log($"The path {Application.persistentDataPath + path} not exists.");
			}
			invoke?.Invoke(methodName, invokeTime);
		}
		public struct Constants {
			public const string DICE_ROll_DATA = "/diceRole.json";
			public const string QUALITIES_AFTER_DISTRIBUTING = "/qualitiesAfterDistributing.json";
			public const string CLASS_OF_CHARACTER = "/classOfCharacter.json";
			public const string WALLET = "/wallet.json";
			public const string RISK_POINTS = "/riskPoints.json";
			public const string USED_EQUIPMENT = "/usedEquipment.json";
			public const string GAME_POINTS = "/gamePoints.json";
			public const string GAME_SAVE = "/gameSave.json";
		}
	}
}
