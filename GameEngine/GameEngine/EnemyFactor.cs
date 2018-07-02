using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
	public class EnemyFactor
	{
		public Enemy Create(string name, bool isBoss = false)
		{
			if (name is null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (isBoss)
			{
				if (!IsValidBossName(name))
				{
					throw new EnemyCreationException(
						$"{name} is not a valid name for a Boss enemy, Boss enemy names must end ", name
						);
				}
				return new BossEnemy { Name = name };
			}
			return new NormalEnemy { Name = name };
		}

		private bool IsValidBossName(string name) => name.EndsWith("King") ||
																									name.EndsWith("Queen");
	}
}
