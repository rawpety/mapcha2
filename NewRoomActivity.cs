
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
	public class NewRoomActivity : ActionBarActivity
	{
		string Tag = "NewRoomActivity";

		TextView latitude;
		TextView longitude;
		TextView location;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Log.Debug (Tag, "OnCreate called");

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.NewRoom);
			latitude = FindViewById<TextView> (Resource.Id.latitude);
			longitude = FindViewById<TextView> (Resource.Id.longitude);
			location = FindViewById<TextView> (Resource.Id.location);

			InitActionBar ();

			Location loc = new Location ("loc");

			loc.Latitude = MainActivity.lastKnownLocation.Latitude;
			loc.Longitude = MainActivity.lastKnownLocation.Longitude;

			if (MainActivity._currentLocation != null) {
				loc.Latitude = MainActivity._currentLocation.Latitude;
				loc.Longitude = MainActivity._currentLocation.Longitude;
			}

			latitude.Text = loc.Latitude.ToString();
			longitude.Text = loc.Longitude.ToString();

			setGeoCoder (loc, location);
		}

		async void setGeoCoder(Location loc, TextView location)
		{

			Geocoder geocoder = new Geocoder(this);
			IList<Address> addressList = await geocoder.GetFromLocationAsync(loc.Latitude, loc.Longitude, 10);

			Address address = addressList.FirstOrDefault();
			if (address != null)
			{
				StringBuilder deviceAddress = new StringBuilder();
				for (int i = 0; i < address.MaxAddressLineIndex; i++)
				{
					deviceAddress.Append(address.GetAddressLine(i))
						.AppendLine(",");
				}
				location.Text = deviceAddress.ToString();
			}
			else
			{
				location.Text = "No se puede determinar la ubicación.";
			}
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
				RestClient.Instance ().newRoom (t.Text, MainActivity._currentLocation.Latitude.ToString(), MainActivity._currentLocation.Longitude.ToString(), "dqw12d81d2");
				Toast.MakeText(this.BaseContext, "Sala Creada", ToastLength.Short).Show();
				this.Finish ();
			}
			if(item.ItemId == Resource.Id.home)
			{
				Toast.MakeText(this.BaseContext, "jom", ToastLength.Short).Show();
				this.Finish ();
			}
			return base.OnOptionsItemSelected(item);
		}
	}
}

