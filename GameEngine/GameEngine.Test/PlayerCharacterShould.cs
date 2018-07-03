using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace GameEngine.Test
{
	[TestClass]
	public class PlayerCharacterShould
	{
		[TestMethod]
		[TestCategory("Player Defaults")]
		//[Ignore]
		public void BeInexperiencedWhenNew()
		{
			var sut = new PlayerCharacter();

			Assert.IsTrue(sut.IsNoob);
		}

		[TestMethod]
		[TestCategory("Player Defaults")]
		//[Ignore("Temporarly disabled for refactoring")]
		public void NotHaveNickNameByDefault()
		{
			var sut = new PlayerCharacter();

			Assert.IsNull(sut.NickName);
		}

		[TestMethod]
		[TestCategory("Player Defaults")]
		public void StartWithDefaultHealth()
		{
			var sut = new PlayerCharacter();

			Assert.AreEqual(100, sut.Health);
		}

		[DataTestMethod]
		//[DynamicData(nameof(GetDamages),DynamicDataSourceType.Method)]
		[DynamicData(nameof(ExternalHealthDamageTestData.TestData),
								typeof(ExternalHealthDamageTestData))]
		[TestCategory("Player Health")]
		public void TakeDamage_UsingCSV(int damage, int expectedHealth)
		{
			var sut = new PlayerCharacter();

			sut.TakeDamage(damage);

			Assert.AreEqual(expectedHealth, sut.Health);
		}


		[DataTestMethod]
		//[DynamicData(nameof(GetDamages),DynamicDataSourceType.Method)]
		[DynamicData(nameof(DamageData.GetDamages),
									typeof(DamageData), 
									DynamicDataSourceType.Method)]
		[TestCategory("Player Health")]
		public void TakeDamage_UsingMethod(int damage, int expectedHealth)
		{
			var sut = new PlayerCharacter();

			sut.TakeDamage(damage);

			Assert.AreEqual(expectedHealth, sut.Health);
		}

		public static IEnumerable<object[]> Damages
		{
			get
			{
				return new List<object[]>
				{
					new object[]{1, 99},
					new object[]{0, 100},
					new object[]{100, 1},
					new object[]{101, 1},
					new object[]{50, 50},
					new object[]{40, 60}
				};
			}
		}

		[DataTestMethod]
		[DynamicData(nameof(Damages))]
		[TestCategory("Player Health")]
		public void TakeDamage_UsingPropery(int damage, int expectedHealth)
		{
			var sut = new PlayerCharacter();

			sut.TakeDamage(damage);

			Assert.AreEqual(expectedHealth, sut.Health);
		}

		[DataTestMethod]
		[DataRow(1, 99)]
		[DataRow(0, 100)]
		[DataRow(100, 1)]
		[DataRow(101, 1)]
		[DataRow(50, 50)]
		[TestCategory("Player Health")]
		public void TakeDamage_USingDataRows(int damage, int expectedHealth)
		{
			var sut = new PlayerCharacter();

			sut.TakeDamage(damage);

			Assert.AreEqual(expectedHealth, sut.Health);
		}


		[TestMethod]
		[TestCategory("Player Health")]
		public void TakeDamage_NotEqual()
		{
			var sut = new PlayerCharacter();

			sut.TakeDamage(1);

			Assert.AreNotEqual(100, sut.Health);
		}

		[TestMethod]
		[TestCategory("Player Health")]
		[TestCategory("Another Category")]
		public void IncreaseHealtAfterSleeping()
		{
			var sut = new PlayerCharacter();

			sut.Sleep(); // Expect increase between 1 to 100 inclusive

			Assert.IsTrue(sut.Health >= 101 && sut.Health <= 200);

		}

		[TestMethod]
		public void CalculateFullName()
		{
			var sut = new PlayerCharacter();

			sut.FirstName = "Sarah";
			sut.LastName = "Smith";

			Assert.AreEqual("SARAH SMITH", sut.FullName, true);

		}

		[TestMethod]
		public void HaveFullNameStartingwithFirstName()
		{
			var sut = new PlayerCharacter();

			sut.FirstName = "Sarah";
			sut.LastName = "Smith";

			StringAssert.StartsWith(sut.FullName, "Sarah");

		}

		[TestMethod]
		public void HaveFullNameEndingWithLastName()
		{
			var sut = new PlayerCharacter();

			sut.FirstName = "Sarah";
			sut.LastName = "Smith";

			StringAssert.EndsWith(sut.FullName, "Smith");

		}

		[TestMethod]
		public void HaveFullName_SubstringAssertExample()
		{
			var sut = new PlayerCharacter();

			sut.FirstName = "Sarah";
			sut.LastName = "Smith";

			StringAssert.Contains(sut.FullName, "ah Sm");

		}


		[TestMethod]
		public void CalculateFullNameWithTitleCase()
		{
			var sut = new PlayerCharacter();

			sut.FirstName = "Sarah";
			sut.LastName = "Smith";

			StringAssert.Matches(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]"));

		}

		[TestMethod]
		public void HaveALongBow()
		{
			var sut = new PlayerCharacter();

			CollectionAssert.Contains(sut.Weapons, "Long Bow");
		}


		[TestMethod]
		public void NotHaveAStaffOfWonder()
		{
			var sut = new PlayerCharacter();

			CollectionAssert.DoesNotContain(sut.Weapons, "Staff Of Wonder");
		}

		[TestMethod]
		public void HaveAllExpectedWeapons()
		{
			var sut = new PlayerCharacter();

			var expectedWeapons = new[]
			{
				"Long Bow",
				"Short Bow",
				"Short Sword"
			};

			CollectionAssert.AreEqual(expectedWeapons, sut.Weapons);
		}

		[TestMethod]
		public void HaveAllExpectedWeapons_AnyOrder()
		{
			var sut = new PlayerCharacter();

			var expectedWeapons = new[]
			{
				"Short Bow",
				"Long Bow",
				"Short Sword"
			};

			CollectionAssert.AreEquivalent(expectedWeapons, sut.Weapons);
		}


		[TestMethod]
		public void HaveNoDupicateWeapons()
		{
			var sut = new PlayerCharacter();

			CollectionAssert.AllItemsAreUnique(sut.Weapons);
		}


		[TestMethod]
		public void HaveAtLeaseOneKindOfSword()
		{
			var sut = new PlayerCharacter();

			Assert.IsTrue(sut.Weapons.Any(weapon => weapon.Contains("Sword")));
			//custon assert later
		}

		[TestMethod]
		public void HaveNoEmptyDefaultWeapons()
		{
			var sut = new PlayerCharacter();

			Assert.IsFalse(sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
			//custon assert later
		}
	}
}
