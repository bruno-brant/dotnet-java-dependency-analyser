// Copyright (c) Bruno Brant. All rights reserved.

namespace TinyJavaParser
{
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
			return Value.ToString();
		}
	}
}
