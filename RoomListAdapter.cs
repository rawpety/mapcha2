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
	class RoomListAdapter : BaseAdapter<Room>
	{
		List<Room> items;
		Activity context;
		public RoomListAdapter(Activity context, List<Room> items)
			: base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Room this[int position]
		{   
			get { return items[position]; } 
		}
		public override int Count {
			get { return items.Count; } 
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			//View view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.RoomItem, null);
			view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Title;
			view.FindViewById<TextView> (Resource.Id.Text2).Text = item.CommentsCount;
			view.FindViewById<ImageView>(Resource.Id.Icon).SetImageResource(item.ImageResourceId);

			return view;
		}
	}
}