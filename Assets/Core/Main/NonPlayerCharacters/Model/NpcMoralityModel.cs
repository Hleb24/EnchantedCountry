using System;
using System.Collections.Generic;
using Aberrance.Extensions;

namespace Core.Main.NonPlayerCharacters.Model {
  public class NpcMoralityModel {
    public NpcMoralityModel(int morality, bool immoral, List<int> escapePossibility) {
      Morality = morality;
      Immoral = immoral;
      EscapePossibility = escapePossibility ?? throw new ArgumentNullException(nameof(escapePossibility));
      DontRunAway = IsDontRunAway();

      bool IsDontRunAway() {
        return EscapePossibility.Empty();
      }
    }

    public int Morality { get; }

    public bool Immoral { get; }

    public bool DontRunAway { get; }

    public List<int> EscapePossibility { get; }
  }
}