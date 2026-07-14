namespace Weather.Controls.StickyControl;

[SupportedOSPlatform("windows")]
public class StickyBehavior : Behavior<ScrollViewer>
{
    public static readonly DependencyProperty IsStickyProperty =
       DependencyProperty.Register(
           nameof(IsSticky), 
           typeof(bool), 
           typeof(StickyBehavior),
           new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public bool IsSticky
    {
        get => (bool)GetValue(IsStickyProperty);
        set => SetValue(IsStickyProperty, value);
    }

    public static readonly DependencyProperty TargetElementProperty =
    DependencyProperty.Register(
        nameof(TargetElement), 
        typeof(FrameworkElement), 
        typeof(StickyBehavior),
        new PropertyMetadata(null));

    public FrameworkElement TargetElement
    {
        get => (FrameworkElement)GetValue(TargetElementProperty);
        set => SetValue(TargetElementProperty, value);
    }

    private TranslateTransform? _translateTransform;
    private double _elementPositionInContent;
    private bool _isPositionCalculated;
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.ScrollChanged += OnScrollChanged;
        AssociatedObject.Loaded += OnLoaded;
        _translateTransform = new TranslateTransform();
        TargetElement?.LayoutUpdated += OnLayoutUpdated;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.ScrollChanged -= OnScrollChanged;
        AssociatedObject.Loaded -= OnLoaded;
        TargetElement?.LayoutUpdated -= OnLayoutUpdated;
        base.OnDetaching();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        CalculateElementPosition();
        UpdateSticky(AssociatedObject.VerticalOffset);
    }

    private void OnLayoutUpdated(object? sender, EventArgs e)
    {
        CalculateElementPosition();
        if (AssociatedObject != null)
            UpdateSticky(AssociatedObject.VerticalOffset);
    }
    private void CalculateElementPosition()
    {
        if (TargetElement == null || AssociatedObject == null) return;

        try
        {
            var transform = TargetElement.TransformToAncestor(AssociatedObject);
            var point = transform.Transform(new Point(0, 0));
            _elementPositionInContent = point.Y;
            _isPositionCalculated = true;
        }
        catch
        {
            _isPositionCalculated = false;
        }
    }

    private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        UpdateSticky(e.VerticalOffset);
    }

    private void UpdateSticky(double verticalOffset)
    {
        if (!_isPositionCalculated || TargetElement == null || AssociatedObject == null)
            return;

        double elementTopRelativeToViewport = _elementPositionInContent - verticalOffset;
        bool shouldBeSticky = elementTopRelativeToViewport <= 0;

        if (shouldBeSticky && !IsSticky)
        {
            IsSticky = true;
            if (TargetElement.RenderTransform != _translateTransform)
                TargetElement.RenderTransform = _translateTransform;
            _translateTransform!.Y = verticalOffset - _elementPositionInContent;
        }
        else if (!shouldBeSticky && IsSticky)
        {
            IsSticky = false;
            TargetElement.RenderTransform = null;
        }
        else if (IsSticky)
        {
            _translateTransform!.Y = verticalOffset - _elementPositionInContent;
        }
    }
}
