using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Utility
	{
		public static int GetRangedValue(int value, int min, int max)
		{
			if (value < min)
				return min;
			else if (value > max)
				return max;
			else
				return value;
		}

		public static int GetRangedArmHP(int value)
		{
			return GetRangedValue(value, 0, 100);
		}

		public static int RandomNum(int min, int max)
		{
			Random r = new Random(DateTime.Now.GetHashCode());
			return r.Next(min, max);
		}
	}
}
