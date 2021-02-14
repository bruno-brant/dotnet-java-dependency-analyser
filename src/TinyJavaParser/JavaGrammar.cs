// Copyright (c) Bruno Brant. All rights reserved.

using System.Linq;
using Sprache;

namespace TinyJavaParser
{
	/// <summary>
	/// Parsers for our subset grammar for Java.
	/// </summary>
	public static class JavaGrammar
	{
		/// <summary>
		/// Parses a Java identifier.
		/// </summary>
		public static readonly Parser<string> Identifier = Sprache.Parse.LetterOrDigit.Many().Text();

		/// <summary>
		/// Parses a PackageName statement.
		/// </summary>
		public static readonly Parser<PackageName> PackageName =
			from packageKeyword in Sprache.Parse.String("package").Once()
			from space in Sprache.Parse.WhiteSpace.Many()
			from packageHead in Identifier
			from packageTail in (from delimiter in Sprache.Parse.Char('.').Once()
								 from identifier in Identifier
								 select identifier).Many()
			from terminator in Sprache.Parse.Char(';')
			select new PackageName(new[] { packageHead }.Concat(packageTail).ToList());
	}
}
