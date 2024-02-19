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
            if (sample[i] == majority)
            {
                counter += 1;
            }
            else
            {
                counter -= 1;
            }
            if (counter == 0)
            {
                majority = sample[i];
                counter = 1;
            }
        }

        counter = 0;
        for (int i = 0; i < sample.Length; i++)
        {
            if (majority == sample[i])
            {
                counter += 1;
            }
        }

        if (counter <= threshold)
        {
            return "No majority element";
        }
        return majority.ToString();
    }
}