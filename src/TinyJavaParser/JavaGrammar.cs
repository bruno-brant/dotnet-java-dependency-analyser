// Copyright (c) Bruno Brant. All rights reserved.

using System.Collections.Generic;
using System.Globalization;
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
		/// Parses an identifier.
		/// </summary>
		public static readonly Parser<string> Identifier = Parse.LetterOrDigit.Many().Text();

		/// <summary>
		/// Parses a <see cref="ComposedIdentifer"/>.
		/// </summary>
		public static readonly Parser<ComposedIdentifier> ComposedIdentifer =
			from head in Identifier
			from tail in (from delimiter in Parse.Char('.').Once()
						  from identifier in Identifier
						  select identifier).Many()
			let identifierSequence = new[] { head }.Concat(tail)
			select new ComposedIdentifier(identifierSequence);

		/// <summary>
		/// Parsers a Java package name.
		/// </summary>
		public static readonly Parser<PackageName> PackageName =
			from composedIdentifier in ComposedIdentifer
			select new PackageName(composedIdentifier);

		/// <summary>
		/// Parses a package statement.
		/// </summary>
		public static readonly Parser<PackageStatement> PackageStatement =
			from packageKeyword in Parse.String("package").Once()
			from space in Parse.WhiteSpace.Many()
			from packageName in PackageName
			from terminator in Parse.Char(';')
			select new PackageStatement(packageName);

		/// <summary>
		/// Parses an import statement.
		/// </summary>
		public static readonly Parser<ImportStatement> ImportStatement =
			from importKeyword in Parse.String("import").Token()
			from packageName in PackageName.Token()
			from delimiter in Parse.Char(';').Token()
			select new ImportStatement(packageName);

		/// <summary>
		/// Parses a block of imports.
		/// </summary>
		public static readonly Parser<List<ImportStatement>> ImportList =
			from statements in ImportStatement.Many().Token()
			select statements.ToList();

		/// <summary>
		/// Parses a integer (sequence of numbers).
		/// </summary>
		public static readonly Parser<IntegerLiteral> IntegerLiteral =
			from digits in Parse.Digit.AtLeastOnce()
			let number = string.Concat(digits)
			let value = int.Parse(number, CultureInfo.CurrentCulture)
			select new IntegerLiteral(value);

		/// <summary>
		/// Parses a Java annotation.
		/// </summary>
		public static readonly Parser<Annotation> Annotation =
			from at in Parse.Char('@').Once()
			from identifier in Identifier.Token()
			from startList in Parse.Char('(').Token()
			from literal in IntegerLiteral.Token().Optional()
			from endList in Parse.Char(')').Token()
			let arguments = literal.IsDefined
				? new List<ILiteral> { literal.Get() }
				: new List<ILiteral>()
			select new Annotation(identifier, arguments);

		/// <summary>
		/// Parses a class definition.
		/// </summary>
		public static readonly Parser<ClassDefinition> ClassDefinition =
			from annotation in Annotation.Token()
			from visibility in Parse.String("public").Token()
			from classKeyword in Parse.String("class").Token()
			from className in Identifier.Token()
			from extendsKeyword in Parse.String("extends").Token()
			from baseClassName in Identifier.Token()
			select new ClassDefinition(Visibility.Public, className, baseClassName, annotation);

		/// <summary>
		/// Parsers a Java Expression.
		/// </summary>
		/// <seealso cref="IExpression"/>.
		public static readonly Parser<IExpression> Expression;
	}
}
