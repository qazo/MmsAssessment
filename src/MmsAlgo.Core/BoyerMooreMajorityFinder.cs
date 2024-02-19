namespace MmsAlgo.Core;

public class BoyerMooreMajorityFinder : IMajorityElementFinder
{
    public string FindElement(int[] sample)
    {
        var threshold = sample.Length / 2M;
        var counter = 1;
        var majority = sample[0];
        for (int i = 1; i < sample.Length; i++)
        {
            if (counter == 0)
            {
                majority = sample[i];
                counter = 1;
            }
            
            if (sample[i] == majority)
            {
                counter += 1;
            }
            else
            {
                counter -= 1;
            }
        }

        if (majority <= threshold)
        {
            return "No majority element";
        }
        return majority.ToString();
    }
}