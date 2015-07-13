
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
	[Activity (Label = "NewRoomActivity")]			
	public class NewRoomActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetTitle (Resource.String.chat_room);
			var details = new NewRoomFragment(); // Details
			var fragmentTransaction = FragmentManager.BeginTransaction();
			fragmentTransaction.Add(Android.Resource.Id.Content, details);
			fragmentTransaction.Commit();
		}
	}
}

