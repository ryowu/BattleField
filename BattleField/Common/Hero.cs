using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Hero : Army
	{
		private string heroName;

		public string HeroName
		{
			get { return heroName; }
			set { heroName = value; }
		}
	}
}
