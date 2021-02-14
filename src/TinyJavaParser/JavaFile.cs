// Copyright (c) Bruno Brant. All rights reserved.

using System.Collections.Generic;

namespace TinyJavaParser
{
	/// <summary>
	/// Defines a java file (a compilation unit) that the parser will analyze.
	/// </summary>
	public class JavaFile
	{
		/// <summary>
		/// Gets or sets the package that this file declares.
		/// </summary>
		public Package Package { get; set; }

		/// <summary>
		/// Gets the list of imports present in this file.
		/// </summary>
		public List<Import> ImportList { get; } = new List<Import>();

		/// <summary>
		/// Gets or sets the class that is defined in this file.
		/// </summary>
		public ClassDefinition ClassDefinition { get; set; }
	}
}
