using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameEngine.Test
{
	[TestClass]
	[TestCategory("Enemy Creation")]
	public class EnemyFactorShould
	{

		[TestMethod]
		public void NotAllowNullName()
		{

			Console.WriteLine("Creating EnemyFactory");
			EnemyFactor sut = new EnemyFactor();


			Console.WriteLine("Calling create method");
			Assert.ThrowsException<ArgumentNullException>(
				() => sut.Create(null)
				);
		}


		[TestMethod]
		public void OnlyAllowKingOrQueenBossEnemies()
		{
			EnemyFactor sut = new EnemyFactor();

			EnemyCreationException ex = Assert.ThrowsException<EnemyCreationException>(
				() => sut.Create("Zombie", true)
				);

			Assert.AreEqual("Zombie", ex.RequestedEnemyName);
		}

		[TestMethod]
		public void CreateNormalEnemyByDefault()
		{
			EnemyFactor sut = new EnemyFactor();

			Enemy enemy = sut.Create("Zombie");

			Assert.IsInstanceOfType(enemy, typeof(NormalEnemy));
		}


		//[TestMethod]
		//public void CreateNormalEnemyByDefault_NotTypeExample()
		//{
		//	EnemyFactor sut = new EnemyFactor();

		//	Enemy enemy = sut.Create("Zombie");

		//	Assert.IsNotInstanceOfType(enemy, typeof(NormalEnemy));
		//}

		[TestMethod]
		public void CreateBossEnemy()
		{
			EnemyFactor sut = new EnemyFactor();

			Enemy enemy = sut.Create("Zombie King",true);

			Assert.IsInstanceOfType(enemy, typeof(BossEnemy));
		}

		[TestMethod]
		public void CreateSeparatedInstances()
		{
			EnemyFactor sut = new EnemyFactor();

			Enemy enemy1 = sut.Create("Zombie");
			Enemy enemy2 = sut.Create("Zombie");

			Assert.AreNotSame(enemy1, enemy2);
		}

	}
}
