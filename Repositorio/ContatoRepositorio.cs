﻿using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;
        public ContatoRepositorio(BancoContext bancoContext) 
        { 
            this._context = bancoContext;
        }

        public ContatoModel ListarPorId(int id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodosRegistros()
        {
            return _context.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            // Gravar no banco de dados
            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null) throw new System.Exception("Houve um erro ao atualizar o contato");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contatos.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null) throw new System.Exception("Houve um erro ao deletar o contato");

            _context.Contatos.Remove(contatoDB);
            _context.SaveChanges();
            return true;
        }
    }
}
