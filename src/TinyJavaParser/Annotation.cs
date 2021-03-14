// Copyright (c) Bruno Brant. All rights reserved.

using System.Collections.Generic;

namespace TinyJavaParser
{
	/// <summary>
	/// An annotation is a Java language element that can adorn any declaration.
	/// </summary>
	public class Annotation
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Annotation"/> class.
		/// </summary>
		/// <param name="name">The name of the annotation.</param>
		/// <param name="arguments">The list of arguments of this annotation.</param>
		public Annotation(string name, List<ILiteral> arguments)
		{
			Name = name;
			Arguments = arguments;
		}

		/// <summary>
		/// Gets the identifier of this annotation.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets the arguments that where passed to the instance of this annotation.
		/// </summary>
		public List<ILiteral> Arguments { get; }

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"@{Name}({string.Join(", ", Arguments)})";
		}
	}
}
