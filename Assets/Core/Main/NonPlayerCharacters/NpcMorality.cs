using System;
using System.Collections.Generic;
using Aberrance.Extensions;
using JetBrains.Annotations;

namespace Core.Main.NonPlayerCharacters {
  public class NpcMorality {
    private readonly int _morality;
    private readonly bool _immoral;
    private readonly bool _dontRunAway;
    private readonly List<int> _escapePossibility;

    public NpcMorality([NotNull] NpcMoralityModel model) {
      _escapePossibility = model.EscapePossibility ?? throw new ArgumentNullException(nameof(model.EscapePossibility));
      _morality = model.Morality;
      _immoral = model.Immoral;
      _dontRunAway = model.DontRunAway;
    }

    public bool IsRunAway(int checkValue) {
      if (_immoral) {
        return false;
      }

      return _escapePossibility.Contains(checkValue);
    }

    public int GetMorality() {
      return _morality;
    }

    public bool IsImmoral() {
      return _immoral;
    }

    public bool CanRunAway() {
      return !_dontRunAway;
    }
  }
  
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