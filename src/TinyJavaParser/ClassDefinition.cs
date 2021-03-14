// Copyright (c) Bruno Brant. All rights reserved.

namespace TinyJavaParser
{
	/// <summary>
	/// A Java class definition.
	/// </summary>
	public class ClassDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDefinition"/> class.
		/// </summary>
		/// <param name="visibility">The visibility of the class.</param>
		/// <param name="name">The name of the class.</param>
		/// <param name="baseClass">The base class of this class.</param>
		/// <param name="annotation">Annotations for this class.</param>
		public ClassDefinition(Visibility visibility, string name, string? baseClass = null, Annotation? annotation = null)
		{
			Visibility = visibility;
			Name = name;
			BaseClass = baseClass;
			Annotation = annotation;
		}

		/// <summary>
		/// Gets the visibility of the class.
		/// </summary>
		public Visibility Visibility { get; }

		/// <summary>
		/// Gets the identifier that is the name of the class.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets the class that this inherits from.
		/// </summary>
		public string? BaseClass { get; }

		/// <summary>
		/// Gets annotations for this class.
		/// </summary>
		public Annotation? Annotation { get;  }
	}
}
