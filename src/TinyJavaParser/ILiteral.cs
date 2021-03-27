// Copyright (c) Bruno Brant. All rights reserved.

using System.Globalization;

#pragma warning disable SA1402 // File may only contain a single type

namespace TinyJavaParser
{
	/// <summary>
	/// A literal, that is, a value that is explicitely in code (also called a constant).
	/// </summary>
	public interface ILiteral : IExpression
	{
		/// <summary>
		/// Gets the value of this literal.
		/// </summary>
		object Value { get; }
	}

	/// <summary>
	/// A type of literal that is an integer.
	/// </summary>
	public class IntegerLiteral : ILiteral
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IntegerLiteral"/> class.
		/// </summary>
		/// <param name="value">The value of this integer literal.</param>
		public IntegerLiteral(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value of the literal.
		/// </summary>
		public int Value { get; }

		/// <inheritdoc/>
		object ILiteral.Value => Value;

		/// <inheritdoc/>
		public override string ToString()
		{
			return Value.ToString(CultureInfo.CurrentCulture);
		}
	}

	/// <summary>
	/// A type of literal that is an long integer.
	/// </summary>
	public class LongLiteral : ILiteral
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LongLiteral"/> class.
		/// </summary>
		/// <param name="value">The value of this integer literal.</param>
		public LongLiteral(long value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value of the literal.
		/// </summary>
		public long Value { get; }

		/// <inheritdoc/>
		object ILiteral.Value => Value;

		/// <inheritdoc/>
		public override string ToString()
		{
			return Value.ToString(CultureInfo.CurrentCulture) + "L";
		}
	}

	/// <summary>
	/// A type of literal that is a string.
	/// </summary>
	public class StringLiteral : ILiteral
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringLiteral"/> class.
		/// </summary>
		/// <param name="value">The value of the literal.</param>
		public StringLiteral(string value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value of the literal.
		/// </summary>
		public string Value { get; }

		/// <inheritdoc/>
		object ILiteral.Value => Value;

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"\"{Value}\"";
		}
	}
}
