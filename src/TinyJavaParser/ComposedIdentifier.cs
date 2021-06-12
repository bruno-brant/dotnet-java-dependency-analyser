// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyJavaParser
{
	/// <summary>
	/// The composed identifier is a list of identifiers divided by a dot ('.').
	/// </summary>
	/// <remarks>
	/// This kind of construct can be used to identify packages, classes, members, etc.
	/// </remarks>
	public class ComposedIdentifier
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ComposedIdentifier"/> class.
		/// </summary>
		/// <param name="identifiers">The sequence of identifiers.</param>
		public ComposedIdentifier(params string[] identifiers)
		{
			if (identifiers is null)
			{
				throw new ArgumentNullException(nameof(identifiers));
			}

			Identifiers = identifiers.ToList();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ComposedIdentifier"/> class.
		/// </summary>
		/// <param name="identifiers">The sequence of identifiers.</param>
		public ComposedIdentifier(IEnumerable<string> identifiers)
		{
			Identifiers = identifiers?.ToList() ?? throw new ArgumentNullException(nameof(identifiers));
		}

		/// <summary>
		/// Gets the sequence of identifiers.
		/// </summary>
		public List<string> Identifiers { get; }

		/// <inheritdoc/>
		public override string ToString()
		{
			return string.Join('.', Identifiers);
		}
	}
}
