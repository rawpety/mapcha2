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
	public class Room
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTime	CreationDate { get; set; }
		private String comments;
		public string CommentsCount { 
			get{ 
				return comments;
			}
			set{
				switch (value) {
				case("0"):
					comments = "0 Comentarios";
					break;
				case("1"):
					comments = "1 Comentario";
					break;
				default:
					comments = value + " Comentarios";
					break;
				}
			}
		}

		public int ImageResourceId { get; set; }

		public override string ToString() 
		{
			string s = "ID: " + Id + "\n";
			s += "Title: " + Title + "\n";
			s += "Latitude: " + Latitude + "\n";
			s += "Longitude: " + Longitude + "\n";
			s += "CreationDate: " + CreationDate + "\n";
			return s;
		}
	}
}