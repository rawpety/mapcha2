using System;
using System.Collections.Generic;

namespace FabSample
{
	public class ChatRoomData
	{
		public Room Room { get; set; }
		public List<Message> Messages { get; set; }
		public Boolean Available { get; set; }
	}
}

