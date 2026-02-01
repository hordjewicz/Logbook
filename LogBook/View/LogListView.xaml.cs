using LogBook.Model;
using LogBook.ViewModel;
using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.ListView;

namespace LogBook.View;

public partial class LogListView : ContentPage
{
	public LogListView()
	{
		InitializeComponent();
        ListView.AllowSwiping = true;
        ListView.SelectionMode = Syncfusion.Maui.ListView.SelectionMode.None;
        ListView.SwipeOffset = 360;
        ListView.SwipeThreshold = 30;

        ListView.EndSwipeTemplate = new DataTemplate(() =>
        {
            var grid = new Grid();

            var grid1 = new Grid()
            {
                BackgroundColor = Colors.AliceBlue,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            var deleteGrid = new Grid() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            var deleteImage = new Image() { BackgroundColor = Colors.Transparent, HeightRequest = 35, WidthRequest = 35 };
            deleteImage.Source = ImageSource.FromResource("Swiping.Images.Delete.png");
            deleteGrid.Children.Add(deleteImage);
            grid1.Children.Add(deleteGrid);

            grid.Children.Add(grid1);

            return grid;
        });
    }

    private async void ListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {   
        if (e.DataItem is Flight selectedFlight)
        {
            var currentApplication = Application.Current;
            var mainPage = currentApplication?.Windows.FirstOrDefault()?.Page;

            if (mainPage?.Navigation != null)
            {
                await mainPage.Navigation.PushAsync(new FlightDetails(selectedFlight));
            }
        }
    }

    private void ListView_ItemDoubleTapped(object sender, ItemDoubleTappedEventArgs e)
    {
        //if (e.DataItem == )
        //    viewModel.InboxInfo.Remove(e.DataItem as ListViewInboxInfo);
    }

    private void ListView_SwipeEnded(object sender, Syncfusion.Maui.ListView.SwipeEndedEventArgs e)
    {
        if (e.Offset >= 360)
        {         
            //ListView.ResetSwipeItem();
        }
    }
}