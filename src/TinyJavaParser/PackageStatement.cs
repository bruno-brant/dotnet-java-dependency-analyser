using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyJavaParser
{
	/// <summary>
	/// Declares the package name for all subsequent structures in the compilation unit (file).
	/// </summary>
	public class PackageStatement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PackageStatement"/> class.
		/// </summary>
		/// <param name="packageName">
		/// The name of the package that was declared.
		/// </param>
		public PackageStatement(PackageName packageName)
		{
			PackageName = packageName;
		}

		/// <summary>
		/// Gets the name of the package that was declared.
		/// </summary>
		public PackageName PackageName { get; }
	}
}
