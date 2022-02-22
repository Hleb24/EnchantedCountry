using Core.Main.GameRule.Item;

namespace Core.Main.NonPlayerCharacters.Item {
  public interface INpcWeaponSet {
    public Weapon GetNpcWeapon(int id);
  }
}