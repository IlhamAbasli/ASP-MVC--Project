﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface ISubscriberRepository
    {
        Task Create(Subscriber subscriber);
        Task Delete(Subscriber subscriber);
        Task<List<Subscriber>> GetAll();
        Task<Subscriber> GetById(int id);
    }
}
