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
		public PackageName(List<string> identifers)
		{
			Identifiers = identifers;
		}

		/// <summary>
		/// Gets the list of identifiers that is the package name.
		/// </summary>
		public List<string> Identifiers { get; } = new List<string>();

		/// <inheritdoc/>
		public override string ToString()
		{
			return string.Join('.', Identifiers);
		}
	}
}
