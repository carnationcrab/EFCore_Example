using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotnetBakery.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BakersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BakersController(ApplicationContext context) {
            _context = context;
        }

        // The `[HttpGet]` attribute defines this method as our `GET /api/bakers` endpoint
        // This function returns a `IEnumerable<Baker>` object,
        // which is .NET's fancy way of saying "A list of baker objects"
        [HttpGet]
        public IEnumerable<Baker> GetAll() 
        {
        // Look ma, no SQL queries!
        return _context.Bakers;
        }

        // GET /api/bakers/:id
        [HttpGet("{id}")]
        public ActionResult<Baker> GetById(int id) {
            Baker baker =  _context.Bakers
                .SingleOrDefault(baker => baker.id == id);
            
            // Return a `404 Not Found` if the baker doesn't exist
            if(baker is null) {
                return NotFound();
            }

            return baker;
        }

        [HttpPost]
        public Baker Post(Baker baker) 
        {
            _context.Add(baker);
            _context.SaveChanges();

            return baker;
        }
    }
}
