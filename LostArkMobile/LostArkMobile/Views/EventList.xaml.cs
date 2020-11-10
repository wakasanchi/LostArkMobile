using LostArkMobile.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace LostArkMobile.Views
{
    public partial class EventList
    {
        public EventList()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            MyListView.ScrollTo((MyListView.ItemsSource as ObservableCollection<Event>)[0], ScrollToPosition.Start, true);
        }
    }
}
