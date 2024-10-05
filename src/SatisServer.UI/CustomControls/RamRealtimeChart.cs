using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;

namespace SatisServer.UI.CustomControls;

public partial class RamRealtimeChart : UserControl
{
    private readonly CartesianChart _cartesianChart;

    public RamRealtimeChart()
    {
        InitializeComponent();
        Size = new Size(50, 50);

        var viewModel = new RamRealtimeViewModel();

        _cartesianChart = new CartesianChart
        {
            SyncContext = viewModel.Sync,
            Series = viewModel.Series,
            XAxes = viewModel.XAxes,
            ForeColor = Color.White,
            YAxes =
            [
                new Axis
                {
                    MinLimit = 0,
                    MinStep = 512,
                    ForceStepToMin = true,

                    LabelsPaint = new SolidColorPaint(SKColors.Lime),
                    TextSize = 10,
                    Labeler = (value) =>
                    {
                        if (value < 1024) return $"{value:N2} Mb";
                        value /= 1024;
                        return $"{value:N2} Gb";
                    },

                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 2,
                        PathEffect = new DashEffect([3, 3])
                    }
                }
            ],

            // out of livecharts properties...
            Location = new Point(0, 0),
            Size = new Size(50, 50),
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom
        };

        Controls.Add(_cartesianChart);
    }
}
