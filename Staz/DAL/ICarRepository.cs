using Microsoft.AspNetCore.Mvc;
using Staz.Model;
using Staz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staz.DAL
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task Create(CarJson car);
        Task Delete(int id);
        Task Update(Car car);

    }
}
