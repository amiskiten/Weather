namespace Weather.Controls.Elements;
public partial class MainElement : UserControl
{
    public MainElement()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty TitleTextProperty =
        DependencyProperty.Register(
            nameof(TitleText),
            typeof(string),
            typeof(MainElement),
            new PropertyMetadata(""));

    public string TitleText
    {
        get => (string)GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }

    public static readonly DependencyProperty MainTextProperty =
    DependencyProperty.Register(
        nameof(MainText),
        typeof(string),
        typeof(MainElement),
        new PropertyMetadata(""));

    public string MainText
    {
        get => (string)GetValue(MainTextProperty);
        set => SetValue(MainTextProperty, value);
    }
}
