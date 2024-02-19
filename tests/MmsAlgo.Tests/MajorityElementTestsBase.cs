using MmsAlgo.Core;

namespace MmsAlgo.Tests;

public abstract class MajorityElementTestsBase
{
    protected readonly IMajorityElementFinder _finder;

    protected MajorityElementTestsBase(IMajorityElementFinder finder)
    {
        _finder = finder;
    }

    [Fact]
    public virtual void Example1_Returns_Four()
    {
        var sample = new int[] { 3, 3, 4, 2, 4, 4, 2, 4, 4 };
        var result = _finder.FindElement(sample);
        Assert.Equal("4", result);
    }

    [Fact]
    public virtual void Example2_Returns_404()
    {
        var sample = new int[] { 3, 3, 4, 2, 4, 2, 2 };
        var result = _finder.FindElement(sample);
        Assert.Equal("No majority element", result);
    }
}