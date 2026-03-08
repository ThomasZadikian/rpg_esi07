using Xunit;
using FluentValidation.TestHelper;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Application.Validators;

namespace RPG_ESI07.Tests.Validators;

public class CreateEnemyValidatorTests
{
    private readonly CreateEnemyValidator _validator = new();

    [Fact]
    public void Validate_WithValidCommand_Succeeds()
    {
        // Arrange
        var command = new CreateEnemyCommand(
            Name: "Dragon",
            Type: "boss",
            MaxHP: 100,
            Strength: 50,
            Intelligence: 40,
            Speed: 30,
            PhysicalResistance: 1.5f,
            MagicalResistance: 1.0f,
            ExperienceReward: 1000,
            GoldReward: 500,
            Description: "A mighty dragon"
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyName_Fails()
    {
        // Arrange
        var command = new CreateEnemyCommand(
            Name: "",
            Type: "basic",
            MaxHP: 10,
            Strength: 5,
            Intelligence: 5,
            Speed: 10,
            PhysicalResistance: 1.0f,
            MagicalResistance: 1.0f,
            ExperienceReward: 50,
            GoldReward: 25,
            Description: null
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithInvalidType_Fails()
    {
        // Arrange
        var command = new CreateEnemyCommand(
            Name: "Ghost",
            Type: "legendary", // ← Invalid type
            MaxHP: 10,
            Strength: 5,
            Intelligence: 5,
            Speed: 10,
            PhysicalResistance: 1.0f,
            MagicalResistance: 1.0f,
            ExperienceReward: 50,
            GoldReward: 25,
            Description: null
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Type");
    }

    [Fact]
    public void Validate_WithZeroMaxHP_Fails()
    {
        // Arrange
        var command = new CreateEnemyCommand(
            Name: "Weak",
            Type: "basic",
            MaxHP: 0, // ← Invalid
            Strength: 5,
            Intelligence: 5,
            Speed: 10,
            PhysicalResistance: 1.0f,
            MagicalResistance: 1.0f,
            ExperienceReward: 50,
            GoldReward: 25,
            Description: null
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "MaxHP");
    }

    [Fact]
    public void Validate_WithInvalidResistance_Fails()
    {
        // Arrange
        var command = new CreateEnemyCommand(
            Name: "Fragile",
            Type: "basic",
            MaxHP: 10,
            Strength: 5,
            Intelligence: 5,
            Speed: 10,
            PhysicalResistance: 3.0f, // ← Out of range (0.5-2.0)
            MagicalResistance: 1.0f,
            ExperienceReward: 50,
            GoldReward: 25,
            Description: null
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "PhysicalResistance");
    }
}