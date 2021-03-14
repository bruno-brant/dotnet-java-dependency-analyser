// Copyright (c) Bruno Brant. All rights reserved.

namespace TinyJavaParser
{
	/// <summary>
	/// A literal, that is, a value that is explicitely in code (also called a constant).
	/// </summary>
	public interface ILiteral
	{
		/// <summary>
		/// Gets the value of this literal.
		/// </summary>
		object Value { get; }
	}
}
