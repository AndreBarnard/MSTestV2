using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
	public class NormalEnemy : Enemy
	{
		public override double TotalSpecialPower => 500;

		public override double SpecialPowerUses => 4;
	}
}
