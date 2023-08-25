namespace Krankenkassen.Popups;
using CommunityToolkit.Maui.Views;
using Krankenkassen.Models.Model;

public partial class EditFieldPop : Popup
{
	readonly Label lbl;
	readonly CsvLineModel model;
	readonly int index;
	public EditFieldPop(Label lbl , CsvLineModel model , int index)
	{
		InitializeComponent();
		this.lbl = lbl;
		this.model = model;
		this.index = index;
		entry.Placeholder = lbl.Text;
	}
	private void Save()
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			lbl.Text = entry.Text;
			model.Line[index] = lbl.Text;
		});
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Save();
		Close();
    }
}