using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Windows;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using HattrickAnalyzer.Core.Calculators;
using HattrickAnalyzer.Core.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace HattrickAnalyzer.Wpf;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<PlayerAnalysis> _players = new();

    public MainWindow()
    {
        InitializeComponent();
        PlayersGrid.ItemsSource = _players;
    }

    private void Analyze_Click(object sender, RoutedEventArgs e)
    {
        // Commit any pending edits in the DataGrid so values entered by user are applied to the bound object
        PlayersGrid.CommitEdit();
        PlayersGrid.CommitEdit(DataGridEditingUnit.Row, true);

        // If a row is selected analyze that row, otherwise analyze all rows
        if (PlayersGrid.SelectedItem is PlayerAnalysis selected)
            AnalyzePlayerInPlace(selected);
        else
        {
            foreach (var p in _players)
                AnalyzePlayerInPlace(p);
        }
    }

    // ➕ ADD PLAYER (adds an empty row for the user to fill)
    private void AddPlayer_Click(object sender, RoutedEventArgs e)
    {
        _players.Add(new PlayerAnalysis()); // blank editable row
        // Optionally select the newly added row:
        PlayersGrid.SelectedItem = _players.Last();
        PlayersGrid.ScrollIntoView(_players.Last());
    }

    // 📥 IMPORT CSV
    private void ImportCsv_Click(object sender, RoutedEventArgs e)
    {

        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv"
        };

        if (dialog.ShowDialog() != true)
            return;


        using var reader = new StreamReader(dialog.FileName);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Allow missing columns/fields without throwing and ignore bad data/blank lines
            MissingFieldFound = null,
            HeaderValidated = null,
            BadDataFound = null,
            IgnoreBlankLines = true
        };
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<PlayerAnalysisMap>();
        var players = csv.GetRecords<PlayerAnalysis>().ToList();

        foreach (var p in players)
            _players.Add(p);
    }

    // 📤 EXPORT EXCEL
    private void ExportExcel_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "Excel (*.xlsx)|*.xlsx",
            FileName = "HattrickResults.xlsx"
        };

        if (dialog.ShowDialog() != true)
            return;

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Results");

        ws.Cell(1, 1).Value = "Name";
        ws.Cell(1, 2).Value = "ageYears";
        ws.Cell(1, 3).Value = "ageDays";
        ws.Cell(1, 4).Value = "playmaking"; 
        ws.Cell(1, 5).Value = "scoring";
        ws.Cell(1, 6).Value = "passing";
        ws.Cell(1, 7).Value = "defending";
        ws.Cell(1, 8).Value = "winger";
        ws.Cell(1, 9).Value = "TSI Now";
        ws.Cell(1, 10).Value = "TSI Projected";
        ws.Cell(1, 11).Value = "Value";
        ws.Cell(1, 12).Value = "Training";

        for (int i = 0; i < _players.Count; i++)
        {
            var r = _players[i];
            ws.Cell(i + 2, 1).Value = r.Name;
            ws.Cell(i + 2, 2).Value = r.AgeYears;
            ws.Cell(i + 2, 3).Value = r.AgeDays;
            ws.Cell(i + 2, 4).Value = r.Playmaking;
            ws.Cell(i + 2, 5).Value = r.Scoring;
            ws.Cell(i + 2, 6).Value = r.Passing;
            ws.Cell(i + 2, 7).Value = r.Defending;
            ws.Cell(i + 2, 8).Value = r.Winger;
            ws.Cell(i + 2, 9).Value = r.TsiNow;
            ws.Cell(i + 2, 10).Value = r.TsiProjected;
            ws.Cell(i + 2, 11).Value = r.Value;
            ws.Cell(i + 2, 12).Value = r.Training;
        }

        var range = ws.Range(1, 1, _players.Count + 1, 12);
        var table = range.CreateTable("PlayersTable");
        table.Theme = XLTableTheme.TableStyleMedium9;

        ws.Columns().AdjustToContents();

        wb.SaveAs(dialog.FileName);
        MessageBox.Show("Excel export completed ✔");
    }

    // Analyze a single player and write results into the same object (so the grid updates)
    private void AnalyzePlayerInPlace(PlayerAnalysis p)
    {
        p.TSI = TsiCalculator.Calculate(p);

        var projected = TrainingCalculator.ProjectTSI(p, 10);
        var training = TrainingCalculator.RecommendTraining(p);
        var value = ValueCalculator.Estimate(projected, p.AgeYears);

        p.TsiNow = p.TSI;
        p.TsiProjected = projected;
        p.Value = value;
        p.Training = training;
    }

    private void PlayersGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
    }
}
