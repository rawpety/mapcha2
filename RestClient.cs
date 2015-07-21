using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using Android.Locations;

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

		public ChatRoomData getMessagesByRoom(int RoomId){
			var request = new RestRequest("mapcha/Messages/by_room.php", Method.GET);

			Location loc = new Location ("loc");

			loc.Latitude = MainActivity.lastKnownLocation.Latitude;
			loc.Longitude = MainActivity.lastKnownLocation.Longitude;

			if (MainActivity._currentLocation != null) {
				loc.Latitude = MainActivity._currentLocation.Latitude;
				loc.Longitude = MainActivity._currentLocation.Longitude;
			}

			request.AddParameter ("room", RoomId);
			request.AddParameter ("latitude", loc.Latitude);
			request.AddParameter ("longitude", loc.Longitude);


			var response = client.Execute (request);

			RestSharp.Deserializers.JsonDeserializer deserial= new JsonDeserializer();
			List<Message> Messages = new List<Message> ();
			ChatRoomData crd = new ChatRoomData ();

			crd = deserial.Deserialize<ChatRoomData> (response);
				
			return crd;
		}

		public void newRoom(string Title, string Latitude, string Longitude, string AuthorId){
			var request = new RestRequest("mapcha/Room/new.php", Method.POST);
			request.AddParameter ("title", Title);
			request.AddParameter ("latitude", Latitude);
			request.AddParameter ("longitude", Longitude);
			request.AddParameter ("author_id", AuthorId);

			var response = client.Execute (request);
		}
	}
}