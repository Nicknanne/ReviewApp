namespace ReviewApp.Controls;

public partial class RatingSliderControl : ContentView
{
    public static readonly BindableProperty TitleTextProperty =
        BindableProperty.Create(nameof(TitleText), typeof(string), typeof(RatingSliderControl), "Параметр: ");
    public string TitleText
    {
        get => (string)GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }

    public static readonly BindableProperty CurrentValueProperty =
        BindableProperty.Create(nameof(CurrentValue), typeof(double), typeof(RatingSliderControl), 1.0, BindingMode.TwoWay);

    public double CurrentValue
    {
        get => (double)GetValue(CurrentValueProperty);
        set => SetValue(CurrentValueProperty, value);
    }

    public static readonly BindableProperty MinimumValueProperty =
        BindableProperty.Create(nameof(MinimumValue), typeof(double), typeof(RatingSliderControl), 1.0);

    public double MinimumValue
    {
        get => (double)GetValue(MinimumValueProperty);
        set => SetValue(MinimumValueProperty, value);
    }

    public static readonly BindableProperty MaximumValueProperty =
        BindableProperty.Create(nameof(MaximumValue), typeof(double), typeof(RatingSliderControl), 1.0);

    public double MaximumValue
    {
        get => (double)GetValue(MaximumValueProperty);
        set => SetValue(MaximumValueProperty, value);
    }

    public RatingSliderControl()
    {
        InitializeComponent();
    }
}