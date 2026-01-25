namespace HattrickAnalyzer.Core.Youth;

public static class YouthAnalyzer
{
    public static string Analyze(int maxPotential, int age)
    {
        if (maxPotential >= 7 && age <= 16)
            return "🔥 Κράτα τον – υψηλή προοπτική";

        if (maxPotential >= 6)
            return "⚠ Οριακός – εξαρτάται από training";

        return "❌ Άφησέ τον";
    }
}
