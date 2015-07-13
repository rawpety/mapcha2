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
		public int ShownPlayId { get { return Arguments.GetInt("current_play_id", 0); } }
		List<Room> ListItems = new List<Room>();
		public static RoomChatFragment NewInstance(int playId)
		{
			var detailsFrag = new RoomChatFragment { Arguments = new Bundle() };
			detailsFrag.Arguments.PutInt("current_play_id", playId);
			return detailsFrag;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			String[] titles = new String[]{};
			String[] subtitles = new String[]{};;

			switch (ShownPlayId) {
			case 0:
				titles = new String[] { "Está todo super barato!!! :D", "Siiiiiiiii!!! Me compré una polera super kawaiii", "Dejé un polerón escondido, vuelvo a la tarde. QUE NADIE ME LO ROBEEEEEE!!!!!!!", "Alguien que me preste su tarjeta cencosud, le pago altiro :(", "Yo te la presto, nos juntamos en la parte de lencería... 1313", "Que cool esta aplicación c:", "40%!!! ESTAMOS TODOS LOCOOOOOOSSSSSSS O.O", "Salí recién, está LLENO!!", "Está lleno, pero me atendieron súper rápido", "Siiii igual andan motivados los cajeros" };
				subtitles = new String[] { "Está todo super barato!!! :D", "Siiiiiiiii!!! Me compré una polera super kawaiii", "Dejé un polerón escondido, vuelvo a la tarde. QUE NADIE ME LO ROBEEEEEE!!!!!!!", "Alguien que me preste su tarjeta cencosud, le pago altiro :(", "Yo te la presto, nos juntamos en la parte de lencería... 1313", "Que cool esta aplicación c:", "40%!!! ESTAMOS TODOS LOCOOOOOOSSSSSSS O.O", "Salí recién, está LLENO!!", "Está lleno, pero me atendieron súper rápido", "Siiii igual andan motivados los cajeros" };
				break;
			case 1:
				titles = new String[] { "Hay una protesta en el Jumbo... :s", "Qué onda? qué piden?", "Aumento en los sueldos y reducción en las horas de trabajo", "Jajajjaja, alguien vió al gordito que se cayó??", "Lo tengo Grabado perro!", "Que lata que los trabajadores tengan que llegar a esto para que los escuchen :/ Tienen todo mi apoyo chiquillos!!!", "Paro? QUE CLASE MEDIA", "Están en paro, pero igual se puede comprar...", "Hay sólo una caja funcionando... imagínate la cola D:", "Quien me compra unas galletas? :(" };
				subtitles = new String[] { "Hay una protesta en el Jumbo... :s", "Qué onda? qué piden?", "Aumento en los sueldos y reducción en las horas de trabajo", "Jajajjaja, alguien vió al gordito que se cayó??", "Lo tengo Grabado perro!", "Que lata que los trabajadores tengan que llegar a esto para que los escuchen :/ Tienen todo mi apoyo chiquillos!!!", "Paro? QUE CLASE MEDIA", "Están en paro, pero igual se puede comprar...", "Hay sólo una caja funcionando... imagínate la cola D:", "Quien me compra unas galletas? :(" };
				break;
			case 2:
				titles = new String[] { "Los estamos esperando, vengan a matricularse chiquillos (:", "Qué onda? qué piden?", "PSU Rendida solamente c:", "Hay muchas colas?", "Más o menos... pero avanzan rapidito", "Estará soltera la promotora?", "Ojalá xD", "jajajajjajaa", "Vengan a saludarme chiquillos!! Yo les ayudo con sus matrículas", "Qué carreras tienen?", "Construcción Civil, Ingeniería de ejecución en informática, Ingeniería de ejecución en administración de empresas, Ingeniería en prevención de Riesgos, Auditoría, Psicopedagogía y muchas otras carreras técnicas", "Te invitamos a visitar http://www.aiep.cl/sedes/sedes-curico.aspx", ":o! Muchas Gracias!!" };
				subtitles = new String[] { "Los estamos esperando, vengan a matricularse chiquillos (:", "Qué onda? qué piden?", "PSU Rendida solamente c:", "Hay muchas colas?", "Más o menos... pero avanzan rapidito", "Estará soltera la promotora?", "Ojalá xD", "jajajajjajaa", "Vengan a saludarme chiquillos!! Yo les ayudo con sus matrículas", "Qué carreras tienen?", "Construcción Civil, Ingeniería de ejecución en informática, Ingeniería de ejecución en administración de empresas, Ingeniería en prevención de Riesgos, Auditoría, Psicopedagogía y muchas otras carreras técnicas", "Te invitamos a visitar http://www.aiep.cl/sedes/sedes-curico.aspx", ":o! Muchas Gracias!!" };
				break;
			case 3:
				titles = new String[] { "Estamos en el estacionamiento de Mall Curicó, presentando el nuevo cine 8D!", "8D?", "Sí!! 8D!!", "wtf", "8 Dimensiones :)", "._________________.", "Mejor voy al teatro xP", "Igual me llama la atención... alguien ha ido? qué tal?", "Yapo chiquillos, vengan :( Es súper bacán" };
				subtitles = new String[] { "Estamos en el estacionamiento de Mall Curicó, presentando el nuevo cine 8D!", "8D?", "Sí!! 8D!!", "wtf", "8 Dimensiones :)", "", "Mejor voy al teatro xP", "Igual me llama la atención... alguien ha ido? qué tal?", "Yapo chiquillos, vengan :( Es súper bacán" };
				break;
			case 4:
				break;
			default:
				break;
			}	

			int i = 0;
			foreach (var title in titles)
			{
				ListItems.Add(new Room() { Title = title, CommentsCount = subtitles[i++], ImageResourceId = Resource.Drawable.Icon });
			}

			ListAdapter = new ChatListAdapter(Activity, ListItems);
		}


	}
}