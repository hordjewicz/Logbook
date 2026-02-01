using LogBook.Model;
using Syncfusion.Maui.DataGrid;

namespace LogBook.View
{
    public partial class LogView : ContentPage
    {
        public LogView()
        {
            InitializeComponent();
        }

        private async void dataGrid_CellTapped(object sender, DataGridCellTappedEventArgs e)
        {

            if (e.RowData is Flight selectedFlight)
            {
                var currentApplication = Application.Current;
                var mainPage = currentApplication?.Windows.FirstOrDefault()?.Page;

                if (mainPage?.Navigation != null)
                {
                    await mainPage.Navigation.PushAsync(new FlightDetails(selectedFlight));
                }
            }
        }
    }
}