using Common;
using System;
using System.Collections.Generic;
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

			// Player1
			ArmyBlock a = new ArmyBlock(mainField);
			mainField.Children.Add(a);
			armyBlocks.Add(a);

			ArmyBlock a1 = new ArmyBlock(mainField);
			mainField.Children.Add(a1);
			armyBlocks.Add(a1);

			ArmyBlock h1 = new ArmyBlock(mainField);
			mainField.Children.Add(h1);
			armyBlocks.Add(h1);

			Army army1 = new Army() { Hp = 100, Position = new System.Drawing.Point(1, 1), Type = ArmType.Infantry, Side = BattleSide.Player1 };
			a.ApplyArmy(army1);

			Army army2 = new Army() { Hp = 100, Position = new System.Drawing.Point(1, 2), Type = ArmType.Infantry, Side = BattleSide.Player1 };
			a1.ApplyArmy(army2);

			Hero hero1 = new Hero() { Hp = 100, Position = new System.Drawing.Point(0, 2), Type = ArmType.Hero, HeroName = "Ryo", Side = BattleSide.Player1 };
			h1.ApplyArmy(hero1);

			// Player2
			ArmyBlock b = new ArmyBlock(mainField);
			mainField.Children.Add(b);
			armyBlocks.Add(b);

			ArmyBlock b1 = new ArmyBlock(mainField);
			mainField.Children.Add(b1);
			armyBlocks.Add(b1);

			ArmyBlock h2 = new ArmyBlock(mainField);
			mainField.Children.Add(h2);
			armyBlocks.Add(h2);

			Army army3 = new Army() { Hp = 100, Position = new System.Drawing.Point(12, 1), Type = ArmType.Cavalry, Side = BattleSide.Player2 };
			b.ApplyArmy(army3);

			Army army4 = new Army() { Hp = 100, Position = new System.Drawing.Point(12, 2), Type = ArmType.Lancer, Side = BattleSide.Player2 };
			b1.ApplyArmy(army4);

			Hero hero2 = new Hero() { Hp = 100, Position = new System.Drawing.Point(13, 2), Type = ArmType.Hero, HeroName = "James", Side = BattleSide.Player2 };
			h2.ApplyArmy(hero2);

			//set target
			army1.Target = hero2;
			army2.Target = hero2;
			hero1.Target = hero2;

			army3.Target = hero1;
			army4.Target = hero1;
			hero2.Target = hero1;

			//Compose army both list follow order
			testArmyList.Add(army1);
			testArmyList.Add(army2);
			testArmyList.Add(hero1);
			testArmyList.Add(army3);
			testArmyList.Add(army4);
			testArmyList.Add(hero2);

			//Init action to forward
			testArmyList.ForEach((ay) => ay.Action = ActionType.Forward);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			AI.DoAction();
			BattleTick(testArmyList);
		}
	}
}
