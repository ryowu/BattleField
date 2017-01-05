using Common.ArmyTypes;
using Common.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Common
{
	public class BattleManager
	{
		private ArmyAI AI;
		private List<Army> armyList;
		private Canvas innerCanvas;
		private List<ArmyBlock> armyBlocks = new List<ArmyBlock>();

		public event EventHandler OnActionCompleted;
		DoubleAnimation blockMovingAnime = new DoubleAnimation();

		public BattleManager(Canvas c)
		{
			this.innerCanvas = c;
			AI = new ArmyAI();
			blockMovingAnime.Completed += OnAnimeCompleted;
		}

		public void InitializeBattle(List<Army> armyList)
		{
			Reset();
			this.armyList = armyList;
			AI.ArmyList = this.armyList;
			InitializePlayer();
		}

		public void Reset()
		{
			AI.ResetBattle();
			armyBlocks.Clear();
		}

		public void DoAction()
		{
			//AI calculate
			AI.DoAction();

			//Play anime for specific item
			if (AI.CurrentAnime.Count > 0)
				PlayAnime(AI.CurrentAnime[0]);
			else
				OnAnimeCompleted(null, null);
		}

		private void PlayAnime(BattleAnime ba)
		{
			if (ba.AnimeType == AnimeType.Move)
			{
				if (ba.FromPoint.X == ba.ToPoint.X)
				{
					blockMovingAnime.From = Canvas.GetTop(ba.CurrentArmy.Block);
					blockMovingAnime.To = ba.ToPoint.Y * (Constants.BLOCK_WIDTH + 2);
					blockMovingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
					ba.CurrentArmy.Block.BeginAnimation(Canvas.TopProperty, blockMovingAnime);
				}
				else
				{
					blockMovingAnime.From = Canvas.GetLeft(ba.CurrentArmy.Block);
					blockMovingAnime.To = ba.ToPoint.X * (Constants.BLOCK_WIDTH + 2);
					blockMovingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
					ba.CurrentArmy.Block.BeginAnimation(Canvas.LeftProperty, blockMovingAnime);
				}
			}
			else if (ba.AnimeType == AnimeType.Attack)
			{
 
			}
		}

		private void OnAnimeCompleted(object sender, EventArgs e)
		{
			//Refresh all army
			for (int i = 0; i < armyList.Count; i++)
			{
				armyBlocks[i].ApplyArmy(armyList[i]);
			}

			//Inform the main UI that current action is completed (include all animation)
			if (OnActionCompleted != null)
				OnActionCompleted(null, null);
		}

		private void InitializePlayer()
		{
			foreach (var a in armyList)
			{
				//Create UI block
				ArmyBlock block = new ArmyBlock();
				block.MouseDown += block_MouseDown;
				innerCanvas.Children.Add(block);
				armyBlocks.Add(block);
				a.Block = block;
				block.Width = Constants.BLOCK_WIDTH;
				block.Height = Constants.BLOCK_WIDTH;
				Canvas.SetLeft(block, a.Position.X * (Constants.BLOCK_WIDTH + 2));
				Canvas.SetTop(block, a.Position.Y * (Constants.BLOCK_WIDTH + 2));
			}
		}

		private void block_MouseDown(object sender, MouseButtonEventArgs e)
		{
			//Set focus
			armyBlocks.ForEach((a) => a.Focused = false);
			ArmyBlock ab = (ArmyBlock)sender;
			ab.Focused = true;
			SetFocusBlock(ab);
		}

		private void SetFocusBlock(ArmyBlock ab)
		{
			//if (ab.CurrentArmy.Hp <= 0)
			//{
			//	borderFocus.Visibility = System.Windows.Visibility.Hidden;
			//	return;
			//}

			//Canvas.SetLeft(borderFocus, Canvas.GetLeft(ab) - 2);
			//Canvas.SetTop(borderFocus, Canvas.GetTop(ab) - 2);
			//Canvas.SetZIndex(borderFocus, Canvas.GetZIndex(ab) - 1);
			////armyDetailControl.ApplyArmy(ab.CurrentArmy);
			//borderFocus.Visibility = System.Windows.Visibility.Visible;
		}
	}
}
