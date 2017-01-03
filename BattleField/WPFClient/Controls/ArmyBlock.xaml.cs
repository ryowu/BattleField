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

namespace WPFClient.Controls
{
	/// <summary>
	/// Interaction logic for ArmyBlock.xaml
	/// </summary>
	public partial class ArmyBlock : UserControl
	{
		private readonly Canvas parentField;

		private Army currentArmy;

		public ArmyBlock(Canvas parent)
		{
			InitializeComponent();
			parentField = parent;
		}

		public void ApplyArmy(Army arm)
		{
			currentArmy = arm;

			if (currentArmy.Hp <= 0)
			{
				this.Visibility = System.Windows.Visibility.Hidden;
				return;
			}

			this.Width = Constants.BLOCK_WIDTH;
			this.Height = Constants.BLOCK_WIDTH;

			SetTypeNameLabel();
			SetHPbar();
			SetPosition();
			SetSide();
		}

		private void SetSide()
		{
			if (currentArmy.Side == BattleSide.Player1)
				this.lblTypeName.Background = new SolidColorBrush(Color.FromArgb(255, 100, 149, 237));
			else
				this.lblTypeName.Background = new SolidColorBrush(Color.FromArgb(255, 231, 231, 231));
		}

		private void SetTypeNameLabel()
		{
			string typeName = string.Empty;
			switch (currentArmy.Type)
			{
				case ArmType.Archer:
					{
						typeName = "弓箭手";
						break;
					}
				case ArmType.Cavalry:
					{
						typeName = "骑兵";
						break;
					}
				case ArmType.Infantry:
					{
						typeName = "步兵";
						break;
					}
				case ArmType.Lancer:
					{
						typeName = "枪兵";
						break;
					}
				case ArmType.Hero:
					{
						typeName = ((Hero)currentArmy).HeroName;
						break;
					}
			}
			lblTypeName.Content = typeName;
		}

		private void SetHPbar()
		{
			int hp = currentArmy.Hp;
			lblHP.Width = Convert.ToInt32((double)hp / 100.0 * (double)Constants.BLOCK_WIDTH);
			if (hp <= 20)
			{
				this.lblHP.Background = new SolidColorBrush(Colors.Red);
			}
			else if (hp <= 50)
			{
				this.lblHP.Background = new SolidColorBrush(Colors.Orange);
			}
			else
				this.lblHP.Background = new SolidColorBrush(Color.FromArgb(255, 35, 255, 0));
		}

		private void SetPosition()
		{
			Canvas.SetLeft(this, currentArmy.Position.X * (Constants.BLOCK_WIDTH + 2));
			Canvas.SetTop(this, currentArmy.Position.Y * (Constants.BLOCK_WIDTH + 2));
		}
	}
}
