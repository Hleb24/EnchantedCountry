using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters {
  public class NpcEquipmentsModel {
    public NpcEquipmentsModel([NotNull] IEnumerable<int> weaponsIdList, int classOfArmor) {
      ClassOfArmor = classOfArmor;
      WeaponsIdList = weaponsIdList;
    }

    public int ClassOfArmor { get; }
    public IEnumerable<int> WeaponsIdList { get; }
  }
}