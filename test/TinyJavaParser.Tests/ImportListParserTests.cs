// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;
using Xunit;

namespace TinyJavaParser.Tests
{
	public class ImportListParserTests
	{
		/// <summary>
		/// Gets data for Parse_WhenValidPackageName_ReturnsStructureWithCorrectName.
		/// </summary>
		/// <returns>
		/// Will return instances of arrays of strings, each containing a single, 
		/// multiline string that is a bunch of import statements.
		/// </returns>
		public static IEnumerable<object[]> ImportLists()
		{
			yield return new string[]
			{
				@"
import com.google.android.apps.authenticator.util.EmptySpaceClickableDragSortListView;
import com.google.android.apps.authenticator.util.annotations.FixWhenMinSdkVersion;
import com.google.android.apps.authenticator2.R;
import com.google.common.annotations.VisibleForTesting;
				".Trim(),
			};

			yield return new string[]
			{
				@"
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.util.Log;
import android.view.ActionMode;
import android.view.ContextMenu;
				".Trim(),
			};
		}

		[Theory]
		[MemberData(nameof(ImportLists))]
		public void Parse_WhenValidPackageName_ReturnsStructureWithCorrectName(string importList)
		{
			if (importList == null)
			{
				throw new ArgumentNullException(nameof(importList));
			}

			var expected = importList.Split(Environment.NewLine).ToList();
			var actual = JavaGrammar.ImportList.Parse(importList).Select(_ => _.ToString()).ToList();

			Assert.Equal(expected, actual);
		}
	}
}
