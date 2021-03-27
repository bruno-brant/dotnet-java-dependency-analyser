// Copyright (c) Bruno Brant. All rights reserved.

namespace TinyJavaParser
{
	/// <summary>
	/// Types of visibility.
	/// </summary>
	public enum Visibility
	{
		/// <summary>
		/// The code structure is accessible from other scopes.
		/// </summary>
		Public,

		/// <summary>
		/// The code structure is accessible only in it owning and inherited scopes.
		/// </summary>
		Protected,

		/// <summary>
		/// The code structure is acessible only to its owning scope.
		/// </summary>
		Private,
	}
}
