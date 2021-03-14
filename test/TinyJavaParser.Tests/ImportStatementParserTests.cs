// Copyright (c) Bruno Brant. All rights reserved.

using System;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class ImportStatementParserTests
	{
		[Theory]
		[InlineData("import android.annotation.TargetApi;")]
		[InlineData("import android.app.Activity;")]
		public void Parse_WhenValidPackageName_ReturnsStructureWithCorrectName(string importStatement)
		{
			var actual = JavaGrammar.ImportStatement.Parse(importStatement);

			Assert.Equal(importStatement, actual.ToString());
		}
	}
}
