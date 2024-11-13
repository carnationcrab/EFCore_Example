using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotnetBakery.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreadsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BreadsController(ApplicationContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Bread> GetBreads() 
        {
            return _context.Breads
                .Include(bread => bread.bakedBy);
        }

        [HttpGet("{id}")]
        public ActionResult<Bread> GetById(int id) {
            Bread bread =  _context.Breads
                .SingleOrDefault(bread => bread.id == id);
            
            if(bread is null) {
                return NotFound();
            }

            return bread;
        }

        [HttpPost]
        public Bread Post(Bread bread) 
        {
            // Tell the DB context about our new bread object
            _context.Add(bread);
            // ...and save the bread object to the database
            _context.SaveChanges();

            // Respond back with the created bread object
            return bread;
        }

        // /api/put/:id
        [HttpPut("{id}")]
        public Bread Put(int id, Bread bread)
        {
            bread.id = id;
            _context.Update(bread);
            _context.SaveChanges();

            return bread;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Bread bread = _context.Breads.Find(id);

            _context.Breads.Remove(bread);

            _context.SaveChanges();
        }
    }


}
