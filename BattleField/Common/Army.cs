using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Army
    {
		private BattleSide side;

		public BattleSide Side
		{
			get { return side; }
			set { side = value; }
		}

		private ArmType type;

		public ArmType Type
		{
			get { return type; }
			set { type = value; }
		}

		private int hp = 0;

		public int Hp
		{
			get { return hp; }
			set
			{
				hp = Utility.GetRangedArmHP(value);
			}
		}



		private Point position;

		public Point Position
		{
			get { return position; }
			set { position = value; }
		}

		private Hero target;

		public Hero Target
		{
			get { return target; }
			set { target = value; }
		}

		private ActionType action = ActionType.StandBy;

		public ActionType Action
		{
			get { return action; }
			set { action = value; }
		}
    }
}
