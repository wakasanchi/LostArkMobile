using System;
using Xamarin.Forms;

namespace LostArkMobile.Models
{
	public class Event
	{
		public ImageSource ImageSource { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public DateTime Time { get; set; }
		public string TimeString { get; set; }

		public Event()
		{
		}
	}
}
