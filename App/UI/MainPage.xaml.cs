using Data.Entities;

namespace App.UI;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		collectionView.ItemsSource = await App.Db.GetNotesAsync();
	}
	private async void AddNoteClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NotePage));
	}

	private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection != null)
		{
			Note note = (Note)e.CurrentSelection.FirstOrDefault();
			await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Id}");
		}
	}
}