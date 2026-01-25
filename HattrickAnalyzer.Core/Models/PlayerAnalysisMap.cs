using CsvHelper.Configuration;

namespace HattrickAnalyzer.Core.Models;

public sealed class PlayerAnalysisMap : ClassMap<PlayerAnalysis>
{
    public PlayerAnalysisMap()
    {
        Map(m => m.Name).Name("Name", "Όνομα");
        Map(m => m.AgeYears).Name("AgeYears", "Ηλικία");
        Map(m => m.Playmaking).Name("Playmaking", "Δημιουργία");
        Map(m => m.Scoring).Name("Scoring", "Τελείωμα");
        Map(m => m.Passing).Name("Passing", "Πάσες");
        Map(m => m.Defending).Name("Defending", "Άμυνα");
        Map(m => m.Winger).Name("Winger", "Πλάγια");
        // Add other mappings as needed
    }
}