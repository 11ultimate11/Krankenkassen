using Krankenkassen.Models.Model;

namespace Krankenkassen.Components;

public partial class CsvLineComponent : Grid
{
    private object color;
    public CsvLineComponent(CsvLineModel data)
    {
        _ = Application.Current.Resources.TryGetValue("Primary", out color);
        InitializeComponent();
        CreateItem(data);
    }
    /// <summary>
    /// Erstellung und Rückgabe eines Rasters in Abhängigkeit von der Datenlänge
    /// </summary>
    /// <param name="data"></param>
    private void CreateItem(CsvLineModel data)
    {
        CreateGrid(data.Line.Length);
        AddChildsToGrid(data.Line);
    }
    private void CreateGrid(int columns)
    {
        var cols = new ColumnDefinitionCollection();
        var rows = new RowDefinitionCollection()
            {
                new RowDefinition() { Height = new GridLength(40 , GridUnitType.Absolute)}
            };
        ColumnDefinitions = cols;
        RowDefinitions = rows;
        for (int i = 0; i < columns + 2; i++)
        {
            ColumnDefinition columnDefinition = new ();
            if (i == 0)
            {
                columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            }
            else if (i == 1)
            {
                columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            }
            else if (i == 2)
            {
                columnDefinition.Width = new GridLength(3, GridUnitType.Star);
            }
            else
            {
                columnDefinition.Width = new GridLength(1, GridUnitType.Star);
            }
            ColumnDefinitions.Add(columnDefinition);
        }
    }
    private void AddChildsToGrid(string[] data)
    {
        SetArrrow();
        int index = 2;
        foreach (string item in data)
        {
            Label lbl = new() { Text = item , VerticalTextAlignment = TextAlignment.Center };
            Grid.SetColumn(lbl, index++);
            SetLabelChildDataTrigger(lbl);
            Children.Add(lbl);
        }
    }
    private void SetLabelChildDataTrigger(Label lbl)
    {
        Setter setter = new()
        {
            Property = Label.TextColorProperty,
            Value = Color.FromRgb(255, 255, 255)
        };
        DataTrigger trigger = new(typeof(Label))
        {
            Binding = new Binding(nameof(this.BackgroundColor), source: this),
            Value = color
        };
        trigger.Setters.Add(setter);
        lbl.Triggers.Add(trigger);
    }
    private void SetArrrow()
    {
        Image img = new()
        {
            Source = "caret_right_fill_normal.png",
            HeightRequest = 15,
            WidthRequest = 15,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            IsVisible = false
        };
        Children.Add(img);
        Image img2 = new()
        {
            Source = "text_left.png",
            HeightRequest = 15,
            WidthRequest = 15,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            IsVisible = true
        };
        Grid.SetColumn(img2, 1);
        Children.Add(img2);
        SetImagesTriggers(img, img2);
    }
    private void SetImagesTriggers(Image img , Image img2)
    {
        DataTrigger trigger = new(typeof(Image))
        {
            Binding = new Binding(nameof(this.BackgroundColor), source: this),
            Value = color
        };
        Setter setter = new()
        {
            Property = Image.IsVisibleProperty,
            Value = true
        };
        trigger.Setters.Add(setter);
        img.Triggers.Add(trigger);

        DataTrigger trigger2 = new(typeof(Image))
        {
            Binding = new Binding(nameof(this.BackgroundColor), source: this),
            Value = color
        };
        Setter setter2 = new()
        {
            Property = Image.IsVisibleProperty,
            Value = false
        };
        trigger2.Setters.Add(setter2);
        img2.Triggers.Add(trigger2);
    }
}