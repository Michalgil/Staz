using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Staz.Data;
using Staz.Model;
using Staz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staz.DAL
{
    public class CarRepository : ICarRepository, IDisposable
    {
        private Context context;

        public CarRepository(Context context)
        {
            this.context = context;
        }

        public async Task Create(CarJson car)
        {
            Car c = new Car
            {
                Brand = car.Brand,
                Type = car.Type,
                Price = car.Price
            };
          
            context.Cars.Add(c);
            await context.SaveChangesAsync();

        }
        public async Task Delete(int id)
        {
            var deleteCar = context.Cars.Where(c => c.Id == id).FirstOrDefault();
            context.Cars.Remove(deleteCar);
            await context.SaveChangesAsync();
        }
        public async Task Update(Car car)
        {
            var updateCar = context.Cars.Where(c => c.Id == car.Id).FirstOrDefault();
            updateCar.Brand = car.Brand;
            updateCar.Type = car.Type;
            updateCar.Price = car.Price;
            context.Cars.Update(updateCar);
            await context.SaveChangesAsync();

        }
        public async Task<IEnumerable<Car>> GetAll()
        {
            var cars = await context.Cars.ToListAsync();
            return cars;
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
