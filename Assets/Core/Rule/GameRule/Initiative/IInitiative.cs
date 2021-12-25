using System;

namespace Core.Rule.GameRule.Initiative {
  public interface IInitiative : IComparable<IInitiative> {
    public int Initiative { get; set; }
  }
}