using System;
using Core.Main.Dice;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Core.Main.Character.Class {
  public enum ClassType {
    Warrior = 0,
    Elf = 1,
    Wizard = 2,
    Kron = 3,
    Gnom = 4,
    Human = 5
  }

  public abstract class CharacterClass {
    protected SixSidedDice RiskDice { get; }
    public abstract ClassType ClassType { get; }
    public abstract int GetFirstRiskPoint();

    public CharacterClass([NotNull] SixSidedDice riskDice) {
      Assert.IsNotNull(riskDice, nameof(riskDice));
      RiskDice = riskDice;
    }
  }

  public static class CharacterClassBuilder {
    private static readonly Lazy<SixSidedDice> SidedDice = new(new SixSidedDice());

    public static CharacterClass GetCharacterClass(ClassType classType) {
      return classType switch {
               ClassType.Elf => new Elf(SidedDice.Value),
               ClassType.Gnom => new Gnom(SidedDice.Value),
               ClassType.Human => new Human(SidedDice.Value),
               ClassType.Kron => new Kron(SidedDice.Value),
               ClassType.Warrior => new Warrior(SidedDice.Value),
               ClassType.Wizard => new Wizard(SidedDice.Value),
               _ => new Human(SidedDice.Value)
             };
    }
  }

  public class Elf : CharacterClass {
    public override ClassType ClassType { get; } = ClassType.Elf;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(2) + 6;
    }

    public Elf([NotNull] SixSidedDice riskDice) : base(riskDice) { }
  }

  public class Wizard : CharacterClass {
    public override ClassType ClassType { get; } = ClassType.Wizard;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRoll() + 4;
    }

    public Wizard([NotNull] SixSidedDice riskDice) : base(riskDice) { }
  }

  public class Kron : CharacterClass {
    public override ClassType ClassType { get; } = ClassType.Kron;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(3) + 4;
    }

    public Kron([NotNull] SixSidedDice riskDice) : base(riskDice) { }
  }

  public class Gnom : CharacterClass {
    public override ClassType ClassType { get; } = ClassType.Gnom;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(3) + 5;
    }

    public Gnom([NotNull] SixSidedDice riskDice) : base(riskDice) { }
  }

  public class Human : CharacterClass {
    public override ClassType ClassType { get; } = ClassType.Human;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(3) + 4;
    }

    public Human([NotNull] SixSidedDice riskDice) : base(riskDice) { }
  }

  public class Warrior : CharacterClass {
    public override ClassType ClassType { get; } = ClassType.Warrior;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(2) + 6;
    }

    public Warrior([NotNull] SixSidedDice riskDice) : base(riskDice) { }
  }
}