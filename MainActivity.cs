using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Android.Util;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Support.V7.App;
using ActionBar = Android.Support.V7.App.ActionBar;
using Android.Text;
using com.refractored.fab;
using Android.Support.V7.Widget;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using System.Collections.Generic;
using Java.Lang;
using System.Collections.Concurrent;
using Android.Locations;

namespace FabSample
{
	[Activity (Label = "Mapcha", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class MainActivity : ActionBarActivity, ActionBar.ITabListener, ILocationListener
	{
		public static readonly int InstallGooglePlayServicesId = 1000;
		public static readonly string Tag = "MapchaDemo";
		public static LocationManager locMgr;
		public static string android_id;
		public static Location _currentLocation = new Location("provider");

		public static Location lastKnownLocation = new Location("provider");


		private bool _isGooglePlayServicesInstalled;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			InitActionBar ();
			_isGooglePlayServicesInstalled = TestIfGooglePlayServicesIsInstalled ();
			android_id = Android.Provider.Settings.Secure.GetString(BaseContext.ContentResolver,
				Android.Provider.Settings.Secure.AndroidId); 
			if (_isGooglePlayServicesInstalled) {
				//SetContentView(Resource.Layout.Main);
				//InitMapFragment();
				//SetupMapIfNeeded();

			} else {
				Log.Error ("MainActivity", "Google Play Services is not installed");
			}

	      
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			Log.Debug (Tag, "OnStart called");
		}

		// OnResume gets called every time the activity starts, so we'll put our RequestLocationUpdates
		// code here, so that 
		protected override void OnResume ()
		{
			base.OnResume (); 
			Log.Debug (Tag, "OnResume called");


			lastKnownLocation.Latitude = -34.998941965558;
			lastKnownLocation.Longitude = -71.2445449777383;

			// initialize location manager
			locMgr = GetSystemService (Context.LocationService) as LocationManager;

			//const string locationProvider = LocationManager.NetworkProvider;
			// Or use LocationManager.GPS_PROVIDER

			/*if (locMgr.AllProviders.Contains (locationProvider)
				&& locMgr.IsProviderEnabled (locationProvider)) {
				locMgr.RequestLocationUpdates (locationProvider, 2000, 1, this);
			} else {
				Toast.MakeText (this, "The Network Provider does not exist or is not enabled!", ToastLength.Long).Show ();
			}*/

			var locationCriteria = new Criteria();
			locationCriteria.Accuracy = Accuracy.Coarse;
			locationCriteria.PowerRequirement = Power.Medium;
			string locationProvider = locMgr.GetBestProvider(locationCriteria, true);
			Log.Debug(Tag, "Starting location updates with " + locationProvider.ToString());
			locMgr.RequestLocationUpdates (locationProvider, 2000, 1, this);

			lastKnownLocation = locMgr.GetLastKnownLocation(locationProvider);

		}

		protected override void OnPause ()
		{
			base.OnPause ();

			// stop sending location updates when the application goes into the background
			// to learn about updating location in the background, refer to the Backgrounding guide
			// http://docs.xamarin.com/guides/cross-platform/application_fundamentals/backgrounding/


			// RemoveUpdates takes a pending intent - here, we pass the current Activity
			locMgr.RemoveUpdates (this);
			Log.Debug (Tag, "Location updates paused because application is entering the background");
		}

		protected override void OnStop ()
		{
			base.OnStop ();
			Log.Debug (Tag, "OnStop called");
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			Log.Debug (Tag, "Location changed");
			_currentLocation = location;
			lastKnownLocation = location;

		}
		public void OnProviderDisabled (string provider)
		{
			Log.Debug (Tag, provider + " disabled by user");
		}
		public void OnProviderEnabled (string provider)
		{
			Log.Debug (Tag, provider + " enabled by user");
		}
		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			Log.Debug (Tag, provider + " availability has changed to " + status);
		}

		private bool TestIfGooglePlayServicesIsInstalled ()
		{
			int queryResult = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
			if (queryResult == ConnectionResult.Success) {
				Log.Info (Tag, "Google Play Services is installed on this device.");
				return true;
			}

			if (GooglePlayServicesUtil.IsUserRecoverableError (queryResult)) {
				string errorString = GooglePlayServicesUtil.GetErrorString (queryResult);
				Log.Error (Tag, "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);
				Dialog errorDialog = GooglePlayServicesUtil.GetErrorDialog (queryResult, this, InstallGooglePlayServicesId);
				ErrorDialogFragment dialogFrag = new ErrorDialogFragment (errorDialog);

				dialogFrag.Show (FragmentManager, "GooglePlayServicesDialog");
			}
			return false;
		}

		private void InitActionBar ()
		{
			if (SupportActionBar == null)
				return;

			var actionBar = SupportActionBar;
			actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

			var tab1 = actionBar.NewTab ();
			tab1.SetTabListener (this);
			tab1.SetText ("Salas cercanas");
			actionBar.AddTab (tab1);

			var tab2 = actionBar.NewTab ();
			tab2.SetTabListener (this);
			tab2.SetText ("Mapa");
			actionBar.AddTab (tab2);
		}

		public void OnTabReselected (ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
		}

		public void OnTabSelected (ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
			switch (tab.Text) {
			case "Salas cercanas":
				ft.Replace (Android.Resource.Id.Content, new ListViewFragment ());
				break;
			case "Mapa":
				ft.Replace (Android.Resource.Id.Content, new MapViewFragment ());
				break;
			}
		}

		public void OnTabUnselected (ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
		{
	      
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.main, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Resource.Id.about) {
				var text = (TextView)LayoutInflater.Inflate (Resource.Layout.about_view, null);
				text.TextFormatted = (Html.FromHtml (GetString (Resource.String.about_body)));
				new AlertDialog.Builder (this)
	        .SetTitle (Resource.String.about)
	        .SetView (text)
	        .SetInverseBackgroundForced (true)
	        .SetPositiveButton (Android.Resource.String.Ok, (sender, args) => {
					((IDialogInterface)sender).Dismiss ();
				}).Create ().Show ();
			}
			return base.OnOptionsItemSelected (item);
		}
	}

	public class ListViewFragment : Android.Support.V4.App.ListFragment, IScrollDirectorListener, AbsListView.IOnScrollListener
	{
		private List<Room> rooms;
		private View root;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			root = inflater.Inflate (Resource.Layout.fragment_listview, container, false);

			var list = root.FindViewById<ListView> (Android.Resource.Id.List);
	      
			rooms = RestClient.Instance ().getAllRooms ();

			var ListAdapter = new RoomListAdapter (Activity, rooms);

			list.Adapter = ListAdapter;

	      	var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
      		fab.AttachToListView(list, this, this);
	      	fab.Click += (sender, args) =>
	        {
				var intent = new Intent();

				intent.SetClass (Activity, typeof(NewRoomActivity));
				StartActivity (intent);
			};
			return root;
		}

		public override void OnResume ()
		{
			base.OnResume (); 

			var list = root.FindViewById<ListView> (Android.Resource.Id.List);

			rooms = RestClient.Instance ().getAllRooms ();

			var ListAdapter = new RoomListAdapter (Activity, rooms);

			list.Adapter = ListAdapter;

			ListAdapter.NotifyDataSetChanged ();
		}

		public void OnScrollDown ()
		{
			Console.WriteLine ("ListViewFragment: OnScrollDown");
		}

		public void OnScrollUp ()
		{
			Console.WriteLine ("ListViewFragment: OnScrollUp");
		}

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			ShowDetails (position);
		}

