using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FabSample
{
	class ListItem
	{
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public int ImageResourceId { get; set; }
	}
}