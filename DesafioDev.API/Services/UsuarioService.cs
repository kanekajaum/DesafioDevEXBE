using DesafioDev.API.Contexto;
using DesafioDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DesafioDev.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}