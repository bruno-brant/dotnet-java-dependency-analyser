// Copyright (c) Bruno Brant. All rights reserved.

using System.Collections.Generic;

namespace TinyJavaParser
{
	/// <summary>
	/// A package name in a Java sense.
	/// </summary>
	public class PackageName
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PackageName"/> class.
		/// </summary>
		/// <param name="identifers">The list of idenfiers that compose the name.</param>
		public PackageName(ComposedIdentifier identifers)
		{
			ComposedIdentifier = identifers;
		}

		/// <summary>
		/// Gets the list of identifiers that is the package name.
		/// </summary>
		public ComposedIdentifier ComposedIdentifier { get; }

		/// <inheritdoc/>
		public override string ToString()
		{
			return string.Join('.', ComposedIdentifier);
		}
	}
}
