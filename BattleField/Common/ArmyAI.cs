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

		public List<Army> ArmyList
		{
			get { return armyList; }
			set { armyList = value; }
		}

		private List<BattleAnime> currentAnime = new List<BattleAnime>();

		public List<BattleAnime> CurrentAnime
		{
			get { return currentAnime; }
		}

		private int currentIndex = 0;

		public ArmyAI()
		{
		}

		public void ResetBattle()
		{
			armyList.Clear();
			currentAnime.Clear();
			currentIndex = 0;
		}

		public void DoAction()
		{
			//Clear current anime
			currentAnime.Clear();

			//Get current army which needs action
			Army currentArmy = GetNextArmy();

			//Get the delta X Y for against target
			int deltaX = currentArmy.Target.Position.X.CompareTo(currentArmy.Position.X);
			int deltaY = currentArmy.Target.Position.Y.CompareTo(currentArmy.Position.Y);

			//Get first/second move target
			Army firstTargetAmry = null;
			Army secondTargetArmy = null;

			//check if can attack
			Army targetArmyForAttack = null;
			if (currentArmy.Type == ArmType.Archer)
				targetArmyForAttack = GetAttackTarget(currentArmy, 4);
			else
				targetArmyForAttack = GetAttackTarget(currentArmy, 1);

			if (targetArmyForAttack != null)
			{
				int damage = currentArmy.DoAttack(targetArmyForAttack);
				currentAnime.Add(new BattleAnime() { CurrentArmy = currentArmy, AnimeType = AnimeType.Attack, FromPoint = currentArmy.Position, ToPoint = targetArmyForAttack.Position, TargetArmy = targetArmyForAttack, Value1 = damage });
			}
			else // if not attack, try move
			{
				if (deltaX == 0 && deltaY != 0)
				{
					firstTargetAmry = GetArmyByPosition(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
					if (firstTargetAmry == null && currentArmy.Action != ActionType.StandBy)
					{
						//move to first target
						System.Drawing.Point p = new System.Drawing.Point(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
						currentAnime.Add(new BattleAnime() { CurrentArmy = currentArmy, AnimeType = AnimeType.Move, FromPoint = currentArmy.Position, ToPoint = p });
						currentArmy.DoMoveTo(p);
					}
					else
						currentArmy.DoStandBy();
				}
				else if (deltaX != 0 && deltaY == 0)
				{
					firstTargetAmry = GetArmyByPosition(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
					if (firstTargetAmry == null && currentArmy.Action != ActionType.StandBy)
					{
						//move to first target
						System.Drawing.Point p = new System.Drawing.Point(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
						currentAnime.Add(new BattleAnime() { CurrentArmy = currentArmy, AnimeType = AnimeType.Move, FromPoint = currentArmy.Position, ToPoint = p });
						currentArmy.DoMoveTo(new System.Drawing.Point(currentArmy.Position.X + deltaX, currentArmy.Position.Y));
					}
					else
						currentArmy.DoStandBy();
				}
				else //deltax&y !=0
				{
					firstTargetAmry = GetArmyByPosition(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
					if (firstTargetAmry == null && currentArmy.Action != ActionType.StandBy)
					{
						//move to first target
						System.Drawing.Point p = new System.Drawing.Point(currentArmy.Position.X + deltaX, currentArmy.Position.Y);
						currentAnime.Add(new BattleAnime() { CurrentArmy = currentArmy, AnimeType = AnimeType.Move, FromPoint = currentArmy.Position, ToPoint = p });
						currentArmy.DoMoveTo(new System.Drawing.Point(currentArmy.Position.X + deltaX, currentArmy.Position.Y));
					}
					else
					{
						secondTargetArmy = GetArmyByPosition(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
						if (secondTargetArmy == null && currentArmy.Action != ActionType.StandBy)
						{
							//move to first target
							System.Drawing.Point p = new System.Drawing.Point(currentArmy.Position.X, currentArmy.Position.Y + deltaY);
							currentAnime.Add(new BattleAnime() { CurrentArmy = currentArmy, AnimeType = AnimeType.Move, FromPoint = currentArmy.Position, ToPoint = p });
							currentArmy.DoMoveTo(new System.Drawing.Point(currentArmy.Position.X, currentArmy.Position.Y + deltaY));
						}
						else
							currentArmy.DoStandBy();
					}
				}
			}
		}

		private Army GetNextArmy()
		{
			Army result = null;
			while (true)
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

		private Army GetAttackTarget(Army currentArmy, int range)
		{
			Army result = null;
			for (int i = 0; i < armyList.Count; i++)
			{
				if (armyList[i].Hp > 0 && armyList[i].Side != currentArmy.Side
					&&
					(
						(armyList[i].Position.X == currentArmy.Position.X && Math.Abs(armyList[i].Position.Y - currentArmy.Position.Y) <= range)
						||
						(armyList[i].Position.Y == currentArmy.Position.Y && Math.Abs(armyList[i].Position.X - currentArmy.Position.X) <= range)
						)
					)
				{
					return armyList[i];
				}
			}
			return result;
		}

		private void MoveToNextIndex()
		{
			currentIndex++;
			if (currentIndex == armyList.Count)
				currentIndex = 0;
		}
	}
}
