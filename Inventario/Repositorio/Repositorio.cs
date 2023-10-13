﻿using Inventario.Datos;
using Inventario.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }
        public async Task Crear(T entidad)
        {
            await DbSet.AddAsync(entidad);
            await Grabar();

        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(System.Linq.Expressions.Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = DbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(System.Linq.Expressions.Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = DbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }

        public async Task Remover(T entidad)
        {
            DbSet.Remove(entidad);
            await Grabar();
        }
    }
}
