using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Yamaha.Rota.Data.Repositories
{
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        protected readonly IConfiguration _configuracao;

        public DbSession(IConfiguration configuration)
        {
            _configuracao = configuration;
            _id = Guid.NewGuid();
            Connection = new SqlConnection(_configuracao.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }

        public SqlConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        public void Dispose() => Connection?.Dispose();
    }
}
