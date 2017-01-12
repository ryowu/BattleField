using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

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

			SetTypeImage();
			SetHPbar();
			SetSide();
			SetPosition();
		}

		private void SetPosition()
		{
			Canvas.SetLeft(this, Utility.PointToCanvas(currentArmy.Position.X));
			Canvas.SetTop(this, Utility.PointToCanvas(currentArmy.Position.Y));
		}

		private void SetSide()
		{
			if (currentArmy.Side == BattleSide.Player1)
				this.lblTypeName.Background = new SolidColorBrush(Color.FromArgb(255, 112, 146, 190));
			else
				this.lblTypeName.Background = new SolidColorBrush(Color.FromArgb(255, 231, 231, 231));
		}

		private void SetTypeImage()
		{
			imgArmy.Source = ImageManager.Instance.GetArmyImage(currentArmy.Type, currentArmy.Side == BattleSide.Player1 ? ImageDirection.LeftToRight : ImageDirection.RightToLeft);
			if (currentArmy.Type == ArmType.Hero)
			{
				lblTypeName.Content = "武将";
			}
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
