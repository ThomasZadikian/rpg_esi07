using Xunit;
using RPG_ESI07.Application.Commands.Characters;
using RPG_ESI07.Application.Validators;

namespace RPG_ESI07.Tests.Validators;

public class DeleteCharacterValidatorTests
{
    private readonly DeleteCharacterValidator _validator = new();

    [Fact]
    public void Validate_WithValidId_Succeeds()
    {
        // Arrange
        var command = new DeleteCharacterCommand(1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithInvalidId_Fails()
    {
        // Arrange
        var command = new DeleteCharacterCommand(0);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }

    [Fact]
    public void Validate_WithNegativeId_Fails()
    {
        // Arrange
        var command = new DeleteCharacterCommand(-1);

        // Act
        var result = _validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }
}