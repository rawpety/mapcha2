using System;
using System.Collections.Generic;

namespace FabSample
{
	public class ChatRoomData
	{
		private List<Message> messages;
		public Room Room { get; set; }
		public Boolean Available { get; set; }
		public List<Message> Messages {
			get {
				if (messages == null) {
					messages = new List<Message> ();
				}
				return messages;
			}
			set {
				if (value [0].Id != 0) {
					this.messages = value;
				}
			}
		}
	}
}

