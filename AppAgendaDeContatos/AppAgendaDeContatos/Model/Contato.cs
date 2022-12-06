using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAgendaDeContatos.Model
{
    public class Contato
    {
        string _nome;
        string _numero;
        string _email;
        string _tpTelefone;
        string _tituloT;
        string _empresa;

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string email 
        {
            get => _email;
            set
            {
                _email = value;
            }
        }
        public string empresa { 
            get => _empresa;
            set
            {
                _empresa = value;
            }
        }
        public string tituloT { 
            get => _tituloT; 
            set
            {
                _tituloT = value;
            }
        }
        public string nome
        {
            get => _nome;
            set
            {
                if (value == null)
                    _nome = numero;
                else
                _nome = value;
            }
        }
        public string tpTelefone 
        { 
            get => _tpTelefone; 
            set
            {
                _tpTelefone = value;
            }
        }
        public string numero
        {
            get => _numero;
            set
            {
                if (value == null)
                    throw new Exception("Preencha o número");

                _numero = value;
            }
        }
    }
}
