namespace MmsAlgo.Core;

public class MajorityElementFinder
{
    public string FindElement(int[] sample, bool greedyFind = false)
    {
        var thresholdCount = sample.Length / 2M;
        var maxCount = 0;
        int? result = null;
        for (var i = 0; i < sample.Length; i++)
        {
            var currCount = 0;
            var currVal = sample[i];
            for (var j = i; j < sample.Length; j++)
            {
                if (currVal == sample[j])
                {
                    currCount += 1;
                }
            }

            if (currCount > maxCount || (greedyFind && currCount >= maxCount))
            {
                maxCount = currCount;
                result = currVal;
            }
        }

        if (maxCount < thresholdCount)
        {
            return "No majority element";
        }
        return result.ToString()!;
    }
}
