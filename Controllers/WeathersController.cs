using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController : ControllerBase
    {
        private readonly WeatherDbContext _context;

        public WeathersController(WeatherDbContext context)
        {
            _context = context;
        }

        // GET: api/Weathers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weather>>> Getweather()
        {
          if (_context.weather == null)
          {
              return NotFound();
          }
            return await _context.weather.ToListAsync();
        }

        // GET: api/Weathers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weather>> GetWeather(int id)
        {
          if (_context.weather == null)
          {
              return NotFound();
          }
            var weather = await _context.weather.FindAsync(id);

            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }

        // PUT: api/Weathers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeather(int id, Weather weather)
        {
            if (id != weather.Id)
            {
                return BadRequest();
            }

            _context.Entry(weather).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Weathers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weather>> PostWeather(Weather weather)
        {
          if (_context.weather == null)
          {
              return Problem("Entity set 'WeatherDbContext.weather'  is null.");
          }
            _context.weather.Add(weather);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeather", new { id = weather.Id }, weather);
        }

        // DELETE: api/Weathers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeather(int id)
        {
            if (_context.weather == null)
            {
                return NotFound();
            }
            var weather = await _context.weather.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            _context.weather.Remove(weather);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherExists(int id)
        {
            return (_context.weather?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
