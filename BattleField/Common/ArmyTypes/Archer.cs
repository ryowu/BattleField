using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArmyTypes
{
	public class Archer : Army
	{
		public Archer()
		{
			this.type = ArmType.Archer;
			this.atk = 10;
			this.def = 3;
			this.atkAlter = 3;
			this.action = ActionType.StandBy;
		}
	}
}
