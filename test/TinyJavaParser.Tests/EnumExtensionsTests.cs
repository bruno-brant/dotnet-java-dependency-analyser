// Copyright (c) Bruno Brant. All rights reserved.

using System.ComponentModel;
using System.Linq;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class EnumExtensionsTests
	{
		public enum Foo
		{
			[Description("Abc")]
			Bar,

			[Description("Def")]
			Baz,
		}

		[Fact]
		public void GetFields_ReturnAllMembers()
		{
			Assert.Equal(new[] { "Abc", "Def" }, EnumExtensions.GetFields<Foo>().Select(EnumExtensions.GetDescription));
		}

		[Fact]
		public void CreateParser_ValidEnum_NotNull()
		{
			Assert.NotNull(EnumExtensions.CreateParser<Foo>());
		}

		[Theory]
		[InlineData(Foo.Bar, "Abc")]
		[InlineData(Foo.Baz, "Def")]
		public void CreateParser_ParseValidString_ExpectedValue(Foo value, string description)
		{
			var parser = EnumExtensions.CreateParser<Foo>();

			var actual = parser.Parse(description);

			Assert.Equal(value, actual);
		}

		[Theory]
		[InlineData("zzz")]
		public void CreateParser_ParseInvalidString_Throws(string description)
		{
			var parser = EnumExtensions.CreateParser<Foo>();

			Assert.Throws<ParseException>(() => parser.Parse(description));
		}
	}
}
