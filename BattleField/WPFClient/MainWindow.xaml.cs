using Common;
using Common.ArmyTypes;
using Common.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private DoubleAnimation blockMovingAnime = new DoubleAnimation();

		private DispatcherTimer timerMain = new DispatcherTimer();

		private List<ArmyBlock> armyBlocks = new List<ArmyBlock>();


		private List<Army> testArmyList = new List<Army>();


		private ArmyAI AI;

		public MainWindow()
		{
			InitializeComponent();
			timerMain.Interval = new TimeSpan(0, 0, 0, 0, 200);
			timerMain.Tick += timerMain_Tick;

			blockMovingAnime.Completed += blockMovingAnime_Completed;
		}

		private void blockMovingAnime_Completed(object sender, EventArgs e)
		{
			timerMain.Start();
		}

		void timerMain_Tick(object sender, EventArgs e)
		{
			timerMain.Stop();
			AI.DoAction();
			BattleTick(testArmyList);
		}

		#region SignalR Client Methods

		/// <summary>
		/// Tick from server
		/// </summary>
		private void BattleTick(List<Army> armyList)
		{
			//Play anime for specific item

			//Refresh all army
			for (int i = 0; i < armyList.Count; i++)
			{
				armyBlocks[i].ApplyArmy(armyList[i]);

				//set focus
				if(armyBlocks[i].Focused)
					SetFocusBorder(armyBlocks[i]);
			}
		}

		#endregion

		private void InitButton_Click(object sender, RoutedEventArgs e)
		{
			//Init AI
			AI = new ArmyAI(testArmyList);
			AI.ResetBattle();

			InitializeBattleField();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			timerMain.Start();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			timerMain.Stop();
		}

		private void InitializeBattleField()
		{
			Hero hero1 = new Hero() { Hp = 100, Position = new System.Drawing.Point(0, 2), Type = ArmType.Hero, HeroName = "Ryo", Side = BattleSide.Player1 };
			Hero hero2 = new Hero() { Hp = 100, Position = new System.Drawing.Point(13, 2), Type = ArmType.Hero, HeroName = "James", Side = BattleSide.Player2 };


			Dictionary<System.Drawing.Point, ArmType> armyDataPlayer1 = new Dictionary<System.Drawing.Point, ArmType>();
			armyDataPlayer1[new System.Drawing.Point(1, 3)] = ArmType.Archer;
			armyDataPlayer1[new System.Drawing.Point(2, 3)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(3, 3)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(1, 2)] = ArmType.Lancer;
			armyDataPlayer1[new System.Drawing.Point(1, 4)] = ArmType.Lancer;
			armyDataPlayer1[new System.Drawing.Point(1, 1)] = ArmType.Cavalry;
			armyDataPlayer1[new System.Drawing.Point(1, 5)] = ArmType.Cavalry;
			armyDataPlayer1[new System.Drawing.Point(0, 3)] = ArmType.Hero;

			Dictionary<System.Drawing.Point, ArmType> armyDataPlayer2 = new Dictionary<System.Drawing.Point, ArmType>();
			armyDataPlayer2[new System.Drawing.Point(10, 0)] = ArmType.Cavalry;
			armyDataPlayer2[new System.Drawing.Point(10, 1)] = ArmType.Cavalry;
			armyDataPlayer2[new System.Drawing.Point(13, 2)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(15, 3)] = ArmType.Archer;
			armyDataPlayer2[new System.Drawing.Point(11, 4)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(15, 5)] = ArmType.Lancer;
			armyDataPlayer2[new System.Drawing.Point(15, 6)] = ArmType.Lancer;
			armyDataPlayer2[new System.Drawing.Point(13, 3)] = ArmType.Hero;


			InitializePlayer(armyDataPlayer1, BattleSide.Player1, hero1, hero2);
			InitializePlayer(armyDataPlayer2, BattleSide.Player2, hero2, hero1);
		}

		private void InitializePlayer(Dictionary<System.Drawing.Point, ArmType> data, BattleSide side, Hero myHero, Hero enemyHero)
		{
			foreach (var item in data)
			{
				//Create UI block
				ArmyBlock block = new ArmyBlock(mainField, blockMovingAnime);
				block.MouseDown += block_MouseDown;
				mainField.Children.Add(block);
				armyBlocks.Add(block);
				//Create Army
				Army a = null;
				switch (item.Value)
				{
					case ArmType.Archer:
						{
							a = new Archer();
							break;
						}
					case ArmType.Cavalry:
						{
							a = new Cavalry();
							break;
						}
					case ArmType.Infantry:
						{
							a = new Infantry();
							break;
						}
					case ArmType.Lancer:
						{
							a = new Lancer();
							break;
						}
					case ArmType.Hero:
						{
							a = myHero;
							break;
						}
				}
				a.Block = block;
				a.Position = item.Key;
				a.Target = enemyHero;
				a.Side = side;
				a.Action = ActionType.Forward;
				block.Width = Constants.BLOCK_WIDTH;
				block.Height = Constants.BLOCK_WIDTH;
				Canvas.SetLeft(block, a.Position.X * (Constants.BLOCK_WIDTH + 2));
				Canvas.SetTop(block, a.Position.Y * (Constants.BLOCK_WIDTH + 2));

				//block.ApplyArmy(a);
				testArmyList.Add(a);
			}
		}

		private void block_MouseDown(object sender, MouseButtonEventArgs e)
		{
			armyBlocks.ForEach((a) => a.Focused = false);
			ArmyBlock ab = (ArmyBlock)sender;
			ab.Focused = true;
			SetFocusBorder(ab);
		}

		private void SetFocusBorder(ArmyBlock ab)
		{
			if (ab.CurrentArmy.Hp <= 0)
			{
				borderFocus.Visibility = System.Windows.Visibility.Hidden;
				return;
			}

			Canvas.SetLeft(borderFocus, Canvas.GetLeft(ab) - 2);
			Canvas.SetTop(borderFocus, Canvas.GetTop(ab) - 2);
			Canvas.SetZIndex(borderFocus, Canvas.GetZIndex(ab) - 1);
			//armyDetailControl.ApplyArmy(ab.CurrentArmy);
			borderFocus.Visibility = System.Windows.Visibility.Visible;
		}
	}
}
