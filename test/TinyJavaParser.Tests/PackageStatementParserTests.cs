// Copyright (c) Bruno Brant. All rights reserved.

using System;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class PackageStatementParserTests
	{
		[Fact]
		public void Parse_WhenCodeNull_Throws()
		{
			Assert.Throws<ArgumentNullException>(() => JavaGrammar.PackageStatement.Parse(null));
		}

		[Theory]
		[InlineData("tinyJavaParser")]
		[InlineData("com.google.android.apps.authenticator")]
		[InlineData("com.google.android.apps.authenticator.enroll2sv.wizard")]
		public void Parse_WhenValidPackage_ReturnsName(string packageName)
		{
			var packageExpresison = $"package {packageName};";

			var actual = JavaGrammar.PackageStatement.Parse(packageExpresison);

			Assert.Equal(packageName, string.Join('.', actual.PackageName.Identifiers));
		}
	}
}
