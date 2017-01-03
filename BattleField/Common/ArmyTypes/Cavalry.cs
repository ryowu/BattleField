using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ArmyTypes
{
	public class Cavalry : Army
	{
		public Cavalry()
		{
			this.type = ArmType.Cavalry;
			this.atk = 22;
			this.def = 7;
			this.atkAlter = 5;
			this.action = ActionType.StandBy;
		}
	}
}
