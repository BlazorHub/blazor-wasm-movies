﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GenresController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (genre == null) { return NotFound(); }
            return genre;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Genre genre)
        {
            _dbContext.Add(genre);
            await _dbContext.SaveChangesAsync();
            return genre.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Genre genre)
        {
            _dbContext.Attach(genre).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}