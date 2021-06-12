// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Sprache;

namespace TinyJavaParser
{
	/// <summary>
	/// Extensions for Enum types.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Creates a sprache parser for an <see cref="Enum"/>.
		/// </summary>
		/// <typeparam name="T">The enum type.</typeparam>
		/// <returns>The sprache <see cref="Parser{T}"/> that parsers the enum.</returns>
		public static Parser<T> CreateParser<T>()
			where T : struct, Enum
		{
			return
				GetFields<T>()
				.Select(field => new { value = (T)field.GetRawConstantValue()!, description = field.GetDescription() })
				.OrderByDescending(_ => _.description.Length) // must match first longer strings to avoid mistakes
				.Select(_ => Parse.String(_.description).Token().Return(_.value))
				.Aggregate((x, y) => x.Or(y));
		}

		/// <summary>
		///     Gets value of the <see cref="DescriptionAttribute"/> that annotates the enum value.
		/// </summary>
		/// <param name="fieldInfo">
		///     A FieldInfo metadata of a enum member.
		/// </param>
		/// <returns>
		///     A string that is the value of the <see cref="DescriptionAttribute"/> that annotates the field.
		/// </returns>
		public static string GetDescription(this FieldInfo fieldInfo)
		{
			return fieldInfo.GetCustomAttributes<DescriptionAttribute>().SingleOrDefault()?.Description
				?? throw new Exception($"Field {fieldInfo.Name} doesn't have a {nameof(DescriptionAttribute)}.");
		}

		/// <summary>
		///     Gets the <see cref="FieldInfo"/> for all enum members.
		/// </summary>
		/// <typeparam name="T">
		///     The type of the enum.
		/// </typeparam>
		/// <returns>
		///     Enumerates each field of the enum.
		/// </returns>
		public static IEnumerable<FieldInfo> GetFields<T>()
			where T : struct, Enum
		{
			var typeOfT = typeof(T);

			return Enum.GetNames<T>().Select(name => typeOfT.GetField(name)!);
		}
	}
}
