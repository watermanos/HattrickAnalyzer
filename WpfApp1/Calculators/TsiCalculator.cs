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
        double SkillValue(double s) => Math.Pow(s, 1.25);

        double expected =
            SkillValue(p.Playmaking) * 950 +
            SkillValue(p.Scoring) * 850 +
            SkillValue(p.Passing) * 650 +
            SkillValue(p.Defending) * 750 +
            SkillValue(p.Winger) * 650;

        // --- πιο ρεαλιστικοί παράγοντες ---

        // Stamina: 0.95–1.05 range (±5%)
        double staminaFactor = 0.95 + (Math.Min(Math.Max(p.Stamina, 1), 10) - 5) * 0.01;

        // Experience: 0.98–1.05 range (μέχρι +5%)
        double expFactor = 0.98 + (Math.Min(Math.Max(p.Experience, 1), 10) * 0.007);

        // Age penalty: ~2% μείωση ανά χρόνο μετά τα 27, μέχρι -30% max
        double agePenalty = p.AgeYears > 27
            ? Math.Max(1.0 - (p.AgeYears - 27) * 0.02, 0.7)
            : 1.0;

        expected *= staminaFactor * expFactor * agePenalty;

        return (int)expected;
    }


    // ================= DETECT REAL SKILL LEVEL (SENIOR MODE) =================
    public static double HiddenSkillFactor(PlayerAnalysis p)
    {
        if (p.CurrentTSI <= 0)
            return 1.0;

        double expected = ExpectedFromSkills(p);
        if (expected == 0)
            return 1.0;

        double rawFactor = (double)p.CurrentTSI / expected;
        double factor = 1.0 + (rawFactor - 1.0) * 0.5;

        return Math.Clamp(factor, 0.85, 1.15);
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
            double maxDiff = p.CurrentTSI * 0.10; // 10%
            if (result > p.CurrentTSI + maxDiff)
                result = (int)(p.CurrentTSI + maxDiff);
            else if (result < p.CurrentTSI - maxDiff)
                result = (int)(p.CurrentTSI - maxDiff);
        }


        return result;
    }
}
