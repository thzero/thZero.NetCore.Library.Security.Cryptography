/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Security.Cryptography
Copyright (C) 2016-2018 thZero.com

<development [at] thzero [dot] com>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 * ------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace thZero.Utilities
{
	public static class StringCrypto
	{
		//http://alchemise.net/wordpress/?p=40
		public static string RandomAlphanumeric(int length)
		{
			const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(alphanumericCharacters, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static string RandomAlphanumeric(this string input, int length)
		{
			return RandomAlphanumeric(length);
		}

		public static string RandomAlphanumericFast(int length)
		{
			const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return RandomAlphanumericFast(length, alphanumericCharacters);
		}

		public static string RandomAlphanumericFast(this string input, int length)
		{
			return RandomAlphanumericFast(length);
		}

		public static string RandomAlphanumericFast(int length, IEnumerable<char> characterSet)
		{
			Enforce.AgainstNull(() => characterSet);

			if (length < 0)
				throw new ArgumentException("length must not be negative", "length");
			if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
				throw new ArgumentException("length is too big", "length");
			var characterArray = characterSet.Distinct().ToArray();
			if (characterArray.Length == 0)
				throw new ArgumentException("characterSet must not be empty", "characterSet");

			var bytes = new byte[length * 8];
			new RNGCryptoServiceProvider().GetBytes(bytes);
			var result = new char[length];
			ulong value = 0;
			for (int i = 0; i < length; i++)
			{
				value = BitConverter.ToUInt64(bytes, i * 8);
				result[i] = characterArray[value % (uint)characterArray.Length];
			}
			return new string(result);
		}

		public static string RandomAlphanumericFast(this string input, int length, IEnumerable<char> characterSet)
		{
			return RandomAlphanumericFast(length, characterSet);
		}
	}
}