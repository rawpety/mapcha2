
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

using ActionBar =  Android.Support.V7.App.ActionBar;

using Android.Support.V7.App;

namespace FabSample
{
	[Activity(Label = "RoomChatActivity")]
	public class RoomChatActivity : ActionBarActivity
	{
		int RoomId;
		String title;

		protected override void OnCreate(Bundle savedInstanceState)
		{

			base.OnCreate(savedInstanceState);
			RoomId = Intent.Extras.GetInt("RoomId");
			title = Intent.Extras.GetString("RoomName");
			Title = title;


			InitActionBar (RoomId,title);

			var details = RoomChatFragment.NewInstance(RoomId); // Details
			var fragmentTransaction = FragmentManager.BeginTransaction();
			fragmentTransaction.Add(Android.Resource.Id.Content, details);
			fragmentTransaction.Commit();


		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.RoomChatActions, menu);

			ChatRoomData crd = RestClient.Instance().getMessagesByRoom (RoomId);

			menu.FindItem(Resource.Id.action_up_vote).SetEnabled(crd.Available);
			menu.FindItem(Resource.Id.action_down_vote).SetEnabled(crd.Available);

			return base.OnCreateOptionsMenu(menu);

		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{

			if(item.ItemId == Resource.Id.action_up_vote)
			{
				Toast.MakeText(this.BaseContext, "Up Vote", ToastLength.Short).Show();
			}
			else if(item.ItemId == Resource.Id.action_down_vote)
			{
				Toast.MakeText(this.BaseContext, "Down Vote", ToastLength.Short).Show();
			}
			else
			{
				this.Finish ();
			}
			return base.OnOptionsItemSelected(item);
		}

		private void InitActionBar(int id, String title)
		{
			if (SupportActionBar == null)
				return;

			var actionBar = SupportActionBar;

			Title = title;
			actionBar.SetDisplayUseLogoEnabled(true);
			actionBar.SetDisplayHomeAsUpEnabled(true); 
			actionBar.SetIcon(Resource.Drawable.ic_launcher);
		}
	}
}