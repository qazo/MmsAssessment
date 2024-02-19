using MmsAlgo.Core;

namespace MmsAlgo.Tests;

public class MajorityElementTests
{
    private readonly IMajorityElementFinder _finder;

    public MajorityElementTests()
    {
        _finder = new FirstMajorityFinder();
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
}