		public void OnScroll (AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
		{
			Console.WriteLine ("ListViewFragment: OnScroll");
		}

		public void OnScrollStateChanged (AbsListView view, ScrollState scrollState)
		{
			Console.WriteLine ("ListViewFragment: OnScrollChanged");
		}

		private void ShowDetails (int position)
		{
			var intent = new Intent ();

			intent.SetClass (Activity, typeof(RoomChatActivity));
			intent.PutExtra ("RoomId", rooms [position].Id);
			intent.PutExtra ("RoomName", rooms [position].Title);
			StartActivity (intent);
		}


	}

	public class MapViewFragment : Android.Support.V4.App.Fragment, IScrollDirectorListener, IOnMapReadyCallback
	{
		public static readonly LatLng Location_UTalca = new LatLng (-35.0019129, -71.2300759);
		public string _gotoMallMarkerId;
		public GoogleMap _map;
		public SupportMapFragment _mapFragment;
		public Marker _mallMarker;
		private ConcurrentDictionary <string, Room> MarkerData;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var root = inflater.Inflate (Resource.Layout.fragment_mapview, container, false);
			MarkerData = new ConcurrentDictionary <string, Room> ();
			_mapFragment = FragmentManager.FindFragmentByTag ("map") as SupportMapFragment;
			
