
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
using Android.Util;
using ActionBar =  Android.Support.V7.App.ActionBar;

using Android.Support.V7.App;
using Android.Locations;

namespace FabSample
{
	[Activity (Label = "NewRoomActivity")]			
	public class NewRoomActivity : ActionBarActivity, ILocationListener
	{
		LocationManager locMgr;
		string tag = "MainActivity";
		Button button;
		TextView latitude;
		TextView longitude;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Log.Debug (tag, "OnCreate called");

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.NewRoom);
			latitude = FindViewById<TextView> (Resource.Id.latitude);
			longitude = FindViewById<TextView> (Resource.Id.longitude);

			InitActionBar ();

		}

		protected override void OnStart ()
		{
			base.OnStart ();
			Log.Debug (tag, "OnStart called");
		}

		// OnResume gets called every time the activity starts, so we'll put our RequestLocationUpdates
		// code here, so that 
		protected override void OnResume ()
		{
			base.OnResume (); 
			Log.Debug (tag, "OnResume called");

			// initialize location manager
			locMgr = GetSystemService (Context.LocationService) as LocationManager;

			if (locMgr.AllProviders.Contains (LocationManager.NetworkProvider)
				&& locMgr.IsProviderEnabled (LocationManager.NetworkProvider)) {
				locMgr.RequestLocationUpdates (LocationManager.NetworkProvider, 2000, 1, this);
			} else {
				Toast.MakeText (this, "The Network Provider does not exist or is not enabled!", ToastLength.Long).Show ();
			}

		}

		protected override void OnPause ()
		{
			base.OnPause ();

			// stop sending location updates when the application goes into the background
			// to learn about updating location in the background, refer to the Backgrounding guide
			// http://docs.xamarin.com/guides/cross-platform/application_fundamentals/backgrounding/


			// RemoveUpdates takes a pending intent - here, we pass the current Activity
			locMgr.RemoveUpdates (this);
			Log.Debug (tag, "Location updates paused because application is entering the background");
		}

		protected override void OnStop ()
		{
			base.OnStop ();
			Log.Debug (tag, "OnStop called");
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			Log.Debug (tag, "Location changed");
			latitude.Text = "Latitude: " + location.Latitude.ToString();
			longitude.Text = "Longitude: " + location.Longitude.ToString();
		}
		public void OnProviderDisabled (string provider)
		{
			Log.Debug (tag, provider + " disabled by user");
		}
		public void OnProviderEnabled (string provider)
		{
			Log.Debug (tag, provider + " enabled by user");
		}
		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			Log.Debug (tag, provider + " availability has changed to " + status.ToString());
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

			SetTitle (Resource.String.new_room);
			actionBar.SetDisplayUseLogoEnabled(true);
			actionBar.SetDisplayHomeAsUpEnabled(true); 
			actionBar.SetIcon(Resource.Drawable.ic_launcher);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.action_new_room)
			{
				EditText t = (EditText)FindViewById(Resource.Id.editText1);
				Toast.MakeText(this.BaseContext, t.Text, ToastLength.Short).Show();
			}
			if(item.ItemId == Resource.Id.home)
			{
				Toast.MakeText(this.BaseContext, "jom", ToastLength.Short).Show();
			}
			return base.OnOptionsItemSelected(item);
		}
	}
}

