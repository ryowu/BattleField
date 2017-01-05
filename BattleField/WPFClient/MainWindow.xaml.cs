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
		private BattleManager bm;
		//private DoubleAnimation blockMovingAnime = new DoubleAnimation();

		private DispatcherTimer timerMain = new DispatcherTimer();
		private List<Army> testArmyList = new List<Army>();

		public MainWindow()
		{
			InitializeComponent();
			timerMain.Interval = new TimeSpan(0, 0, 0, 0, 200);
			timerMain.Tick += timerMain_Tick;

			//blockMovingAnime.Completed += blockMovingAnime_Completed;

			bm = new BattleManager(mainField);
			bm.OnActionCompleted += bm_OnActionCompleted;
		}

		#region Private Methods

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
				a.Position = item.Key;
				a.Target = enemyHero;
				a.Side = side;
				a.Action = ActionType.Forward;
				testArmyList.Add(a);
			}
		}

		#endregion

		#region SignalR Client Methods

		/// <summary>
		/// Tick from server
		/// </summary>
		private void BattleTick(List<Army> armyList)
		{
			bm.DoAction();
		}

		#endregion

		#region Events
		private void timerMain_Tick(object sender, EventArgs e)
		{
			//simulate server invoking
			timerMain.Stop();
			BattleTick(testArmyList);
		}

		private void InitButton_Click(object sender, RoutedEventArgs e)
		{
			InitializeBattleField();
			bm.InitializeBattle(testArmyList);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			timerMain.Start();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			timerMain.Stop();
		}

		private void bm_OnActionCompleted(object sender, EventArgs e)
		{
			//simulate invoke server method to tell server that current action is complete
			timerMain.Start();
			//todo:Invoke server method
		}

		#endregion
	}
}
