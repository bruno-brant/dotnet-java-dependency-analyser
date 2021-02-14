// Copyright (c) Bruno Brant. All rights reserved.

using System;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class PackageNameParserTests
	{
		[Fact]
		public void Parse_WhenCodeNull_Throws()
		{
			Assert.Throws<ArgumentNullException>(() => JavaGrammar.PackageName.Parse(null));
		}

		[Theory]
		[InlineData("tinyJavaParser")]
		[InlineData("com.google.android.apps.authenticator")]
		[InlineData("com.google.android.apps.authenticator.enroll2sv.wizard")]
		public void Parse_WhenValidPackage_ReturnsName(string packageName)
		{
			var packageExpresison = $"package {packageName};";

			var actual = JavaGrammar.PackageName.Parse(packageExpresison);

			Assert.Equal(packageName, string.Join('.', actual.Identifiers));
		}
	}
}
