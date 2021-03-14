// Copyright (c) Bruno Brant. All rights reserved.

using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class ClassDefinitionParserTests
	{
		[Fact]
		public void Parse_AnnotatedClassWithExtends_CorrectParameters()
		{
			var code = @"
@FixWhenMinSdkVersion(11)
public class AuthenticatorActivity extends TestableActivity
".Trim();

			var actual = JavaGrammar.ClassDefinition.Parse(code);

			Assert.Equal("FixWhenMinSdkVersion", actual.Annotation.Name);
			Assert.Equal(11, actual.Annotation.Arguments[0].Value);
			Assert.Equal(Visibility.Public, actual.Visibility);
			Assert.Equal("AuthenticatorActivity", actual.Name);
			Assert.Equal("TestableActivity", actual.BaseClass);
		}
	}
}
