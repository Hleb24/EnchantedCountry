using System;

namespace Core.Main.GameRule.Initiative {
  public interface IInitiative : IComparable<IInitiative> {
    public int Initiative { get; set; }
  }
}