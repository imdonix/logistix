using System;
using System.Linq;
using System.Text;

public static class Utils
{

	public static string CreateURLEndcoded(string url, params string[] args)
    {
		if (args.Length % 2 != 0)
			throw new Exception("args must be even");

		StringBuilder builder = new StringBuilder(url + '?');
		for (int i = 0; i < args.Length; i += 2) 
		{
			builder.Append(args[i]);
			builder.Append('=');
			builder.Append(args[i + 1]);
			builder.Append('&');
		}
		return builder.Remove(builder.Length - 1, 1).ToString();
	}

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
