using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HattrickAnalyzer.Core.Models;

public class PlayerAnalysis : INotifyPropertyChanged
{

    private string _name = "...";
    public string Name { get => _name; set => Set(ref _name, value); }

    private int _ageYears;
    public int AgeYears { get => _ageYears; set => Set(ref _ageYears, value); }

    private int _ageDays;
    public int AgeDays { get => _ageDays; set => Set(ref _ageDays, value); }

    private int _playmaking;
    public int Playmaking { get => _playmaking; set => Set(ref _playmaking, value); }

    private int _scoring;
    public int Scoring { get => _scoring; set => Set(ref _scoring, value); }

    private int _passing;
    public int Passing { get => _passing; set => Set(ref _passing, value); }

    private int _defending;
    public int Defending { get => _defending; set => Set(ref _defending, value); }

    private int _winger;
    public int Winger { get => _winger; set => Set(ref _winger, value); }

    private int _tsi;
    public int TSI { get => _tsi; set => Set(ref _tsi, value); }

    private int _tsiNow;
    public int TsiNow { get => _tsiNow; set => Set(ref _tsiNow, value); }

    private int _tsiProjected;
    public int TsiProjected { get => _tsiProjected; set => Set(ref _tsiProjected, value); }

    private double _value;
    public double Value { get => _value; set => Set(ref _value, value); }

    private string _training = "";
    public string Training { get => _training; set => Set(ref _training, value); }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {

        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

