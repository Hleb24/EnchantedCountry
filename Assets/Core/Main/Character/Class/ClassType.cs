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
    public CharacterClass([NotNull] SixSidedDice riskDice) {
      Assert.IsNotNull(riskDice, nameof(riskDice));
      RiskDice = riskDice;
    }

    public abstract int GetFirstRiskPoint();
    protected SixSidedDice RiskDice { get; }
    public abstract ClassType ClassType { get; }
  }

  public static class CharacterClassBuilder {
    private static readonly Lazy<SixSidedDice> SidedDice = new(new SixSidedDice());

    public static CharacterClass GetCharacterClass(IClassType classType) {
      return classType.GetClassType() switch {
               ClassType.Elf => new Elf(SidedDice.Value),
               ClassType.Gnom => new Gnom(SidedDice.Value),
               ClassType.Human => new Human(SidedDice.Value),
               ClassType.Kron => new Kron(SidedDice.Value),
               ClassType.Warrior => new Warrior(SidedDice.Value),
               ClassType.Wizard => new Wizard(SidedDice.Value),
               _ => new Human(SidedDice.Value)
             };
    }

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
    public Elf([NotNull] SixSidedDice riskDice) : base(riskDice) { }
    public override ClassType ClassType { get; } = ClassType.Elf;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(2) + 6;
    }
  }

  public class Wizard : CharacterClass {
    public Wizard([NotNull] SixSidedDice riskDice) : base(riskDice) { }
    public override ClassType ClassType { get; } = ClassType.Wizard;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRoll() + 4;
    }
  }

  public class Kron : CharacterClass {
    public Kron([NotNull] SixSidedDice riskDice) : base(riskDice) { }
    public override ClassType ClassType { get; } = ClassType.Kron;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(3) + 4;
    }
  }

  public class Gnom : CharacterClass {
    public Gnom([NotNull] SixSidedDice riskDice) : base(riskDice) { }
    public override ClassType ClassType { get; } = ClassType.Gnom;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(3) + 5;
    }
  }

  public class Human : CharacterClass {
    public Human([NotNull] SixSidedDice riskDice) : base(riskDice) { }
    public override ClassType ClassType { get; } = ClassType.Human;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(3) + 4;
    }
  }

  public class Warrior : CharacterClass {
    public Warrior([NotNull] SixSidedDice riskDice) : base(riskDice) { }
    public override ClassType ClassType { get; } = ClassType.Warrior;

    public override int GetFirstRiskPoint() {
      return RiskDice.GetDiceRollAccordingToEdges(2) + 6;
    }
  }
}