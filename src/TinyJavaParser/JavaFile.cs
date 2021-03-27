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
		/// Initializes a new instance of the <see cref="JavaFile"/> class.
		/// </summary>
		/// <param name="package">the package that this file declares.</param>
		/// <param name="importList">the list of imports present in this file.</param>
		/// <param name="classDefinition">the class that is defined in this file.</param>
		public JavaFile(Package package, List<ImportStatement> importList, ClassDefinition classDefinition)
		{
			Package = package;
			ImportList = importList;
			ClassDefinition = classDefinition;
		}

		/// <summary>
		/// Gets the package that this file declares.
		/// </summary>
		public Package Package { get; }

		/// <summary>
		/// Gets the list of imports present in this file.
		/// </summary>
		public List<ImportStatement> ImportList { get; }

		/// <summary>
		/// Gets the class that is defined in this file.
		/// </summary>
		public ClassDefinition ClassDefinition { get; }
	}
}
