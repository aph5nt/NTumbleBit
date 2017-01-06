using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTumbleBit.Common
{

	public class ConfigurationException : Exception
	{
		public ConfigurationException(string message) : base(message)
		{

		}
	}

    public class TextFileConfiguration
    {

		public static Dictionary<string, string> Parse(string data)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			var lines = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			int lineCount = -1;
			foreach(var l in lines)
			{
				lineCount++;
				var line = l.Trim();
				if(line.StartsWith("#"))
					continue;
				var split = line.Split('=');
				if(split.Length == 0)
					continue;
				if(split.Length == 1)
					throw new FormatException("Line " + lineCount + ": No value are set");

				var key = split[0];
				if(result.ContainsKey(key))
					throw new FormatException("Line " + lineCount + ": Duplicate key " + key);
				var value = String.Join("=", split.Skip(1).ToArray());
				result.Add(key, value);
			}
			return result;
		}

		public static String CreateDefaultConfiguration(Network network)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("#rpc.url=http://localhost:"+network.RPCPort+ "/");
			builder.AppendLine("#rpc.user=bitcoinuser");
			builder.AppendLine("#rpc.password=bitcoinpassword");
			builder.AppendLine("#rpc.cookiefile=yourbitcoinfolder/.cookie");
			return builder.ToString();
		}

		public static String CreateClientDefaultConfiguration(Network network)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("#rpc.url=http://localhost:" + network.RPCPort + "/");
			builder.AppendLine("#rpc.user=bitcoinuser");
			builder.AppendLine("#rpc.password=bitcoinpassword");
			builder.AppendLine("#rpc.cookiefile=yourbitcoinfolder/.cookie");
			builder.AppendLine("#tumbler.server=http://localhost:5000");
			builder.AppendLine("#outputwallet.extpubkey=xpub661MyMwAqRbcFMJZyE2opu5nJd6QgyMewDXwxzTsEDuXdaB2HzV1rGEi6DyAXbtHS7H8C9o4c5g6hLsMinmfiVTFYV5TogCvJ7yhQoB4vVa");
			builder.AppendLine("#outputwallet.keypath=0/0");

			return builder.ToString();
		}

		
		
	}
}
