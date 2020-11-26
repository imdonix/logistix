using System;
using System.Linq;
using System.Text;

public static class Utils
{

	public static string ToReadableNumber(int number)
	{
		string Reverse(string str) { return new string(str.Reverse().ToArray()); }

		StringBuilder tmp = new StringBuilder();

		int c = 0;
		foreach (char chr in Reverse(number.ToString()))
		{
			if (c % 3 == 0 && c > 0)
				tmp.Append('.');
			tmp.Append(chr);
			c++;
		}

		return Reverse(tmp.ToString());
	}

}
