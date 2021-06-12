// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Linq;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class ExpressionParserTests
	{
		[Fact]
		public void ArrayInitialization_NoParameters_ReturnsExpected()
		{
			var actual = JavaGrammar.ArrayInitialization.Parse("{ };");

			Assert.Empty(actual.Literals);
		}

		[Fact]
		public void ArrayInitialization_OneParameter_ReturnsExpected()
		{
			var actual = JavaGrammar.ArrayInitialization.Parse("{ 1 };");

			Assert.Single(actual.Literals);
			Assert.IsType<IntegerLiteral>(actual.Literals.ElementAt(0));
		}

		[Theory]
		[InlineData("\"AuthenticatorActivity\"", typeof(StringLiteral))]
		[InlineData("200L", typeof(LongLiteral))]
		[InlineData("{ };", typeof(ArrayInitialization))]
		[InlineData("DependencyInjector.getAccountDb()", typeof(MethodCall))]
		[InlineData("(Toolbar) findViewById(R.id.authenticator_toolbar)", typeof(CastExpression))]
		[InlineData("darkModeEnabled ? R.style.AuthenticatorTheme_NoActionBar_Dark : R.style.AuthenticatorTheme_NoActionBar", typeof(TernaryExpression))]
		[InlineData("2 * 60 * 1000", typeof(InfixExpression))]
		[InlineData("AuthenticatorActivity.class.getName() + \".ScanBarcode\"", typeof(InfixExpression))]
		public void Parse_GivenValidInput_ExpectedOutput(string validInput, Type expectedOutput)
		{
			var actual = JavaGrammar.Expression.Parse(validInput);

			Assert.Equal(expectedOutput, actual.GetType());
		}
	}
}
