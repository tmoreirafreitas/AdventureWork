﻿using AdventureWork.Domain.Entities;
using AdventureWork.Domain.Repositories;
using AdventureWork.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AdventureWork.Infra.Data
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly IDbCommand _command;
        private readonly IDatabaseContext _context;
        private bool disposedValue;

        public Repository(IDatabaseContext context)
        {
            _context = context;
            _command = context.Connection.CreateCommand();
        }

        protected IDataReader ExecuteReader(string query, Dictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                DefineQuery(query, parameters, commandType);
                return _command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected int ExecuteCommand(string query, Dictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                DefineQuery(query, parameters, commandType);
                return Convert.ToInt32(_command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected Task<IDataReader> ExecuteReaderAsync(string query, Dictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                DefineQuery(query, parameters, commandType);
                return Task.FromResult(_command.ExecuteReader());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected Task<int> ExecuteCommandAsync(string query, Dictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                DefineQuery(query, parameters, commandType);
                return Task.FromResult(Convert.ToInt32(_command.ExecuteScalar()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DefineQuery(string query, Dictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("É preciso definir uma query para execução da instrução SQL.");

            _command.CommandText = query;
            _command.CommandType = commandType;

            if (_context.Transaction != null)
                _command.Transaction = _context.Transaction;

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    var parameter = _command.CreateParameter();
                    parameter.ParameterName = item.Key;
                    parameter.Value = item.Value;
                    _command.Parameters.Add(parameter);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_command != null)
                    {
                        _command.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
