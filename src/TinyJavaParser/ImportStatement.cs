// Copyright (c) Bruno Brant. All rights reserved.

namespace TinyJavaParser
{
	/// <summary>
	/// A Java import statement.
	/// </summary>
	public class ImportStatement
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ImportStatement"/> class.
		/// </summary>
		/// <param name="packageName">
		///     The package that this statement is importing.
		/// </param>
		public ImportStatement(PackageName packageName)
		{
			PackageName = packageName;
		}

		/// <summary>
		/// Gets the package name of this import statement.
		/// </summary>
		public PackageName PackageName { get; }

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"import {PackageName};";
		}
	}
}
