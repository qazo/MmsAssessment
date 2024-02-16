using MmsAlgo.Core;

namespace MmsAlgo.Tests;

public class MajorityElementTests
{
    private readonly MajorityElementFinder _finder;

    public MajorityElementTests()
    {
        _finder = new();
    }

    [Fact]
    public void Example1_Returns_Four()
    {
        var sample = new int[] { 3, 3, 4, 2, 4, 4, 2, 4, 4 };
        var result = _finder.FindElement(sample);
        Assert.Equal("4", result);
    }

    [Fact]
    public void Example2_Returns_404()
    {
        var sample = new int[] { 3, 3, 4, 2, 4, 2, 2 };
        var result = _finder.FindElement(sample);
        Assert.Equal("No majority element", result);
    }

    [Fact]
    public void Will_Pick_First_Majority()
    {
        var sample = new int[] {2, 2, 4, 4};
        var result = _finder.FindElement(sample);
        Assert.Equal("2", result);
    }

    [Fact]
    public void Will_Pick_Last_Majority()
    {
        var sample = new int[] {2, 2, 4, 4};
        var result = _finder.FindElement(sample, greedyFind:true);
        Assert.Equal("4", result);
    }
}