using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Core.EnchantedCountry.SupportSystems;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckChildName : MonoBehaviour {
	[FormerlySerializedAs("_child")]
	[SerializeField]
	private GameObject _chil;
	[SerializeField]
	private float one;

	private void Start() {
		Debug.LogError(this.name);
		Debug.LogError(this.GetType());
		Debug.LogError(this.transform.position);
		Debug.LogError(nameof(_chil));
	}
	public void OnValidate() {
		GenericTools.FindAndSetGameObject<CheckChildName>(this, ref _chil, "Child");
	}

	[ContextMenu("Child")]
	private void Child() {
		Debug.LogError(PlayerPrefs.GetString(nameof(_chil)));
	}
}


