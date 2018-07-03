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

		PlayerCharacter sut;
		[TestInitialize]
		public void TestInitialize()
		{
			sut = new PlayerCharacter()
			{
				FirstName = "Sarah",
				LastName = "Smith"
			};
		}

		[TestMethod]
		[PlayerDefaults]
		//[Ignore]
		public void BeInexperiencedWhenNew()
		{
			Assert.IsTrue(sut.IsNoob);
		}

		[TestMethod]
		[PlayerDefaults]
		//[Ignore("Temporarly disabled for refactoring")]
		public void NotHaveNickNameByDefault()
		{
			Assert.IsNull(sut.NickName);
		}

		[TestMethod]
		[PlayerDefaults]
		public void StartWithDefaultHealth()
		{
			Assert.AreEqual(100, sut.Health);
		}

		[DataTestMethod]
		[CSVDataSource("Damage.csv")]
		[PlayerHealth]
		public void TakeDamage_UsingCSV(int damage, int expectedHealth)
		{
			sut.TakeDamage(damage);

			Assert.AreEqual(expectedHealth, sut.Health);
		}


		[DataTestMethod]
		//[DynamicData(nameof(GetDamages),DynamicDataSourceType.Method)]
		[DynamicData(nameof(DamageData.GetDamages),
									typeof(DamageData),
									DynamicDataSourceType.Method)]
		[PlayerHealth]
		public void TakeDamage_UsingMethod(int damage, int expectedHealth)
		{

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
		[PlayerHealth]
		public void TakeDamage_UsingPropery(int damage, int expectedHealth)
		{
			sut.TakeDamage(damage);

			Assert.AreEqual(expectedHealth, sut.Health);
		}

		[DataTestMethod]
		[DataRow(1, 99)]
		[DataRow(0, 100)]
		[DataRow(100, 1)]
		[DataRow(101, 1)]
		[DataRow(50, 50)]
		[PlayerHealth]
		public void TakeDamage_USingDataRows(int damage, int expectedHealth)
		{
			sut.TakeDamage(damage);

			Assert.AreEqual(expectedHealth, sut.Health);
		}


		[TestMethod]
		[PlayerHealth]
		public void TakeDamage_NotEqual()
		{
			sut.TakeDamage(1);

			Assert.AreNotEqual(100, sut.Health);
		}

		[TestMethod]
		[PlayerHealth]
		[TestCategory("Another Category")]
		public void IncreaseHealtAfterSleeping()
		{

			sut.Sleep(); // Expect increase between 1 to 100 inclusive

			Assert.That.IsInRange(sut.Health, 101, 200);

		}

		[TestMethod]
		public void CalculateFullName()
		{
			Assert.AreEqual("SARAH SMITH", sut.FullName, true);

		}

		[TestMethod]
		public void HaveFullNameStartingwithFirstName()
		{

			StringAssert.StartsWith(sut.FullName, "Sarah");

		}

		[TestMethod]
		public void HaveFullNameEndingWithLastName()
		{
			StringAssert.EndsWith(sut.FullName, "Smith");
		}

		[TestMethod]
		public void HaveFullName_SubstringAssertExample()
		{
			StringAssert.Contains(sut.FullName, "ah Sm");
		}


		[TestMethod]
		public void CalculateFullNameWithTitleCase()
		{
			StringAssert.Matches(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]"));
		}

		[TestMethod]
		public void HaveALongBow()
		{
			CollectionAssert.Contains(sut.Weapons, "Long Bow");
		}


		[TestMethod]
		public void NotHaveAStaffOfWonder()
		{

			CollectionAssert.DoesNotContain(sut.Weapons, "Staff Of Wonder");
		}

		[TestMethod]
		public void HaveAllExpectedWeapons()
		{
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
			CollectionAssert.AllItemsAreUnique(sut.Weapons);
		}


		[TestMethod]
		public void HaveAtLeaseOneKindOfSword()
		{
			//Assert.IsTrue(sut.Weapons.Any(weapon => weapon.Contains("Sword")));

			CollectionAssert.That.AtLeastOneItemSatisfies(sut.Weapons,
																										weapon => weapon.Contains("Sword"));
		}

		[TestMethod]
		public void HaveNoEmptyDefaultWeapons()
		{
			//Assert.IsFalse(sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
			//CollectionAssert.That.AllItemsNotNullorWhitespace(sut.Weapons);

			//sut.Weapons.Add(" ");

			//CollectionAssert.That.AllItemsSatisfy(sut.Weapons,
			//																									weapon => !string.IsNullOrWhiteSpace(weapon));

			

			CollectionAssert.That.All(sut.Weapons, weapon =>
			{
				StringAssert.That.NotNullOrWhiteSpace(weapon);
				Assert.IsTrue(weapon.Length > 5);
			});

		}
	}
}
