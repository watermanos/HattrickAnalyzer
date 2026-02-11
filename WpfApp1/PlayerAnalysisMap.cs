using CsvHelper.Configuration;

namespace HattrickAnalyzer.Core.Models;

public sealed class PlayerAnalysisMap : ClassMap<PlayerAnalysis>
{

    public PlayerAnalysisMap()
    {
        Map(m => m.Name).Name("Name", "Όνομα");

        Map(m => m.AgeYears).Name("AgeYears", "Ηλικία").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
        Map(m => m.AgeDays).Name("AgeDays", "Ημερών").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
        Map(m => m.Playmaking).Name("Playmaking", "Οργάνωση").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
        Map(m => m.Scoring).Name("Scoring", "Σκοράρισμα").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
        Map(m => m.Passing).Name("Passing", "Πάσα").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
        Map(m => m.Defending).Name("Defending", "Άμυνα").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
        Map(m => m.Winger).Name("Winger", "Πλάγια").TypeConverter<IntZeroOnInvalidConverter>().Default(0);

        Map(m => m.CurrentTSI).Name("TSI", "ΤSI").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
       // Map(m => m.CurrentTSI).Name("TsiNow", "ΤSI τώρα").TypeConverter<IntZeroOnInvalidConverter>().Default(0);
        Map(m => m.ProjectedTSI).Name("TsiProjected", "Προβολή ΤSI").TypeConverter<IntZeroOnInvalidConverter>().Default(0);

        Map(m => m.Value).Name("Value", "TSI value", "Αξία").TypeConverter<DoubleZeroOnInvalidConverter>().Default(0.0);

        // Add other mappings as needed
    }
}