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
using Android.Util;

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
		private int commentsCount;
		private int image;

		private int votes;
		public int	Votes {
			get{ 
				return votes;
			}
			set{
				votes = value;
				PointsCount = value +"";
			}
		}
		private String points;
		public String PointsCount{
			get{
				return points;
			}
			set{
				switch (value) {
				case("0"):
					points = "0 Puntos";
					break;
				case("1"):
					points = "1 Punto";
					break;
				case("-1"):
					points = "-1 Punto";
					break;
				default:
					points = value + " Puntos";
					break;
				}
			}
		}

		public string CommentsCount { 
			get{ 
				return comments;
			}
			set{
				Int32.TryParse(value, out commentsCount);
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

		public int ImageResourceId { 
			get{ 
				if (this.Id == 1) {
					image = Resource.Drawable.ic_star_black_48dp;
				}
				else if (this.commentsCount > 8) {
					image = Resource.Drawable.ic_whatshot_black_48dp;
				}
				else if(CreationDate.Date == DateTime.Now.Date)
				{
					image = Resource.Drawable.ic_new_releases_black_48dp;
				}
				else {

					image = Resource.Drawable.ic_chat_black_48dp;
				}
				Log.Debug ("room", "votes: " + votes + " image: " +image.ToString());
				return image;
			}

			set{ }

		}

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