// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Linq;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class AnnotationParserTests
	{
		[Theory]
		[InlineData("@Number(11)", new object[] { 11 })]
		public void Parse_WhenAnnotationHasParameters_CorrectParameters(string annotation, object[] parameters)
		{
			var actual = JavaGrammar.Annotation.Parse(annotation);

			Assert.Equal(parameters, actual.Arguments.Select(_=>_.Value));
		}
	}
}
