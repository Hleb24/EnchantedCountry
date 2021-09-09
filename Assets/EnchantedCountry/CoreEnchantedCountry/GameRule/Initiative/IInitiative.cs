using System;

namespace Core.EnchantedCountry.CoreEnchantedCountry.GameRule.Initiative {
  public interface IInitiative : IComparable<IInitiative> {
    public int Initiative { get; set; }
  }
}