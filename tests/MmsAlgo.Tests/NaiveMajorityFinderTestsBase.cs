using MmsAlgo.Core;

namespace MmsAlgo.Tests;

public class NaiveMajorityFinderTestsBase : MajorityElementTestsBase
{
    public NaiveMajorityFinderTestsBase() : base(new FirstMajorityFinder())
    {
    }
}