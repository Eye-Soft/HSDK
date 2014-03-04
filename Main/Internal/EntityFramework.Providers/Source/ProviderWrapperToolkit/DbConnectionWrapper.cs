﻿// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System;
	using System.Data;
	using System.Data.Common;

	/// <summary>
	/// Wrapper for <see cref="DbConnection"/> objects.
	/// </summary>
	public abstract class DbConnectionWrapper : DbConnection
	{
		private const string ConnectionStringKey = "wrappedProvider=";

		private DbConnection wrappedConnection;
		private string wrappedProviderInvariantName;

		/// <summary>
		/// Gets the time to wait while establishing a connection before terminating the attempt and generating an error.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The time (in seconds) to wait for a connection to open. The default value is determined by the
		/// specific type of connection that you are using.
		/// </returns>
		public override int ConnectionTimeout
		{
			get
			{
				return wrappedConnection.ConnectionTimeout;
			}
		}

		/// <summary>
		/// Gets the name of the current database after a connection is opened,
		/// or the database name specified in the connection string before the connection is opened.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The name of the current database or the name of the database to be used after a connection is opened.
		/// The default value is an empty string.
		/// </returns>
		public override string Database
		{
			get { return wrappedConnection.Database; }
		}

		/// <summary>
		/// Gets the name of the database server to which to connect.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The name of the database server to which to connect. The default value is an empty string.
		/// </returns>
		public override string DataSource
		{
			get { return wrappedConnection.DataSource; }
		}

		/// <summary>
		/// Gets the invariant name of the wrapped provider.
		/// </summary>
		/// <value>The invariant name of the wrapped provider.</value>
		public string WrappedProviderInvariantName
		{
			get { return wrappedProviderInvariantName; }
		}

		/// <summary>
		/// Gets or sets the wrapped connection.
		/// </summary>
		/// <value>The wrapped connection.</value>
		public DbConnection WrappedConnection
		{
			get { return wrappedConnection; }
			set { wrappedConnection = value; }
		}

		/// <summary>
		/// Gets or sets the string used to open the connection.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The connection string used to establish the initial connection.
		/// The exact contents of the connection string depend on the specific data source for this connection.
		/// The default value is an empty string.
		/// </returns>
		public override string ConnectionString
		{
			get
			{
				return ConnectionStringKey + wrappedProviderInvariantName + ";" + wrappedConnection.ConnectionString;
			}

			set
			{
				CreateConnectionFromConnectionString(value);
			}
		}

		/// <summary>
		/// Gets a string that represents the version of the server to which the object is connected.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The version of the database. The format of the string returned depends on the specific type of connection you are using.
		/// </returns>
		public override string ServerVersion
		{
			get { return wrappedConnection.ServerVersion; }
		}

		/// <summary>
		/// Gets or sets the <see cref="T:System.ComponentModel.ISite"/> of the <see cref="T:System.ComponentModel.Component"/>.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.ComponentModel.ISite"/> associated with the <see cref="T:System.ComponentModel.Component"/>,
		/// or null if the <see cref="T:System.ComponentModel.Component"/>
		/// is not encapsulated in an <see cref="T:System.ComponentModel.IContainer"/>,
		/// the <see cref="T:System.ComponentModel.Component"/> does not have an <see cref="T:System.ComponentModel.ISite"/>
		/// associated with it, or the <see cref="T:System.ComponentModel.Component"/>
		/// is removed from its <see cref="T:System.ComponentModel.IContainer"/>.
		/// </returns>
		public override System.ComponentModel.ISite Site
		{
			get { return wrappedConnection.Site; }
			set { wrappedConnection.Site = value; }
		}

		/// <summary>
		/// Gets a string that describes the state of the connection.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The state of the connection. The format of the string returned depends on the specific type of connection you are using.
		/// </returns>
		public override ConnectionState State
		{
			get { return wrappedConnection.State; }
		}

		/// <summary>
		/// Gets the name of the default wrapped provider.
		/// </summary>
		/// <returns>Name of the default wrapped provider.</returns>
		protected abstract string DefaultWrappedProviderName { get; }

		/// <summary>
		/// Enlists in the specified transaction.
		/// </summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction"/> in which to enlist.</param>
		public override void EnlistTransaction(System.Transactions.Transaction transaction)
		{
			wrappedConnection.EnlistTransaction(transaction);
		}

		/// <summary>
		/// Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection"/>
		/// using the specified string for the schema name.
		/// </summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <returns>
		/// A <see cref="T:System.Data.DataTable"/> that contains schema information.
		/// </returns>
		public override DataTable GetSchema(string collectionName)
		{
			return wrappedConnection.GetSchema(collectionName);
		}

		/// <summary>
		/// Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Data.DataTable"/> that contains schema information.
		/// </returns>
		public override DataTable GetSchema()
		{
			return wrappedConnection.GetSchema();
		}

		/// <summary>
		/// Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection"/>
		/// using the specified string for the schema name and the specified string array for the restriction values.
		/// </summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <returns>
		/// A <see cref="T:System.Data.DataTable"/> that contains schema information.
		/// </returns>
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return wrappedConnection.GetSchema(collectionName, restrictionValues);
		}

		/// <summary>
		/// Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString"/>.
		/// </summary>
		public override void Open()
		{
			wrappedConnection.Open();
		}

		/// <summary>
		/// Changes the current database for an open connection.
		/// </summary>
		/// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
		public override void ChangeDatabase(string databaseName)
		{
			wrappedConnection.ChangeDatabase(databaseName);
		}

		/// <summary>
		/// Closes the connection to the database. This is the preferred method of closing any open connection.
		/// </summary>
		/// <exception cref="T:System.Data.Common.DbException">
		/// The connection-level error that occurred while opening the connection.
		/// </exception>
		public override void Close()
		{
			wrappedConnection.Close();
		}

		/// <summary>
		/// Wraps the connection.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <param name="wrapperProviderInvariantNames">The wrapper provider invariant names.</param>
		/// <returns>Wrapped connection.</returns>
		internal static DbConnection WrapConnection(DbConnection connection, params string[] wrapperProviderInvariantNames)
		{
			foreach (var invariantName in wrapperProviderInvariantNames)
			{
				var factory = DbProviderFactories.GetFactory(invariantName);
				var connectionWrapper = factory.CreateConnection();
				var wrapper = (DbConnectionWrapper)connectionWrapper;
				wrapper.WrappedConnection = connection;
				connection = connectionWrapper;
			}

			return connection;
		}

		/// <summary>
		/// Starts a database transaction.
		/// </summary>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		/// <returns>
		/// An object representing the new transaction.
		/// </returns>
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return wrappedConnection.BeginTransaction(isolationLevel);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/>
		/// and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && wrappedConnection != null)
			{
				wrappedConnection.Dispose();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Creates and returns a <see cref="T:System.Data.Common.DbCommand"/> object associated with the current connection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Data.Common.DbCommand"/> object.
		/// </returns>
		protected override DbCommand CreateDbCommand()
		{
			return wrappedConnection.CreateCommand();
			throw new NotSupportedException();
		}

		/// <summary>
		/// Creates the connection from connection string.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		private void CreateConnectionFromConnectionString(string connectionString)
		{
			string providerInvariantName;
			string newConnectionString;

			var pos = connectionString.IndexOf(ConnectionStringKey, StringComparison.OrdinalIgnoreCase);

			if (pos == 0)
			{
				var pos2 = connectionString.IndexOf(";", pos, StringComparison.Ordinal);
				if (pos2 < 0)
				{
					throw new ArgumentException("Invalid connection string format");
				}

				providerInvariantName = connectionString.Substring(ConnectionStringKey.Length, pos2 - ConnectionStringKey.Length);
				newConnectionString = connectionString.Substring(pos2 + 1);
			}
			else
			{
				providerInvariantName = DefaultWrappedProviderName;
				newConnectionString = connectionString;
			}

			var factory = DbProviderFactories.GetFactory(providerInvariantName);
			wrappedProviderInvariantName = providerInvariantName;
			wrappedConnection = factory.CreateConnection();
			wrappedConnection.ConnectionString = newConnectionString;
		}
	}
}