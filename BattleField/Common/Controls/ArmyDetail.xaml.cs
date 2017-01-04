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

namespace Common.Controls
{
	/// <summary>
	/// Interaction logic for ArmyDetail.xaml
	/// </summary>
	public partial class ArmyDetail : UserControl
	{
		public ArmyDetail()
		{
			InitializeComponent();
		}

		public void ApplyArmy(Army army)
		{
			lblName.Content = army.Type.ToString();
			lblHP.Content = army.Hp;
			lblAtk.Content = string.Format("{0}~{1}", army.Atk, army.Atk + army.AtkAlter);
			lblDef.Content = army.Def;
			lblAction.Content = army.Action.ToString();
		}
	}
}
