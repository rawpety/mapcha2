
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
	[Activity(Label = "RoomChatActivity")]
	public class RoomChatActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			int RoomId = Intent.Extras.GetInt("RoomId");
			String title = Intent.Extras.GetString("RoomName");
			Title = title;
			var details = RoomChatFragment.NewInstance(RoomId); // Details
			var fragmentTransaction = FragmentManager.BeginTransaction();
			fragmentTransaction.Add(Android.Resource.Id.Content, details);
			fragmentTransaction.Commit();
		}
	}
}