using Common;
using Common.ArmyTypes;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClient.Controls;

namespace WPFClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<ArmyBlock> armyBlocks = new List<ArmyBlock>();


		private List<Army> testArmyList = new List<Army>();
		

		private ArmyAI AI;

		public MainWindow()
		{
			InitializeComponent();
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
			}
		}

		#endregion

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//Init AI
			AI = new ArmyAI(testArmyList);
			AI.ResetBattle();

			InitializeBattleField();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			AI.DoAction();
			BattleTick(testArmyList);
		}

		private void InitializeBattleField()
		{
			Hero hero1 = new Hero() { Hp = 100, Position = new System.Drawing.Point(0, 2), Type = ArmType.Hero, HeroName = "Ryo", Side = BattleSide.Player1 };
			Hero hero2 = new Hero() { Hp = 100, Position = new System.Drawing.Point(13, 2), Type = ArmType.Hero, HeroName = "James", Side = BattleSide.Player2 };


			Dictionary<System.Drawing.Point, ArmType> armyDataPlayer1 = new Dictionary<System.Drawing.Point, ArmType>();
			armyDataPlayer1[new System.Drawing.Point(0, 0)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(1, 1)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(2, 2)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(3, 3)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(2, 4)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(1, 5)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(0, 6)] = ArmType.Infantry;
			armyDataPlayer1[new System.Drawing.Point(0, 3)] = ArmType.Hero;

			Dictionary<System.Drawing.Point, ArmType> armyDataPlayer2 = new Dictionary<System.Drawing.Point, ArmType>();
			armyDataPlayer2[new System.Drawing.Point(13, 0)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(12, 1)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(11, 2)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(10, 3)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(11, 4)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(12, 5)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(13, 6)] = ArmType.Infantry;
			armyDataPlayer2[new System.Drawing.Point(13, 3)] = ArmType.Hero;


			InitializePlayer(armyDataPlayer1, BattleSide.Player1, hero1, hero2);
			InitializePlayer(armyDataPlayer2, BattleSide.Player2, hero2, hero1);
		}

		private void InitializePlayer(Dictionary<System.Drawing.Point, ArmType> data, BattleSide side, Hero myHero, Hero enemyHero)
		{
			foreach (var item in data)
			{
				//Create UI block
				ArmyBlock block = new ArmyBlock(mainField);
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
				a.Position = item.Key;
				a.Target = enemyHero;
				a.Side = side;
				a.Action = ActionType.Forward;
				block.ApplyArmy(a);
				testArmyList.Add(a);
			}
		}
	}
}
