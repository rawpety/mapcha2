using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace FabSample
{
	public class RoomChatFragment : ListFragment
	{
		public int RoomId { get { return Arguments.GetInt("RoomId", 0); } }
		List<Message> Messages = new List<Message>();
		public static RoomChatFragment NewInstance(int RoomId)
		{
			var detailsFrag = new RoomChatFragment { Arguments = new Bundle() };
			detailsFrag.Arguments.PutInt("RoomId", RoomId);
			return detailsFrag;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			Messages = RestClient.Instance().getMessagesByRoom (RoomId);


			ListAdapter = new ChatListAdapter(Activity, Messages);
		}


	}
}