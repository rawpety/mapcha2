
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
	[Activity (Label = "NewRoomActivity")]			
	public class NewRoomActivity : ActionBarActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			InitActionBar ();

			SetTitle (Resource.String.new_room);

			var details = new NewRoomFragment(); // Details
			var fragmentTransaction = FragmentManager.BeginTransaction();
			fragmentTransaction.Add(Android.Resource.Id.Content, details);
			fragmentTransaction.Commit();


		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.NewRoomActions, menu);

			return base.OnCreateOptionsMenu(menu);

		}

		private void InitActionBar()
		{
			if (SupportActionBar == null)
				return;

			var actionBar = SupportActionBar;
			actionBar.SetDisplayHomeAsUpEnabled(true);

		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.action_new_room)
			{
				Toast.MakeText(this.BaseContext, "Crear sala!", ToastLength.Short).Show();
			}
			return base.OnOptionsItemSelected(item);
		}
	}
}

