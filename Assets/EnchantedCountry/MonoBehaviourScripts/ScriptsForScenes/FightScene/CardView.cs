using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EnchantedCountry.MonoBehaviourScripts.ScriptsForScenes.FightScene {
  public class CardView : MonoBehaviour {
    #region VIEW
    public void SetFieldsInCard(Sprite icon, string nameCard, float riskPoints, int armorClass) {
      _icon.sprite = icon;
      _name.text = nameCard;
      _riskPoints.text = riskPoints.ToString("n2");
      _armorClass.text = armorClass.ToString("n0");
    }
    #endregion
    #region FIELDS
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _riskPoints;
    [SerializeField]
    private TMP_Text _armorClass;
    #endregion
    #region MONOBEHAVIOR_METHODS
    private void OnEnable() {
      AddListeners();
    }

    private void OnDisable() {
      RemoveListeners();
    }
    #endregion
    #region HANDLERS
    private void AddListeners() {
      CombatController.AttackButtonClicked += OnAttackButtonClicked;
    }

    private void OnAttackButtonClicked(string obj, float riskPoints) {
      if (_name.text == obj) {
        _riskPoints.text = riskPoints.ToString("n2");
        if (riskPoints <= 0) {
          _icon.color = Color.red;
        }
      }
    }

    private void RemoveListeners() {
      CombatController.AttackButtonClicked -= OnAttackButtonClicked;
    }
    #endregion
  }
}