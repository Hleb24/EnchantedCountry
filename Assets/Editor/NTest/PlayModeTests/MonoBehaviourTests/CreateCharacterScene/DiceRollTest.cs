using Core.Mono.Scenes.QualityDiceRoll;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.PlayModeTests.MonoBehaviourTests.CreateCharacterScene {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("CreateCharacter"), TestOf("DiceRoll")]
  public class DiceRollTest {
    #region Preparation_for_Tests
    private const string PATH_BY_DICE_ROLL = "ForTests/CreateCharacter/DiceRoll";
    private const string PATH_BY_BUTTON_DICE_ROLL = "ForTests/CreateCharacter/Button_DiceRoll";
    private QualityDiceRoll _qualityDiceRoll;

    [SetUp]
    public void InitFields() {
      _qualityDiceRoll = Object.Instantiate(Resources.Load<QualityDiceRoll>(PATH_BY_DICE_ROLL));
    }

    [TearDown]
    public void DeleteFields() {
      Object.Destroy(_qualityDiceRoll.gameObject);
    }
    #endregion

    #region Tests
    #endregion
  }
}