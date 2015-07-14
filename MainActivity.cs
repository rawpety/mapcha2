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
using ActionBar =  Android.Support.V7.App.ActionBar;
using Android.Text;
using com.refractored.fab;
using Android.Support.V7.Widget;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using System.Collections.Generic;

namespace FabSample
{
	[Activity(Label = "Mapcha", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : ActionBarActivity, ActionBar.ITabListener
	{
		public static readonly int InstallGooglePlayServicesId = 1000;
		public static readonly string Tag = "MapchaDemo";

		private bool _isGooglePlayServicesInstalled;


	    protected override void OnCreate(Bundle bundle)
	    {
	      	base.OnCreate(bundle);
			InitActionBar();
			_isGooglePlayServicesInstalled = TestIfGooglePlayServicesIsInstalled();

			if (_isGooglePlayServicesInstalled)
			{
				//SetContentView(Resource.Layout.Main);
				//InitMapFragment();
				//SetupMapIfNeeded();

			}
			else
			{
				Log.Error("MainActivity", "Google Play Services is not installed");
			}

	      
	    }

		private bool TestIfGooglePlayServicesIsInstalled()
		{
			int queryResult = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
			if (queryResult == ConnectionResult.Success)
			{
				Log.Info(Tag, "Google Play Services is installed on this device.");
				return true;
			}

			if (GooglePlayServicesUtil.IsUserRecoverableError(queryResult))
			{
				string errorString = GooglePlayServicesUtil.GetErrorString(queryResult);
				Log.Error(Tag, "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);
				Dialog errorDialog = GooglePlayServicesUtil.GetErrorDialog(queryResult, this, InstallGooglePlayServicesId);
				ErrorDialogFragment dialogFrag = new ErrorDialogFragment(errorDialog);

				dialogFrag.Show(FragmentManager, "GooglePlayServicesDialog");
			}
			return false;
		}

	    private void InitActionBar()
	    {
			if (SupportActionBar == null)
				return;

			var actionBar = SupportActionBar;
			actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

			var tab1 = actionBar.NewTab();
			tab1.SetTabListener(this);
			tab1.SetText("Salas cercanas");
			actionBar.AddTab(tab1);

			var tab2 = actionBar.NewTab();
			tab2.SetTabListener(this);
			tab2.SetText("Mapa");
			actionBar.AddTab(tab2);
	    }

	    public void OnTabReselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
	    {
	    }

	    public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
	    {
	      switch (tab.Text)
	      {
	        case "Salas cercanas":
	          ft.Replace(Android.Resource.Id.Content, new ListViewFragment());
	          break;
	        case "Mapa":
	          ft.Replace(Android.Resource.Id.Content, new MapViewFragment());
	          break;
	      }
	    }

	    public void OnTabUnselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
	    {
	      
	    }

	    public override bool OnCreateOptionsMenu(IMenu menu)
	    {
	      MenuInflater.Inflate(Resource.Menu.main, menu);
	      return base.OnCreateOptionsMenu(menu);
	    }

	    public override bool OnOptionsItemSelected(IMenuItem item)
	    {
	      if(item.ItemId == Resource.Id.about)
	      {
	        var text = (TextView)LayoutInflater.Inflate(Resource.Layout.about_view, null);
	        text.TextFormatted = (Html.FromHtml(GetString(Resource.String.about_body)));
	        new AlertDialog.Builder(this)
	        .SetTitle(Resource.String.about)
	        .SetView(text)
	        .SetInverseBackgroundForced(true)
	        .SetPositiveButton(Android.Resource.String.Ok, (sender, args) =>
	        {
	          ((IDialogInterface)sender).Dismiss();
	        }).Create().Show();
	      }
	      return base.OnOptionsItemSelected(item);
	    }
	  }

	  public class ListViewFragment : Android.Support.V4.App.ListFragment, IScrollDirectorListener, AbsListView.IOnScrollListener
	  {
		private List<Room> rooms;


	    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
	    {
	      var root = inflater.Inflate(Resource.Layout.fragment_listview, container, false);

	      var list = root.FindViewById<ListView>(Android.Resource.Id.List);
	      
			rooms = RestClient.Instance().getAllRooms();

			var ListAdapter = new RoomListAdapter(Activity, rooms);

			list.Adapter = ListAdapter;

	      var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
	      fab.AttachToListView(list, this, this);
	      fab.Click += (sender, args) =>
	        {
	          	Toast.MakeText(Activity, "FAB Clicked!", ToastLength.Short).Show();
				var intent = new Intent();

				intent.SetClass(Activity, typeof(NewRoomActivity));
				StartActivity(intent);
	        };
	      return root;
	    }

	    public void OnScrollDown()
	    {
	      Console.WriteLine("ListViewFragment: OnScrollDown");
	    }

	    public void OnScrollUp()
	    {
	      Console.WriteLine("ListViewFragment: OnScrollUp");
	    }

		public override void OnListItemClick(ListView l, View v, int position, long id)
		{
			ShowDetails(position);
		}

	    public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
	    {
	      Console.WriteLine("ListViewFragment: OnScroll");
	    }

	    public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
	    {
	      Console.WriteLine("ListViewFragment: OnScrollChanged");
	    }

		private void ShowDetails(int position)
		{
			var intent = new Intent();

			intent.SetClass(Activity, typeof(RoomChatActivity));
			intent.PutExtra ("RoomId", rooms[position].Id);
			intent.PutExtra ("RoomName", rooms[position].Title);
			StartActivity(intent);
		}
	  }

	  public class MapViewFragment : Android.Support.V4.App.Fragment, IScrollDirectorListener
	  {
			public static readonly LatLng inMall = new LatLng(-34.9919, -71.2445);
			public string _gotoMallMarkerId;
			public GoogleMap _map;
			public SupportMapFragment _mapFragment;
			public Marker _mallMarker;

		    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		    {
			
				var root = inflater.Inflate(Resource.Layout.fragment_mapview, container, false);
				
				
				_mapFragment = FragmentManager.FindFragmentByTag("map") as SupportMapFragment;
				
				
					GoogleMapOptions mapOptions = new GoogleMapOptions ()
							.InvokeMapType (GoogleMap.MapTypeSatellite)
							.InvokeZoomControlsEnabled (true)
							.InvokeCompassEnabled (true);

					Android.Support.V4.App.FragmentTransaction fragTx = FragmentManager.BeginTransaction ();
					_mapFragment = SupportMapFragment.NewInstance (mapOptions);
					fragTx.Add (Resource.Id.mapWithOverlay, _mapFragment, "map");
					fragTx.Commit ();
				
				//SetupMapIfNeeded();

				return root;
	    	}


		    public void OnScrollDown()
		    {
		      	Console.WriteLine("RecyclerViewFragment: OnScrollDown");
		    }

		    public void OnScrollUp()
		    {
		      Console.WriteLine("RecyclerViewFragment: OnScrollUp");
		    }
	  }
}

