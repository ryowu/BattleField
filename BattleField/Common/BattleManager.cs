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
		public event EventHandler OnBlockSelected;

		DoubleAnimation blockMovingAnime = new DoubleAnimation();
		DoubleAnimationUsingKeyFrames blockShakingAnime = new DoubleAnimationUsingKeyFrames();
		DoubleAnimation hpChangeAnime = new DoubleAnimation();

		Label lblHPChange = new Label();

		public BattleManager(Canvas c)
		{
			this.innerCanvas = c;
			AI = new ArmyAI();

			lblHPChange.Visibility = Visibility.Hidden;
			lblHPChange.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
			//lblHPChange.Effect = new 
			innerCanvas.Children.Add(lblHPChange);

			blockMovingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
			blockMovingAnime.Completed += OnAnimeCompleted;

			blockShakingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
			blockShakingAnime.Completed += blockShakingAnime_Completed;
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
					ba.CurrentArmy.Block.BeginAnimation(Canvas.TopProperty, blockMovingAnime);
				}
				else
				{
					blockMovingAnime.From = Canvas.GetLeft(ba.CurrentArmy.Block);
					blockMovingAnime.To = ba.ToPoint.X * (Constants.BLOCK_WIDTH + 2);
					ba.CurrentArmy.Block.BeginAnimation(Canvas.LeftProperty, blockMovingAnime);
				}
			}
			else if (ba.AnimeType == AnimeType.Attack)
			{
				blockShakingAnime.KeyFrames.Clear();
				double left = Canvas.GetLeft(ba.CurrentArmy.Block);
				blockShakingAnime.KeyFrames.Add(new LinearDoubleKeyFrame(left - 2));
				blockShakingAnime.KeyFrames.Add(new LinearDoubleKeyFrame(left + 2));
				blockShakingAnime.KeyFrames.Add(new LinearDoubleKeyFrame(left - 2));
				blockShakingAnime.KeyFrames.Add(new LinearDoubleKeyFrame(left + 2));
				blockShakingAnime.KeyFrames.Add(new LinearDoubleKeyFrame(left));
				ba.CurrentArmy.Block.BeginAnimation(Canvas.LeftProperty, blockShakingAnime);

				lblHPChange.Content = string.Format("-{0}", ba.Value1);
				double enemyX = 2 + ba.ToPoint.X * (Constants.BLOCK_WIDTH + 2);
				double enemyY = 2 + ba.ToPoint.Y * (Constants.BLOCK_WIDTH + 2);
				Canvas.SetLeft(lblHPChange, enemyX + 5);
				Canvas.SetTop(lblHPChange, enemyY + 28);

				hpChangeAnime.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				hpChangeAnime.From = enemyY + 10;
				hpChangeAnime.To = enemyY - 30;
				hpChangeAnime.Completed += OnAnimeCompleted;
				lblHPChange.Visibility = Visibility.Visible;
				lblHPChange.BeginAnimation(Canvas.TopProperty, hpChangeAnime);
				Canvas.SetZIndex(lblHPChange, Canvas.GetZIndex(ba.TargetArmy.Block) + 1);
			}
		}

		private void blockShakingAnime_Completed(object sender, EventArgs e)
		{
			
		}

		private void OnAnimeCompleted(object sender, EventArgs e)
		{
			lblHPChange.Visibility = Visibility.Hidden;
			ApplyArmyToUI();

			//Inform the main UI that current action is completed (include all animation)
			if (OnActionCompleted != null)
				OnActionCompleted(null, null);
		}

		private void ApplyArmyToUI()
		{
			//Refresh all army
			for (int i = 0; i < armyList.Count; i++)
			{
				armyBlocks[i].ApplyArmy(armyList[i]);
			}
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
			}
			ApplyArmyToUI();
		}

		private void block_MouseDown(object sender, MouseButtonEventArgs e)
		{
			//Set focus
			armyBlocks.ForEach((a) => a.Focused = false);
			ArmyBlock ab = (ArmyBlock)sender;
			ab.Focused = true;
			SetFocusBlock(ab);

			if (OnBlockSelected != null)
				OnBlockSelected(sender, e);
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
