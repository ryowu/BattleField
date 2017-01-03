using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArmyTypes
{
	public class Infantry : Army
	{
		public Infantry()
		{
			this.type = ArmType.Infantry;
			this.atk = 15;
			this.def = 5;
			this.atkAlter = 4;
			this.action = ActionType.StandBy;
		}
	}
}
