using LogBook.Model;

namespace LogBook.View;

public partial class FlightDetails : ContentPage
{

    public FlightDetails(Flight flight)
    {
        InitializeComponent();
        BindingContext = flight;
    }

}