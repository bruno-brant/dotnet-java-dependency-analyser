// Copyright (c) Bruno Brant. All rights reserved.

using System;
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
			from terminator in Parse.Char(';').Token()
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
		/// Parses a string literal.
		/// </summary>
		public static readonly Parser<StringLiteral> StringLiteral =
			from leftQuote in Parse.Char('"')
			from characters in Parse.CharExcept('"').Many() // TODO: Deal with \" (escaped quote)
			from rightQuote in Parse.Char('"')
			select new StringLiteral(string.Concat(characters));

		/// <summary>
		/// Parses a number literal.
		/// </summary>
		public static readonly Parser<LongLiteral> LongLiteral =
			from digits in Parse.Digit.AtLeastOnce()
			let number = string.Concat(digits)
			let value = long.Parse(number, CultureInfo.CurrentCulture)
			from longSuffix in Parse.Char('L')
			select new LongLiteral(value);

		///// <summary>
		///// Parses a single InfixOperator.
		///// </summary>
		////public static readonly Parser<InfixOperator> InfixOperator = EnumExtensions.CreateParser<InfixOperator>();

		///// <summary>
		///// Parses a infix expression.
		///// </summary>
		//public static readonly Parser<InfixExpression> InfixExpression =
		////from leftExpressions in Expression
		////from @operator in InfixOperator
		////from rightExpression in Expression
		////select new InfixExpression(leftExpressions, @operator, rightExpression);

		/// <summary>
		/// A comma-delimited sequence of <see cref="ILiteral"/>s.
		/// </summary>
		public static readonly Parser<IEnumerable<ILiteral>> LiteralExpressionList =
			from head in LiteralExpression
			from tail in (from delimiter in Parse.Char(',').Token()
						  from literal in LiteralExpression.Token()
						  select literal).Many()
			select new List<ILiteral>() { head }.Concat(tail.ToList());

		/// <summary>
		/// Parses any literal expression.
		/// </summary>
		public static Parser<ILiteral> LiteralExpression => OneOf<ILiteral>(StringLiteral, IntegerLiteral, LongLiteral);

		/// <summary>
		/// Parses an array initialization expression.
		/// </summary>
		public static readonly Parser<ArrayInitialization> ArrayInitialization =
			from openCurlyBraces in Parse.Char('{').Token()
			from literals in LiteralExpressionList.Optional()
			from closeCurlyBraces in Parse.Char('}').Token()
			from end in Parse.Char(';')
			select literals.IsEmpty ? new ArrayInitialization() : new ArrayInitialization(literals.Get()); 

		/// <summary>
		/// Gets a parser for Java expressions.
		/// </summary>
		/// <seealso cref="IExpression"/>.
		public static Parser<IExpression> Expression => OneOf<IExpression>(
				StringLiteral, LongLiteral, ArrayInitialization /*, InfixExpression*/);

		private static Parser<T> OneOf<T>(params Parser<T>[] parsers)
		{
			if (parsers is null)
			{
				throw new ArgumentNullException(nameof(parsers));
			}

			if (parsers.Length < 1)
			{
				throw new ArgumentException("At least one parser should be informed");
			}

			return parsers.Aggregate((x, y) => x.Or(y));
		}
	}
}
