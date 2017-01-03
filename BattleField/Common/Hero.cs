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

		public Hero()
		{
			this.type = ArmType.Hero;
			this.atk = 20;
			this.def = 6;
			this.atkAlter = 5;
			this.action = ActionType.StandBy;
		}
	}
}
