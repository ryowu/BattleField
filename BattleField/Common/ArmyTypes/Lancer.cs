using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArmyTypes
{
	public class Lancer : Army
	{
		public Lancer()
		{
			this.type = ArmType.Lancer;
			this.atk = 13;
			this.def = 8;
			this.atkAlter = 3;
			this.action = ActionType.StandBy;
		}
	}
}
