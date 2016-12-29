using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class ArmyAI
	{
		private List<Army> armyList = new List<Army>();
		private int currentIndex = 0;

		public ArmyAI(List<Army> armyList)
		{
			this.armyList = armyList;
		}

		public void ResetBattle()
		{
			armyList.Clear();
			currentIndex = 0;
		}

		public void DoAction()
		{
			//Get current army which needs action
			Army currentArmy = GetNextArmy();

			//Get the delta X Y for against target
			int deltaX = currentArmy.Target.Position.X.CompareTo(currentArmy.Position.X);
			int deltaY = currentArmy.Target.Position.Y.CompareTo(currentArmy.Position.Y);

			//Get first/second move target
			Army firstTargetAmry = null;
			Army secondTargetArmy = null;
			if (deltaX == 0 && deltaY != 0)
			{
				firstTargetAmry = GetArmyByPosition(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
				if (firstTargetAmry == null)
				{
					//move to first target
					currentArmy.Position = new System.Drawing.Point(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
					return;
				}
			}
			else if (deltaX != 0 && deltaY == 0)
			{
				firstTargetAmry = GetArmyByPosition(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
				if (firstTargetAmry == null)
				{
					//move to first target
					currentArmy.Position = new System.Drawing.Point(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
					return;
				}
			}
			else //deltax&y !=0
			{
				firstTargetAmry = GetArmyByPosition(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
				if (firstTargetAmry == null)
				{
					//move to first target
					currentArmy.Position = new System.Drawing.Point(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
					return;
				}
				secondTargetArmy = GetArmyByPosition(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
				if (secondTargetArmy == null)
				{
					//move to first target
					currentArmy.Position = new System.Drawing.Point(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
					return;
				}
			}

			if (firstTargetAmry != null)
			{
				//If first target is friend
				if (currentArmy.Side == firstTargetAmry.Side)
				{
					if (secondTargetArmy != null)
					{
						if (currentArmy.Side == secondTargetArmy.Side)
						{
							//standby, do nothing
						}
						else
						{
							//Attack enemy
							Attack(secondTargetArmy);
						}
					}
				}
				else
				{
					//Attack enemy
					Attack(firstTargetAmry);
				}
			}
		}

		private void Attack(Army targetArmy)
		{
			targetArmy.Hp -= 10;

			Army currentArmy = armyList[currentIndex];
			if (currentArmy.Type == ArmType.Infantry && targetArmy.Type == ArmType.Lancer)
			{

			}

		}

		private Army GetNextArmy()
		{
			Army result = null;
			while(true)
			{
				if (armyList[currentIndex].Hp > 0)
				{
					result = armyList[currentIndex];
					MoveToNextIndex();
					break;
				}
				else
				{
					MoveToNextIndex();
				}
			}
			if (result != null)
				return result;
			throw new Exception("cannot find next Army!");
		}

		private Army GetArmyByPosition(int x, int y)
		{
			return armyList.Find((a) => a.Hp > 0 && a.Position.X == x && a.Position.Y == y);
		}

		private void MoveToNextIndex()
		{
			currentIndex++;
			if (currentIndex == armyList.Count)
				currentIndex = 0;
		}
	}
}
