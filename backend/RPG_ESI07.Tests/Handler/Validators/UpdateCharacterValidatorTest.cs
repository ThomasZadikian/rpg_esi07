using Xunit;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Application.Validators;

namespace RPG_ESI07.Tests.Validators;

public class UpdateCharacterValidatorTests
{
    private readonly UpdateCharacterValidator _validator = new();

    [Fact]
    public void Validate_WithValidCommand_Succeeds()
    {
        // Arrange
        var command = new UpdateCharactersCommand(
            UserId: 1,
            CharacterName: "Hero",
            Level: 10,
            CurrentHP: 150,
            MaxHP: 150,
            CurrentMP: 80,
            MaxMP: 80,
            Strength: 20,
            Intelligence: 15,
            Speed: 15,
            Experience: 5000,
            Gold: 1000
        );

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithInvalidId_Fails()
    {
        // Arrange
        var command = new UpdateCharactersCommand(
            UserId: 0,
            CharacterName: "Hero",
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
        Assert.Contains(result.Errors, e => e.PropertyName == "UserId");
    }

    [Fact]
    public void Validate_WithEmptyCharacterName_Fails()
    {
        // Arrange
        var command = new UpdateCharactersCommand(
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
    }
}