using DesafioDev.API.Contexto;
using DesafioDev.API.Contexto.DesafioDev.API.UoW;
using DesafioDev.API.Interfaces.DesafioDev.API.UoW;
using DesafioDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DesafioDev.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            try
            {
                return await _context.Usuarios.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SalvarNovoUsuario(Usuario usuario)
        {
            try
            {
                usuario.SenhaHash = PasswordService.HashSenha(usuario.SenhaHash);

                _context.Usuarios.Add(usuario);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}