using Core.ScriptableObject.Products;
using Core.ScriptableObject.Weapon;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.EditorTests.ProductObjectTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("ProductObject")]
  public class ProductObjectTest {
    #region Tests
    [Test, Description("Test Resources Load And Get Name With Item"), Repeat(1)]
    public void TestResourcesLoadAndGetNameWithItem() {
      _productObject.price = _price;
      _productObject.productType = _productType;
      _productObject.item = Object.Instantiate(Resources.Load<WeaponObject>(pathByLongSword));
      _productObject.OnEnable();
      Assert.That(_productObject.productName, Is.EqualTo(weaponName).Or.EqualTo(weaponNameWithinEffectName));
    }
    #endregion

    #region Preparation_for_Tests
    private ProductObject _productObject;
    private readonly ProductObject.ProductType _productType = ProductObject.ProductType.Weapon;
    private readonly int _price = 20;
    private readonly string weaponName = "Normal Long Sword";
    private readonly string weaponNameWithinEffectName = "Long Sword";
    private readonly string pathByLongSword = "Weapons/OneHanded/LongSword";

    [SetUp]
    public void InitFields() {
      _productObject = ScriptableObject.CreateInstance<ProductObject>();
    }

    [TearDown]
    public void DeleteFields() {
      _productObject = null;
    }
    #endregion
  }
}