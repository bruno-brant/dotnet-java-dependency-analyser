using System.Collections.Generic;
using System.Linq;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class InfixOperatorParser
	{
		public static IEnumerable<object[]> Data()
		{
			return EnumExtensions
				.GetFields<InfixOperator>()
				.Select(fi => new object[] { (InfixOperator)fi.GetRawConstantValue(), fi.GetDescription() });
		}

		[Theory]
		[MemberData(nameof(Data))]
		public void InfixOperatorWorks(InfixOperator value, string @operator)
		{
			var parser = EnumExtensions.CreateParser<InfixOperator>();

			Assert.Equal(value, parser.Parse(@operator));
		}
	}
}
