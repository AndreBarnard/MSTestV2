using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Test
{
	public static class CustomAsserts
	{

		public static void IsInRange(this Assert assert,
																	int actual,
																	int expectedMinimumValue,
																	int expectedMaximumValue)
		{
			if (actual < expectedMinimumValue || actual > expectedMaximumValue)
			{
				throw new AssertFailedException($"{actual} was not in the range {expectedMinimumValue}-{expectedMaximumValue}");
			}
		}


		public static void AllItemsNotNullorWhitespace(this CollectionAssert collectionAssert,
																										ICollection<string> collection)
		{
			foreach (var item in collection)
			{
				if (string.IsNullOrWhiteSpace(item))
				{
					throw new AssertFailedException("One or more items are null or white space");
				}
			}
		}

		public static void AllItemsSatisfy<T>(this CollectionAssert collectionAssert,
																										ICollection<T> collection,
																										Predicate<T> predicate)
		{
			foreach (var item in collection)
			{
				if (!predicate(item))
				{
					throw new AssertFailedException("All items do not satisfy predicate");
				}
			}
		}


		public static void All<T>(this CollectionAssert collectionAssert,
																									ICollection<T> collection,
																									Action<T> assert)
		{
			foreach (var item in collection)
			{
				assert(item);
			}
		}

		public static void NotNullOrWhiteSpace(this StringAssert stringAssert,
																string actual)
		{
			if (string.IsNullOrWhiteSpace(actual))
			{
				throw new AssertFailedException("Value is null or white space");
			}
		}

		public static void AtLeastOneItemSatisfies<T>(this CollectionAssert collectionAssert,
																								ICollection<T> collection,
																								Predicate<T> predicate)
		{
			foreach (var item in collection)
			{
				if (predicate(item))
				{
					return;
				}
			}

			throw new AssertFailedException("No item satisfied predicate");

		}

	}
}
