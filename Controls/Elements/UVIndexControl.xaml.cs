namespace Weather.Controls.Elements;

public partial class UVIndexControl : UserControl
{
    public UVIndexControl()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty UVIndexProperty =
        DependencyProperty.Register(
            nameof(UVIndex),
            typeof(float),
            typeof(UVIndexControl),
            new PropertyMetadata(0f));

    public float UVIndex
    {
        get => (float)GetValue(UVIndexProperty);
        set => SetValue(UVIndexProperty, value);
    }
}
