using Core.Rule.Character.GamePoints;
using NUnit.Framework;

namespace Editor.NTest.PlayModeTests.CharacterTests.GamePointsTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("Character"), TestOf("GamePoints")]
  public class GamePointsTest {
    #region Tests
    [Test, Description("Game point at least 0"), Repeat(1)]
    public void GamePointsAtLeastZero([Values(0, 1, 2, 3, 4, 5)] int points) {
      Assert.That(_gamePoints.Points, Is.EqualTo(0));
      _gamePoints.Points = points;
      Assert.That(_gamePoints.Points, Is.AtLeast(0));
    }
    #endregion

    #region Preparation_for_Tests
    private GamePoints _gamePoints;

    [SetUp]
    public void InitFields() {
      _gamePoints = new GamePoints();
    }

    [TearDown]
    public void DeleteFields() {
      _gamePoints = null;
    }
    #endregion
  }
}