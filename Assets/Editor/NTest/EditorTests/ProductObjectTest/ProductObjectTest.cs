using Core.SO.Product;
using Core.SO.Weapon;
using NUnit.Framework;
using UnityEngine;

namespace Editor.NTest.EditorTests.ProductObjectTest {
  [Author("Hleb Cheliakh", "4elyah@gmail.com"), Category("GameRule"), TestOf("ProductObject")]
  public class ProductObjectTest {
    #region Tests
    [Test, Description("Test Resources Load And Get Name With Item"), Repeat(1)]
    public void TestResourcesLoadAndGetNameWithItem() {
      _productSO.SetPrice(_price);
      _productSO.SetProductType(_productType);
      _productSO.SetItem(Object.Instantiate(Resources.Load<WeaponSO>(pathByLongSword)));
      _productSO.OnEnable();
      Assert.That(_productSO.GetProductName(), Is.EqualTo(weaponName).Or.EqualTo(weaponNameWithinEffectName));
    }
    #endregion

    #region Preparation_for_Tests
    private ProductSO _productSO;
    private readonly ProductSO.ProductType _productType = ProductSO.ProductType.Weapon;
    private readonly int _price = 20;
    private readonly string weaponName = "Normal Long Sword";
    private readonly string weaponNameWithinEffectName = "Long Sword";
    private readonly string pathByLongSword = "Weapons/OneHanded/LongSword";

    [SetUp]
    public void InitFields() {
      _productSO = ScriptableObject.CreateInstance<ProductSO>();
    }

    [TearDown]
    public void DeleteFields() {
      _productSO = null;
    }
    #endregion
  }
}