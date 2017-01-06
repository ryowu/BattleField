using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Common
{
	public class ImageManager
	{
		private static ImageManager _instance;

		public static ImageManager Instance
		{
			get
			{
				if (_instance == null) _instance = new ImageManager();
				return ImageManager._instance;
			}
		}

		private BitmapImage armyImage = new BitmapImage();
		private TransformedBitmap reversedArmyImage;

		private ImageManager()
		{
			armyImage.BeginInit();
			armyImage.UriSource = new Uri("pack://application:,,,/Common;component/Resources/warsongstyle.png");
			armyImage.EndInit();

			reversedArmyImage = new TransformedBitmap();
			reversedArmyImage.BeginInit();
			reversedArmyImage.Source = armyImage.Clone();
			var transform = new ScaleTransform(-1, 1, 0, 0);
			reversedArmyImage.Transform = transform;
			reversedArmyImage.EndInit();
		}

		public CroppedBitmap GetArmyImage(ArmType armyType, ImageDirection d)
		{
			CroppedBitmap cb = null;
			switch (armyType)
			{
				case ArmType.Archer:
					{
						if (d == ImageDirection.RightToLeft)
							cb = new CroppedBitmap(reversedArmyImage, new Int32Rect(251, 40, 38, 38));
						else
							cb = new CroppedBitmap(armyImage, new Int32Rect(177, 40, 38, 38));
						break;
					}
				case ArmType.Cavalry:
					{
						if (d == ImageDirection.RightToLeft)
							cb = new CroppedBitmap(reversedArmyImage, new Int32Rect(215, 115, 48, 49));
						else
							cb = new CroppedBitmap(armyImage, new Int32Rect(203, 115, 48, 49));
						break;
					}
				case ArmType.Infantry:
					{
						if (d == ImageDirection.RightToLeft)
							cb = new CroppedBitmap(reversedArmyImage, new Int32Rect(361, 40, 40, 38));
						else
							cb = new CroppedBitmap(armyImage, new Int32Rect(65, 40, 40, 38));
						break;
					}
				case ArmType.Lancer:
					{
						if (d == ImageDirection.RightToLeft)
							cb = new CroppedBitmap(reversedArmyImage, new Int32Rect(119, 41, 43, 37));
						else
							cb = new CroppedBitmap(armyImage, new Int32Rect(304, 41, 43, 37));
						break;
					}
				case ArmType.Hero:
					{
						cb = null;
						break;
					}
			}

			return cb;
		}
	}
}
