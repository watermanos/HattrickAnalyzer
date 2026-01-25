using HattrickAnalyzer.Core.Models;

namespace HattrickAnalyzer.Core.Calculators;

public static class TsiCalculator
{
    public static int Calculate(PlayerAnalysis p)
    {
        return
            p.Playmaking * 1000 +
            p.Scoring * 900 +
            p.Passing * 700 +
            p.Defending * 800 +
            p.Winger * 700;
    }
}
