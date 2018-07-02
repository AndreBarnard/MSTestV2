using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Linq;

namespace GameEngine.Test
{
	[TestClass]
	public class PlayerCharacterShould
	{
		[TestMethod]
		public void BeInexperiencedWhenNew()
		{
			var sut = new PlayerCharacter();

			Assert.IsTrue(sut.IsNoob);
		}

		[TestMethod]

		public void NotHaveNickNameByDefault()
		{
			var sut = new PlayerCharacter();

			Assert.IsNull(sut.NickName);
		}

		[TestMethod]

		public void StartWithDefaultHealth()
		{
			var sut = new PlayerCharacter();

			Assert.AreEqual(100, sut.Health);
		}

		[TestMethod]
		public void TakeDamage()
		{
			var sut = new PlayerCharacter();

			sut.TakeDamage(1);

			Assert.AreEqual(99, sut.Health);
		}


		[TestMethod]
		public void TakeDamage_NotEqual()
		{
			var sut = new PlayerCharacter();

			sut.TakeDamage(1);

			Assert.AreNotEqual(100, sut.Health);
		}

		[TestMethod]
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

			CollectionAssert.AreEqual(expectedWeapons,sut.Weapons);
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
