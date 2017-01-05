using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Common.Controls
{
	/// <summary>
	/// Interaction logic for ArmyBlock.xaml
	/// </summary>
	public partial class ArmyBlock : UserControl
	{
		private bool focused = false;

		public bool Focused
		{
			get { return focused; }
			set { focused = value; }
		}

		private Army currentArmy;

		public Army CurrentArmy
		{
			get { return currentArmy; }
		}

		//private DoubleAnimation blockMovingAnime;

		private DoubleAnimation hpHurtAnime;

		public ArmyBlock()
		{
			InitializeComponent();
			hpHurtAnime = new DoubleAnimation();
		}

		public void ApplyArmy(Army arm)
		{
			currentArmy = arm;

			if (currentArmy.Hp <= 0)
			{
				this.Visibility = System.Windows.Visibility.Hidden;
				return;
			}

			SetTypeNameLabel();
			SetHPbar();
			SetSide();
			SetPosition();
		}

		private void SetPosition()
		{
			Canvas.SetLeft(this, currentArmy.Position.X * (Constants.BLOCK_WIDTH + 2));
			Canvas.SetTop(this, currentArmy.Position.Y * (Constants.BLOCK_WIDTH + 2));
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
			hpHurtAnime.From = lblHP.Width;
			hpHurtAnime.To = Convert.ToInt32((double)hp / 100.0 * (double)Constants.BLOCK_WIDTH);
			hpHurtAnime.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100));
			lblHP.BeginAnimation(WidthProperty, hpHurtAnime);

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
	}
}
