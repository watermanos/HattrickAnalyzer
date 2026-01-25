using HattrickAnalyzer.Core.Models;

namespace HattrickAnalyzer.Core.Calculators;

public static class TrainingCalculator
{
    public static int ProjectTSI(PlayerAnalysis p, int weeks)
    {
        double ageFactor =
            p.AgeYears < 18 ? 1.3 :
            p.AgeYears < 21 ? 1.1 :
            p.AgeYears < 25 ? 0.9 : 0.6;

        int weeklyGain = (int)(200 * ageFactor);
        return p.TSI + weeklyGain * weeks;
    }

    public static string RecommendTraining(PlayerAnalysis p)
    {
        var skills = new Dictionary<string, int>
        {
            { "Playmaking", p.Playmaking },
            { "Scoring", p.Scoring },
            { "Passing", p.Passing },
            { "Defending", p.Defending },
            { "Winger", p.Winger }
        };

        return skills.OrderBy(x => x.Value).First().Key;
    }
}
