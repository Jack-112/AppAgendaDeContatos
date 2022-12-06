using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppAgendaDeContatos.Model;
using SQLite;
namespace AppAgendaDeContatos.Helper
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDataBaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Contato>().Wait();
        }
         public Task<int> Insert(Contato c)
        {
            return _conn.InsertAsync(c);
        }

        public Task<List<Contato>> Update(Contato c)
        {
            string sql = "UPDATE Contato SET nome = ?, numero = ?, tituloT = ?, empresa = ?, email = ?, tpTelefone = ? WHERE id = ?";
            return _conn.QueryAsync<Contato>(sql, c.nome, c.numero, c.tituloT, c.empresa, c.email, c.tpTelefone, c.id);
        }

        public Task<List<Contato>> Getall()
        {
            return _conn.Table<Contato>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Contato>().DeleteAsync(i => i.id == id);
        }
        public Task<List<Contato>> Search(string c)
        {
            string sql = "SELECT * FROM Contato WHERE nome LIKE '%" + c + "%' or numero LIKE '%" + c + "%'";
            return _conn.QueryAsync<Contato>(sql);
        }
    }
}
