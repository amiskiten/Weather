namespace Weather.WeatherImages.Elements;
public partial class FogLine : UserControl
{
    public FogLine()
    {
        InitializeComponent();
    }
    public static readonly DependencyProperty LineTopMarginProperty =
        DependencyProperty.Register(
            nameof(LineTopMargin),
            typeof(double),
            typeof(FogLine),
            new PropertyMetadata((double)0));


    public double LineTopMargin
    {
        get => (double)GetValue(LineTopMarginProperty);
        set => SetValue(LineTopMarginProperty, value);
    }

    public static readonly DependencyProperty AnimationBeginTimeProperty =
        DependencyProperty.Register(
            nameof(AnimationBeginTime),
            typeof(TimeSpan),
            typeof(FogLine),
            new PropertyMetadata(TimeSpan.Zero));

    public TimeSpan AnimationBeginTime
    {
        get => (TimeSpan)GetValue(AnimationBeginTimeProperty);
        set => SetValue(AnimationBeginTimeProperty, value);
    }
}
