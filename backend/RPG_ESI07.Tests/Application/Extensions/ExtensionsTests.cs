using FluentAssertions;
using RPG_ESI07.Application.Extensions;
using RPG_ESI07.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RPG_ESI07.Tests.Application.Extensions;

public class ExtensionsTests
{
    [Fact]
    public void PaginationExtensions_ToPaginatedResponse_ReturnsCorrectSubset()
    {
        var source = Enumerable.Range(1, 20).ToList();
        var result = source.ToPaginatedResponse(2, 5);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(20);
        result.Items.Should().BeEquivalentTo(new[] { 6, 7, 8, 9, 10 });
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("test", false)]
    public void StringExtensions_IsNullOrEmpty_EvaluatesCorrectly(string value, bool expected)
    {
        value.IsNullOrEmpty().Should().Be(expected);
    }

    [Theory]
    [InlineData("hello", "Hello")]
    [InlineData("WORLD", "WORLD")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void StringExtensions_Capitalize_FormatsCorrectly(string value, string expected)
    {
        value.Capitalize().Should().Be(expected);
    }

    [Fact]
    public void StringExtensions_ToTitleCase_FormatsCorrectly()
    {
        "hello world test".ToTitleCase().Should().Be("Hello World Test");
    }

    [Fact]
    public void CollectionExtensions_Batch_GroupsItemsCorrectly()
    {
        var source = Enumerable.Range(1, 10);
        var result = source.Batch(3).ToList();

        result.Should().HaveCount(4);
        result[0].Should().HaveCount(3);
        result[3].Should().HaveCount(1).And.Contain(10);
    }

    [Fact]
    public void CollectionExtensions_GetOrDefault_ReturnsValueOrFallback()
    {
        var source = new[] { "A", "B" };
        source.GetOrDefault(1, "Fallback").Should().Be("B");
        source.GetOrDefault(5, "Fallback").Should().Be("Fallback");
    }

    [Fact]
    public void DateTimeExtensions_EvaluateRelativeTimeCorrectly()
    {
        DateTime.UtcNow.AddDays(-1).IsInPast().Should().BeTrue();
        DateTime.UtcNow.AddDays(1).IsInFuture().Should().BeTrue();
        DateTime.Today.IsToday().Should().BeTrue();
    }

    [Theory]
    [InlineData(5, 1, 10, true)]
    [InlineData(0, 1, 10, false)]
    public void NumericExtensions_IsBetween_EvaluatesCorrectly(int value, int min, int max, bool expected)
    {
        value.IsBetween(min, max).Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 1, 10, 5)]
    [InlineData(0, 1, 10, 1)]
    [InlineData(15, 1, 10, 10)]
    public void NumericExtensions_Clamp_RestrictsValues(int value, int min, int max, int expected)
    {
        value.Clamp(min, max).Should().Be(expected);
    }

    [Fact]
    public void NumericExtensions_AsPercentageOf_CalculatesCorrectly()
    {
        50.AsPercentageOf(200).Should().Be(25.00m);
        10.AsPercentageOf(0).Should().Be(0m);
    }

    [Fact]
    public void ApiResponseExtensions_AsSuccess_WrapsData()
    {
        var result = "Data".AsSuccess("OK");
        result.Success.Should().BeTrue();
        result.Data.Should().Be("Data");
        result.Message.Should().Be("OK");
    }

    [Fact]
    public void ApiResponseExtensions_AsError_CreatesErrorResponse()
    {
        var result = "Details".AsError<object>("Failure");

        result.Success.Should().BeFalse();
        result.Message.Should().Be("Failure");
        result.Errors.Should().Contain("Details");
    }
}