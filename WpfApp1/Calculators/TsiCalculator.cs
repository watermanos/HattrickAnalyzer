using HattrickAnalyzer.Core.Models;

namespace HattrickAnalyzer.Core.Calculators;

public static class TsiCalculator
{
    public static int GetBaseTSI(PlayerAnalysis p)
    {
        // αν έχει πραγματικό -> αυτό
        if (p.CurrentTSI > 0)
            return p.CurrentTSI;

        // youth -> estimated
        return ExpectedFromSkills(p);
    }
    public static int GetCurrentOrEstimated(PlayerAnalysis p)
    {
        // έχει ήδη πραγματικό tsi → μην το πειράξεις
        if (p.CurrentTSI > 0)
            return p.CurrentTSI;

        // youth → υπολόγισε
        return ExpectedFromSkills(p);
    }
    // ================= EXPECTED TSI FROM SKILLS (YOUTH MODE) =================
    public static int ExpectedFromSkills(PlayerAnalysis p)
    {
        return
            p.Playmaking * 900 +
            p.Scoring * 810 +
            p.Passing * 630 +
            p.Defending * 720 +
            p.Winger * 630;
    }


    // ================= DETECT REAL SKILL LEVEL (SENIOR MODE) =================
    public static double HiddenSkillFactor(PlayerAnalysis p)
    {
        if (p.CurrentTSI <= 0)
            return 1.0;

        double expected = ExpectedFromSkills(p);
        if (expected == 0)
            return 1.0;

        double factor = (double)p.CurrentTSI / expected;

        return Math.Clamp(factor, 0.80, 1.20);
    }

    // ================= FINAL PROJECTED TSI =================
    public static int CalculateProjected(PlayerAnalysis p)
    {
        double factor = HiddenSkillFactor(p);

        double rawProjected =
            (p.Playmaking * 1000 +
             p.Scoring * 900 +
             p.Passing * 700 +
             p.Defending * 800 +
             p.Winger * 700) * factor;

        int result = (int)rawProjected;
        if (p.CurrentTSI > 0)
        {
            int maxDelta = 10_000;
            int delta = result - p.CurrentTSI;
            if (delta > maxDelta) result = p.CurrentTSI + maxDelta;
            else if (delta < -maxDelta) result = p.CurrentTSI - maxDelta;
        }

        return result;
    }
}
