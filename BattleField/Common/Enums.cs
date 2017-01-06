using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public enum BattleSide
	{
 		Player1,
		Player2
	}

	public enum ArmType
	{
		Infantry,
		Cavalry,
		Archer,
		Lancer,
		Hero
	}

	public enum AnimeType
	{
 		Move,
		Attack
	}

	public enum ActionType
	{
 		Forward,
		Backward,
		StandBy
	}

	public enum ImageDirection
	{
 		LeftToRight,
		RightToLeft
	}

	public enum Direction
	{
 		Up,
		Down,
		Left,
		Right
	}
}
