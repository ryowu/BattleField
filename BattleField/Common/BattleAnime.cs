using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class BattleAnime
	{
		private Army currentArmy;

		public Army CurrentArmy
		{
			get { return currentArmy; }
			set { currentArmy = value; }
		}

		private AnimeType animeType;

		public AnimeType AnimeType
		{
			get { return animeType; }
			set { animeType = value; }
		}

		private Point fromPoint;

		public Point FromPoint
		{
			get { return fromPoint; }
			set { fromPoint = value; }
		}

		private Point toPoint;

		public Point ToPoint
		{
			get { return toPoint; }
			set { toPoint = value; }
		}
	}
}
