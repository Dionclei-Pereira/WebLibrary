﻿using Microsoft.EntityFrameworkCore;
using WebLibrary.Data;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Exceptions;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class UserService : IUserService {

        private readonly LibraryContext _context;
        public UserService(LibraryContext libraryContext) {
            _context = libraryContext;
        }

        public async Task<UserDTO> Insert(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.ToDTO();
        }

        public async Task<User> GetUserByEmailNoDTO(string email) {
            var user = await _context.Users.Where(u => u.Email == email).AsNoTracking().FirstOrDefaultAsync() ?? throw new UserException("User not found");
            return user;
        }

        public async Task<UserDTO> GetUserByEmail(string email) {
            var user = await _context.Users.Where(u => u.Email == email).AsNoTracking().FirstOrDefaultAsync() ?? throw new UserException("User not found"); ;
            return user.ToDTO();
        }

        public async Task<List<LoanDTOWithoutUser>> GetUserLoansByEmail(string email) {
            var user = await _context.Users.AsNoTracking().Where(u => u.Email == email).Include(u => u.Loans).ThenInclude(l => l.Book).FirstOrDefaultAsync() ?? throw new UserException("User not found"); ;
            return user.Loans.Select(l => l.ToDTOWithoutUser()).ToList();
        
        }

        public async Task<List<UserDTO>> GetUsersDTO() {
            return await _context.Users.AsNoTracking().Select(User => User.ToDTO()).ToListAsync();
        }

        public async Task<double> AddPenalty(string email, double amount) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) {
                user.Penalty += amount;
                await _context.SaveChangesAsync();
                return user.Penalty;
            }
            throw new UserException("User not found");
        }

        public async Task<double> SetPenalty(string email, double amount) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) {
                user.Penalty = amount;
                await _context.SaveChangesAsync();
                return user.Penalty;
            }
            throw new UserException("User not found");
        }

        public async Task<double> ResetPenalty(string email) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) {
                user.Penalty = 0;
                await _context.SaveChangesAsync();
            }
            throw new UserException("User not found");
        }
    }
}
