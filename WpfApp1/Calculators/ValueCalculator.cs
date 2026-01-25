namespace HattrickAnalyzer.Core.Calculators;

public static class ValueCalculator
{
    public static double Estimate(int tsi, int age)
    {
        double ageFactor = age < 21 ? 1.4 :
                           age < 25 ? 1.1 : 0.7;

        return tsi * ageFactor * 0.02;
    }
}
