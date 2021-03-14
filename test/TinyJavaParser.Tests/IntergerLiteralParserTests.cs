// Copyright (c) Bruno Brant. All rights reserved.

using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class IntergerLiteralParserTests
	{
		[Theory]
		[InlineData(11)]
		public void MyTheory(int value)
		{
			var actual = JavaGrammar.IntegerLiteral.Parse(value.ToString());

			Assert.Equal(value, actual.Value);
		}
	}
}
