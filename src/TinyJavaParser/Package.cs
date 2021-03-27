// Copyright (c) Bruno Brant. All rights reserved.

namespace TinyJavaParser
{
	/// <summary>
	/// A Java package statement.
	/// </summary>
	public class Package
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Package"/> class.
		/// </summary>
		/// <param name="packageName">The name of the package.</param>
		public Package(PackageName packageName)
		{
			PackageName = packageName ?? throw new System.ArgumentNullException(nameof(packageName));
		}

		/// <summary>
		/// Gets the name of the package declared by this statement.
		/// </summary>
		public PackageName PackageName { get; }
	}
}