			GoogleMapOptions mapOptions = new GoogleMapOptions ()
							.InvokeMapType (GoogleMap.MapTypeSatellite)
							.InvokeZoomControlsEnabled (true)
							.InvokeCompassEnabled (true)
							.InvokeScrollGesturesEnabled(true);
			
			Android.Support.V4.App.FragmentTransaction fragTx = FragmentManager.BeginTransaction ();
			_mapFragment = SupportMapFragment.NewInstance (mapOptions);
			_mapFragment.GetMapAsync (this);
			fragTx.Add (Resource.Id.mapWithOverlay, _mapFragment, "map");
			fragTx.Commit ();
			//SetupMapIfNeeded();

			return root;
		}

		void IOnMapReadyCallback.OnMapReady (GoogleMap googleMap)
		{
			var rooms = RestClient.Instance ().getAllRooms ();

			foreach (var item in rooms) {
				Marker m = googleMap.AddMarker(new MarkerOptions()
					.SetPosition(new LatLng(item.Latitude, item.Longitude))
					.SetTitle(item.Title)
					.SetSnippet(item.CommentsCount)
				);
				MarkerData[m.Id] = item;
			}

			googleMap.InfoWindowClick += MapOnInfoWindowClick;
			//Marker mylocation = googleMap.AddMarker (new MarkerOptions ()
			//	.SetPosition (Location_UTalca)
			//	.SetTitle ("Yo")
			//	.InvokeIcon (BitmapDescriptorFactory.DefaultMarker (BitmapDescriptorFactory.HueAzure))
			//);
			//mylocation.ShowInfoWindow ();
			//MarkerData[mylocation.Id] = null;
			CameraUpdate update = CameraUpdateFactory.NewLatLngZoom (new LatLng(MainActivity.lastKnownLocation.Latitude, MainActivity.lastKnownLocation.Longitude), 17);

			if (MainActivity._currentLocation != null) {
				LatLng current = new LatLng(MainActivity._currentLocation.Latitude, MainActivity._currentLocation.Longitude);
				update = CameraUpdateFactory.NewLatLngZoom (current, 17);
			}

			googleMap.MoveCamera(update);
		}

		private void MapOnInfoWindowClick (object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			//Console.WriteLine (MarkerData[e.Marker]);
			Room r = MarkerData[e.Marker.Id];
			if (r != null) {
				var intent = new Intent ();

				intent.SetClass (Activity, typeof(RoomChatActivity));
				intent.PutExtra ("RoomId", r.Id);
				intent.PutExtra ("RoomName", r.Title);
				StartActivity (intent);
			} else {
				var intent = new Intent ();
				intent.SetClass (Activity, typeof(NewRoomActivity));
				StartActivity (intent);
			}
		}

		public void OnScrollDown ()
		{
			Console.WriteLine ("RecyclerViewFragment: OnScrollDown");
		}

		public void OnScrollUp ()
		{
			Console.WriteLine ("RecyclerViewFragment: OnScrollUp");
		}

	}

}

