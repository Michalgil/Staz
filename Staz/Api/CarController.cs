using Microsoft.AspNetCore.Mvc;
using Staz.DAL;
using Staz.Model;
using Staz.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staz.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ICarRepository carRepository;

        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CarJson car)
        {
            try
            {
                await carRepository.Create(car);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var cars = await carRepository.GetAll();
                return Ok(cars);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Car car)
        {
            try
            {
                await carRepository.Update(car);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await carRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}