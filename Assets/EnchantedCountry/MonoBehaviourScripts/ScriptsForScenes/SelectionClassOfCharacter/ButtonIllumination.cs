using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.SelectionClassOfCharacter {
  public class ButtonIllumination : MonoBehaviour {
    #region FIELDS
    private void ChangeColorForPressedButton() { }
    [Header("Set in Inspector"), SerializeField]
    private List<Button> _buttons;
    [FormerlySerializedAs("_colorOfChoise"),SerializeField]
    private Color _colorOfChoice;
    [SerializeField]
    private Color _defaultColor;
    #endregion
  }
}