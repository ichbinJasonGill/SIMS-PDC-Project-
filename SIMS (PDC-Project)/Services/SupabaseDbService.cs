//using Supabase_Example.Models;
using System;
using System.Configuration;
using System.Threading.Tasks;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace Supabase_Example.Services
{
    public class SupabaseDbService
    {
        private readonly string _connectionString;

        public SupabaseDbService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        }

        public async Task<bool> InsertUpdateDelete(string query, List<NpgsqlParameter> parameters)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                // Log or handle error here
                return false;
            }
        }

        public async Task<DataTable> Select(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Optionally handle/log the error
                return null;
            }

            return dataTable;
        }

        public async Task<DataTable> Select(string query, List<NpgsqlParameter> parameters)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        if (parameters != null && parameters.Count > 0)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Optionally handle/log the error
                return null; // or return empty DataTable if you prefer
            }

            return dataTable;
        }

    }
}
