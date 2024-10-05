using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SatisServer.UI.Logic;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace SatisServer.UI.CustomControls;

public partial class RamRealtimeViewModel : ObservableObject
{
    private readonly List<DateTimePoint> _values = [];
    private readonly DateTimeAxis _customAxis;

    public RamRealtimeViewModel()
    {
        Series =
        [
            new LineSeries<DateTimePoint>
            {
                Values = _values,
                Stroke = new SolidColorPaint(SKColors.Cyan, 2),
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];

        _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
        {
            CustomSeparators = GetSeparators(),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            SeparatorsPaint = new SolidColorPaint(SKColors.Red),

            LabelsPaint = new SolidColorPaint(SKColors.LightGray),
            TextSize = 10
        };

        XAxes = [_customAxis];

        ServerControl.OnServerStarted += OnServerStart;
        ServerControl.OnServerStopped += OnServerStop;
        ServerControl.OnDetectServerOnline += OnServerOnline;
    }

    public ObservableCollection<ISeries> Series { get; set; }

    public Axis[] XAxes { get; set; }

    public object Sync { get; } = new object();

    public bool IsReading { get; set; } = true;

    private bool _isReadingRightNow = false;

    private void OnServerStart(object? sender, EventArgs e)
    {
        IsReading = true;
    }

    private void OnServerStop(object? sender, EventArgs e)
    {
        IsReading = false;
    }

    private void OnServerOnline(object? sender, EventArgs e)
    {
        _ = ReadData();
    }

    private async Task ReadData()
    {
        if (_isReadingRightNow) return;
        _isReadingRightNow = true;
        // to keep this sample simple, we run the next infinite loop 
        // in a real application you should stop the loop/task when the view is disposed 

        while (IsReading)
        {
            await Task.Delay(100);

            // Because we are updating the chart from a different thread 
            // we need to use a lock to access the chart data. 
            // this is not necessary if your changes are made in the UI thread. 
            lock (Sync)
            {
                var now = DateTime.UtcNow;
                ServerControl.GetRamUsage(out var ramUsage);

                ramUsage = Math.Round(ramUsage, 2);

                _values.Add(new DateTimePoint(now, ramUsage));
                _values.RemoveAll(r => r.DateTime < now.AddSeconds(-60));

                // we need to update the separators every time we add a new point 
                _customAxis.CustomSeparators = GetSeparators();
            }
        }

        _values.Clear();
        _isReadingRightNow = false;
    }

    private static double[] GetSeparators()
    {
        var now = DateTime.UtcNow;

        return
        [
            now.AddSeconds(-60).Ticks,
            now.AddSeconds(-55).Ticks,
            now.AddSeconds(-50).Ticks,
            now.AddSeconds(-45).Ticks,
            now.AddSeconds(-40).Ticks,
            now.AddSeconds(-35).Ticks,
            now.AddSeconds(-30).Ticks,
            now.AddSeconds(-25).Ticks,
            now.AddSeconds(-20).Ticks,
            now.AddSeconds(-15).Ticks,
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
        ];
    }

    private static string Formatter(DateTime date)
    {
        var secsAgo = (DateTime.UtcNow - date).TotalSeconds;

        return secsAgo < 1
            ? "now"
            : $"{secsAgo:N0}s";
    }
}
