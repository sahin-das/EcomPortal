﻿using EcomPortal.Data;
using EcomPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcomPortal.Repositories
{
    public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IGenericRepository<User>
    {
        private readonly ApplicationDbContext _context = context;
    }
}
