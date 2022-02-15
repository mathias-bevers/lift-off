using System;

namespace MBevers
{
	/// <summary>
	///     The way C# handles some functions of the enum data type are inconvenient. This extension class make the syntax a
	///     bit easier to use.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		///     Get a random element of the passed in <see cref="Enum" />. This is based on the <see cref="System.Random" />
		///     algorithm, this returns a pseudo random number. Based on a seed, by default this is time based.
		/// </summary>
		/// <param name="enum">The <see cref="Enum" /> of which a random element is picked.</param>
		/// <exception cref="ArgumentException">Thrown when the passed in type is not of type <see cref="Enum" />.</exception>
		public static T Random<T>(this T @enum) where T : Enum
		{
			int count = Count(@enum);
			int randomNumber = new Random().Next(0, count);
			var randomizedEnum = (T)Enum.ToObject(typeof(T), randomNumber);

			return randomizedEnum;
		}

		/// <summary>
		///     Get a random element of the passed in <see cref="Enum" />. This is based on the <see cref="System.Random" />
		///     algorithm, this returns a pseudo random number. Based on a seed, by default this is time based.
		/// </summary>
		/// <param name="enum">The <see cref="Enum" /> of which a random element is picked.</param>
		/// <param name="seed">The seed the random number is picked from.</param>
		/// <exception cref="ArgumentException">Thrown when the passed in type is not of type <see cref="Enum" />.</exception>
		public static T Random<T>(this T @enum, int seed) where T : Enum
		{
			int count = Count(@enum);
			int randomNumber = new Random(seed).Next(0, count);
			var randomizedEnum = (T)Enum.ToObject(typeof(T), randomNumber);

			return randomizedEnum;
		}

		/// <summary>
		///     Get the element count of the passed in <see cref="Enum" />.
		/// </summary>
		/// <param name="enum">The <see cref="Enum" /> that needs to be counted.</param>
		/// <exception cref="ArgumentException">Thrown when the passed in type is not of type <see cref="Enum" />.</exception>
		public static int Count<T>(this T @enum) where T : Enum => Enum.GetNames(typeof(T)).Length;
	}
}