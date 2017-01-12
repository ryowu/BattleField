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

		private DispatcherTimer timerMain = new DispatcherTimer();
		private List<Army> testArmyList = new List<Army>();

		public MainWindow()
		{
			InitializeComponent();
			timerMain.Interval = new TimeSpan(0, 0, 0, 0, 10);
			timerMain.Tick += timerMain_Tick;

			bm = new BattleManager(mainField);
			bm.OnActionCompleted += bm_OnActionCompleted;
			bm.OnBlockSelected += bm_OnBlockSelected;
		}

		#region Private Methods

		private void InitializeBattleField()
		{
			Dictionary<System.Drawing.Point, ArmType> armyDataPlayer1 = new Dictionary<System.Drawing.Point, ArmType>();
			armyDataPlayer1[new System.Drawing.Point(1, 0)] = ArmType.Cavalry;
			armyDataPlayer1[new System.Drawing.Point(2, 1)] = ArmType.Archer;
			armyDataPlayer1[new System.Drawing.Point(3, 2)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(4, 3)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(5, 4)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(4, 5)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(3, 6)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(2, 7)] = ArmType.Archer;
			armyDataPlayer1[new System.Drawing.Point(1, 8)] = ArmType.Cavalry;
			armyDataPlayer1[new System.Drawing.Point(0, 4)] = ArmType.Hero;

			Dictionary<System.Drawing.Point, ArmType> armyDataPlayer2 = new Dictionary<System.Drawing.Point, ArmType>();
			armyDataPlayer2[new System.Drawing.Point(15, 0)] = ArmType.Cavalry;
			armyDataPlayer2[new System.Drawing.Point(14, 1)] = ArmType.Archer;
			armyDataPlayer2[new System.Drawing.Point(13, 2)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(12, 3)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(11, 4)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(12, 5)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(13, 6)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(14, 7)] = ArmType.Archer;
			armyDataPlayer2[new System.Drawing.Point(15, 8)] = ArmType.Cavalry;
			armyDataPlayer2[new System.Drawing.Point(16, 4)] = ArmType.Hero;


			Army hero1 = InitializePlayer(armyDataPlayer1, BattleSide.Player1);
			Army hero2 = InitializePlayer(armyDataPlayer2, BattleSide.Player2);

			testArmyList.ForEach((al) =>
				{
					//Use hero property
					if (al.Side == BattleSide.Player1)
					{
						al.Atk = hero1.Atk;
						al.Def = hero1.Def;
						al.AtkAlter = hero1.AtkAlter;
						al.Target = (Hero)hero2;
					}
					else
					{
						al.Atk = hero2.Atk;
						al.Def = hero2.Def;
						al.AtkAlter = hero2.AtkAlter;
						al.Target = (Hero)hero1;
					}
				});
		}

		private Army InitializePlayer(Dictionary<System.Drawing.Point, ArmType> data, BattleSide side)
		{
			Army result = null;
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
							a = new Hero();
							result = a;
							break;
						}
				}
				a.Position = item.Key;
				a.Side = side;

				if (item.Value == ArmType.Hero)
					a.Action = ActionType.StandBy;
				else
					a.Action = ActionType.Forward;
				testArmyList.Add(a);
			}
			return result;
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
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

		private void bm_OnBlockSelected(object sender, EventArgs e)
		{
			armyDetailControl.ApplyArmy(((ArmyBlock)sender).CurrentArmy);
		}
		#endregion
	}
}
