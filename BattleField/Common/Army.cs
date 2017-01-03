using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		protected ArmType type;

		public ArmType Type
		{
			get { return type; }
			set { type = value; }
		}

		protected int hp = 100;

		public int Hp
		{
			get { return hp; }
			set
			{
				hp = Utility.GetRangedArmHP(value);
			}
		}

		protected int atk;

		public int Atk
		{
			get { return atk; }
			set { atk = value; }
		}

		protected int def;

		public int Def
		{
			get { return def; }
			set { def = value; }
		}

		protected int atkAlter;

		public int AtkAlter
		{
			get { return atkAlter; }
			set { atkAlter = value; }
		}

		protected Point position;

		public Point Position
		{
			get { return position; }
			set { position = value; }
		}

		protected Hero target;

		public Hero Target
		{
			get { return target; }
			set { target = value; }
		}

		protected ActionType action = ActionType.StandBy;

		public ActionType Action
		{
			get { return action; }
			set { action = value; }
		}

		public void DoStandBy()
		{
 			// do nothing for now
			Debug.WriteLine(this.type.ToString() + " at '" + this.position.X.ToString() + "," + this.position.Y.ToString() + "' standby");
		}

		public void DoMoveTo(Point newPosition)
		{
			Debug.WriteLine(this.type.ToString() + " at '" + this.position.X.ToString() + "," + this.position.Y.ToString() + "' move to '" + newPosition.X.ToString() + "," + newPosition.Y.ToString() + "'");

			if (this.action != ActionType.StandBy)
				this.position = newPosition;
		}

		public void DoAttack(Army targetArmy)
		{
			int atkAnti = 0;

			if (this.type == ArmType.Infantry && targetArmy.Type == ArmType.Lancer)
			{
				atkAnti = 5;
			}
			else if (this.type == ArmType.Lancer && targetArmy.Type == ArmType.Cavalry)
			{
				atkAnti = 8;
			}
			else if (this.type == ArmType.Cavalry && targetArmy.Type == ArmType.Infantry)
			{
				atkAnti = 6;
			}
			else if (this.type == ArmType.Cavalry && targetArmy.Type == ArmType.Archer)
			{
				atkAnti = 10;
			}

			int damage = this.atk + atkAnti + Utility.RandomNum(0, this.atkAlter);
			targetArmy.Hp -= damage;

			Debug.WriteLine(this.type.ToString() + " at '" + this.position.X.ToString() + "," + this.position.Y.ToString() + "' attack '" + targetArmy.position.X.ToString() + "," + targetArmy.position.Y.ToString() + "' with damage:" + damage.ToString());
		}
    }
}
