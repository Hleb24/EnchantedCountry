using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.SupportSystems {
	public static class GenericTools {
		#region FIND_ONE
		public static void FindAndSetGameObject<T>(T thisType, ref GameObject gObject, string firstName) where T : MonoBehaviour {
			System.Type type = thisType.GetType();
			if (gObject != null) {
				if (PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName}") != gObject.name) {
					PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName}", gObject.name);
					Debug.LogError(($"{thisType.name}.{type}.{firstName}"));
				}
			} else {
				gObject = GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName}", firstName)).gameObject;
				PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName}", gObject.name);
				Debug.LogError($"{thisType.name}.{type}.{firstName}");
			}
		}

		public static void FindAndSetComponent<T, V>(T thisType, ref V component, string firstName) where T : MonoBehaviour where V : MonoBehaviour {
			System.Type type = thisType.GetType();
			if (component != null) {
				if (PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName}") != component.name) {
					PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName}", component.name);
					Debug.LogError(($"{thisType.name}.{type}.{firstName}"));
				}
			} else {
				component = GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName}", firstName)).gameObject.GetComponent<V>();
				PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName}", component.name);
				Debug.LogError($"{thisType.name}.{type}.{firstName}");
			}
		}
		#endregion
		#region FIND_LIST
		public static void FindAndSetGameObjectList<T>(T thisType, ref List<GameObject> gObject, params string[] firstName) where T : MonoBehaviour {
			System.Type type = thisType.GetType();
			if (gObject != null) {
				for (int i = 0; i < firstName.Length; i++) {
					if (gObject[i] == null) {
						gObject.Add(GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}", firstName[i])).gameObject);
						PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
					}
					if (PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}") != gObject[i].name) {
						PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
						Debug.LogError(($"{thisType.name}.{type}.{firstName[i]}"));
					}
				}
			} else {
				gObject = new List<GameObject>();
				for (int i = 0; i < firstName.Length; i++) {
					gObject.Add(GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}", firstName[i])).gameObject);
					PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
					Debug.LogError($"{thisType.name}.{type}.{firstName[i]}");
				}
			}
		}

		public static void FindAndSetComponentList<T, V>(T thisType, ref List<V> gObject, params string[] firstName) where T : MonoBehaviour where V : MonoBehaviour {
			System.Type type = thisType.GetType();
			if (gObject != null) {
				for (int i = 0; i < firstName.Length; i++) {
					try {
						if (gObject.Count != firstName.Length) {
						
						gObject.Add(GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}", firstName[i])).GetComponent<V>());
						
						PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
					}
					if (PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}") != gObject[i].name) {
						PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
						Debug.LogError(($"{thisType.name}.{type}.{firstName[i]}"));
					}
						}catch(Exception ex) {
						Debug.Log(ex.Message);
					}
				}
			} else {
				gObject = new List<V>();
				for (int i = 0; i < firstName.Length; i++) {
					gObject.Add(GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}", firstName[i])).GetComponent<V>());
					PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
					Debug.LogError($"{thisType.name}.{type}.{firstName[i]}");
				}
			}
		}

		public static void FindAndSetComponentArray<T, V>(T thisType, ref V[] gObject, params string[] firstName) where T : MonoBehaviour where V : MonoBehaviour {
			System.Type type = thisType.GetType();
			if (gObject != null) {
				if (gObject.Length != firstName.Length) {
					gObject = new V[firstName.Length];
				}
				for (int i = 0; i < firstName.Length; i++) {
					gObject[i] = GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}", firstName[i])).GetComponent<V>();
					PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
					if (PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}") != gObject[i].name) {
						PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
						Debug.LogError(($"{thisType.name}.{type}.{firstName[i]}"));
					}
				}
			} else {
				gObject = new V[firstName.Length];
				for (int i = 0; i < firstName.Length; i++) {
					gObject[i] = GameObject.Find(PlayerPrefs.GetString($"{thisType.name}.{type}.{firstName[i]}", firstName[i])).GetComponent<V>();
					PlayerPrefs.SetString($"{thisType.name}.{type}.{firstName[i]}", gObject[i].name);
					Debug.LogError($"{thisType.name}.{type}.{firstName[i]}");
				}
			}
		}
		#endregion

		public static void SetUIText(Text uiText, string info, bool add = false) {
			if (add) {
				if (uiText.text.Contains(info))
					return;
				uiText.text += " " + info;
				return;
			}
			uiText.text = info;
		}
		
		public static void SetUIText(TMP_Text uiText, string info, bool add = false) {
			if (add) {
				if (uiText.text.Contains(info))
					return;
				uiText.text += " " + info;
				return;
			}
			uiText.text = info;
		}
	}
}
