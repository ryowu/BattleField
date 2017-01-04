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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Common.Controls
{
	/// <summary>
	/// Interaction logic for ArmyBlock.xaml
	/// </summary>
	public partial class ArmyBlock : UserControl
	{
		private readonly Canvas parentField;

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

		private DoubleAnimation blockMovingAnime;

		public ArmyBlock(Canvas parent, DoubleAnimation blockMovingAnime)
		{
			InitializeComponent();
			this.parentField = parent;
			this.blockMovingAnime = blockMovingAnime;
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
			lblTypeName.Content = currentArmy.Type.ToString();
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

		public void PlayMoveAnime(System.Drawing.Point newPosition)
		{
			//move vertically
			if (Canvas.GetLeft(this) == newPosition.X * (Constants.BLOCK_WIDTH + 2))
			{
				blockMovingAnime.From = Canvas.GetTop(this);
				blockMovingAnime.To = newPosition.Y * (Constants.BLOCK_WIDTH + 2);
				blockMovingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
				this.BeginAnimation(Canvas.TopProperty, blockMovingAnime);
			}
			else //move horizontally
			{
				blockMovingAnime.From = Canvas.GetLeft(this);
				blockMovingAnime.To = newPosition.X * (Constants.BLOCK_WIDTH + 2);
				blockMovingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
				this.BeginAnimation(Canvas.LeftProperty, blockMovingAnime);
			}
		}

		public void PlayStandByAnime()
		{
			blockMovingAnime.From = Canvas.GetTop(this);
			blockMovingAnime.To = Canvas.GetTop(this);
			blockMovingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
			this.BeginAnimation(Canvas.TopProperty, blockMovingAnime);
		}

		public void PlayAttackAnime(Army enemy)
		{
			blockMovingAnime.From = Canvas.GetTop(this);
			blockMovingAnime.To = Canvas.GetTop(this);
			blockMovingAnime.Duration = new Duration(TimeSpan.FromSeconds(0.3));
			this.BeginAnimation(Canvas.TopProperty, blockMovingAnime);
		}
	}
}
