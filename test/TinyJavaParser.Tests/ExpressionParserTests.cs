// Copyright (c) Bruno Brant. All rights reserved.

using System;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class ExpressionParserTests
	{
		[Theory]
		[InlineData("\"AuthenticatorActivity\"", typeof(StringLiteral))]
		[InlineData("200L", typeof(LongLiteral))]
		[InlineData("2 * 60 * 1000", typeof(InfixExpression))]
		[InlineData("AuthenticatorActivity.class.getName() + \".ScanBarcode\"", typeof(InfixExpression))]
		[InlineData("{ };", typeof(ArrayInitialization))]
		[InlineData("DependencyInjector.getAccountDb()", typeof(MethodCall))]
		[InlineData("(Toolbar) findViewById(R.id.authenticator_toolbar)", typeof(CastExpression))]
		[InlineData("darkModeEnabled ? R.style.AuthenticatorTheme_NoActionBar_Dark : R.style.AuthenticatorTheme_NoActionBar", typeof(TernaryExpression))]
		public void Parse_GivenValidInput_ExpectedOutput(string validInput, Type expectedOutput)
		{
			var actual = JavaGrammar.Expression.Parse(validInput);

			Assert.Equal(expectedOutput, actual.GetType());
		}
	}
}
