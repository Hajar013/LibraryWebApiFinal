﻿using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.LibrarianServices
{
    public interface ILibrarianService
    {
        IQueryable<LibrarianDto> FindAll();
        IQueryable<LibrarianDto> FindByCondition(int id);
        void Create(LibrarianDto dto);
        void Update(LibrarianDto dto);
        void Delete(LibrarianDto dto);
    }
}