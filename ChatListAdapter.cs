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
	class ChatListAdapter : BaseAdapter<Message>
	{
		List<Message> Messages;
		Activity context;
		public ChatListAdapter(Activity context, List<Message> Messages)
			: base()
		{
			this.context = context;
			this.Messages = Messages;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Message this[int position]
		{   
			get { return Messages[position]; } 
		}
		public override int Count {
			get { return Messages.Count; } 
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = Messages[position];
			//View view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			View view = convertView;

				if (item.AuthorId.Equals (MainActivity.android_id)) {
					view = context.LayoutInflater.Inflate (Resource.Layout.ChatItem_Reversed, null);
				} else {
					view = context.LayoutInflater.Inflate (Resource.Layout.ChatItem, null);
				}


				
			view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Content;
			view.FindViewById<ImageView>(Resource.Id.Icon).SetImageResource(Resource.Drawable.Icon);

			return view;
		}
	}
}