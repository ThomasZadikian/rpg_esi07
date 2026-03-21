using Xunit;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Application.Validators;

namespace RPG_ESI07.Tests.Validators;

public class CreateCharacterValidatorTests
{
    private readonly CreateCharacterValidator _validator = new();

    [Fact]
    public void Validate_WithValidCommand_Succeeds()
    {
        // Arrange
        var command = new CreateCharacterCommand(
            UserId: 1,
            CharacterName: "Hero",
            Level: 5,
            CurrentHP: 100,
            MaxHP: 100,
            CurrentMP: 50,
            MaxMP: 50,
            Strength: 15,
            Intelligence: 12,
            Speed: 10,
            Experience: 500,
            Gold: 100
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithEmptyCharacterName_Fails()
    {
        // Arrange
        var command = new CreateCharacterCommand(
            UserId: 1,
            CharacterName: "",
            Level: 1,
            CurrentHP: 100,
            MaxHP: 100,
            CurrentMP: 50,
            MaxMP: 50,
            Strength: 10,
            Intelligence: 10,
            Speed: 10,
            Experience: 0,
            Gold: 0
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CharacterName");
    }

    [Fact]
    public void Validate_WithCurrentHPGreaterThanMaxHP_Fails()
    {
        // Arrange
        var command = new CreateCharacterCommand(
            UserId: 1,
            CharacterName: "Hero",
            Level: 1,
            CurrentHP: 150,
            MaxHP: 100,
            CurrentMP: 50,
            MaxMP: 50,
            Strength: 10,
            Intelligence: 10,
            Speed: 10,
            Experience: 0,
            Gold: 0
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CurrentHP");
    }

    [Fact]
    public void Validate_WithLevelAbove100_Fails()
    {
        // Arrange
        var command = new CreateCharacterCommand(
            UserId: 1,
            CharacterName: "Hero",
            Level: 150,
            CurrentHP: 100,
            MaxHP: 100,
            CurrentMP: 50,
            MaxMP: 50,
            Strength: 10,
            Intelligence: 10,
            Speed: 10,
            Experience: 0,
            Gold: 0
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Level");
    }

    [Fact]
    public void Validate_WithNegativeExperience_Fails()
    {
        // Arrange
        var command = new CreateCharacterCommand(
            UserId: 1,
            CharacterName: "Hero",
            Level: 1,
            CurrentHP: 100,
            MaxHP: 100,
            CurrentMP: 50,
            MaxMP: 50,
            Strength: 10,
            Intelligence: 10,
            Speed: 10,
            Experience: -100,
            Gold: 0
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }
}