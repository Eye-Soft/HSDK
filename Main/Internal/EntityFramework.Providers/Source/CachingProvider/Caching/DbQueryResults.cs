﻿// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Caching
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Represents cached query results.
	/// </summary>
	[Serializable]
	public class DbQueryResults
	{
		/// <summary>
		/// Initializes a new instance of the DbQueryResults class.
		/// </summary>
		public DbQueryResults()
		{
			this.Rows = new List<object[]>();
			this.ColumnNames = new List<string>();
		}

		/// <summary>
		/// Gets the collection of column names.
		/// </summary>
		public IList<string> ColumnNames { get; private set; }

		/// <summary>
		/// Gets the collection of rows.
		/// </summary>
		/// <value>The collection of rows.</value>
		public IList<object[]> Rows { get; private set; }
	}
}