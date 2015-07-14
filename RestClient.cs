using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;

namespace FabSample
{
	public class RestClient
	{
		private static RestClient self;
		private RestSharp.RestClient client;

		private RestClient(){
			this.client = new RestSharp.RestClient("http://181.72.174.29/");
		}

		public static RestClient Instance(){
			if (self != null) {
				return self;
			} else {
				self = new RestClient ();
				return self;
			}
		}

		public List<Room> getAllRooms(){
			var request = new RestRequest ("mapcha/Room/all.php", Method.GET);

			var response = client.Execute (request);

			RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer ();
			List<Room> rooms = new List<Room> ();

			if (response.Content.Length != 6) {
				rooms = deserial.Deserialize<List<Room>> (response);
			}
			return rooms;
		}

		public List<Message> getMessagesByRoom(int RoomId){
			var request = new RestRequest("mapcha/Messages/by_room.php", Method.GET);
			request.AddParameter ("room", RoomId);

			var response = client.Execute (request);

			RestSharp.Deserializers.JsonDeserializer deserial= new JsonDeserializer();
			List<Message> Messages = new List<Message> ();

			if (response.Content.Length != 6) {
				Messages = deserial.Deserialize<List<Message>> (response);
			}
				
			return Messages;
		}
	}
}