﻿using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataBase;

namespace FastkartAPI.DataBase.Repositories
{
    public class UserModelRepository : IUserModelRepository
    {
        private readonly MyApplicationContext _context;

        public UserModelRepository(MyApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(UserModel user, string password)
        {
            var result = UserModel.CreateModel(user, password);

            await _context.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<UserModel>> GetAll()
        {
            var result = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<UserModel> GetById(Guid id)
        {
            var result = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<UserModel> GetByEmail(string email) 
        {
            var result = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            return result;
        }

        public Task Update(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
