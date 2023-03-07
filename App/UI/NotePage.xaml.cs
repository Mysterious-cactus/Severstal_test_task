using Data.Entities;

namespace App.UI;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
	public string ItemId
	{
		set
		{
			LoadNote(value);
		}
	}
	public NotePage()
	{
		InitializeComponent();
		BindingContext = new Note();
	}

	private async void LoadNote(string value)
	{
		try
		{
			int id = int.Parse(value);
			Note note = await App.Db.GetNoteByIdAsync(id);
			BindingContext = note;
		}
		catch {

		}
	}

	private async void OnSaveClicked(object sender, EventArgs e)
	{
		Note note = (Note)BindingContext;
		note.Date = DateTimeOffset.UtcNow;
		if (!string.IsNullOrWhiteSpace(note.Text))
		{
			await App.Db.CreateUpdateNoteAsync(note);
		}
		await Shell.Current.GoToAsync("..");
	}

	private async void OnDeleteClicked(object sender, EventArgs e)
	{
        Note note = (Note)BindingContext;
		await App.Db.DeleteNoteAsync(note);
		await Shell.Current.GoToAsync("..");
    }
}