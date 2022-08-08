using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Applications.GenreOperations.Commands.CreateGenres;
using WebAPI.Applications.GenreOperations.Commands.DeleteGenres;
using WebAPI.Applications.GenreOperations.Commands.UpdateGenres;
using WebAPI.Applications.GenreOperations.Queries.GetGenreDetail;
using WebAPI.Applications.GenreOperations.Queries.GetGenres;
using WebAPI.DBOperations;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult GetBooks()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            query.GenreId = id;
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
            
        }

        [HttpPost]
        public ActionResult addGenres([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand query = new CreateGenreCommand(_context, _mapper);
            query.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult updateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            command.GenreID = id;
            command.model = updateGenre;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult deleteGenre(int id)
        {
            DeleteGenreCommand query = new DeleteGenreCommand(_context);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            query.GenreId = id;
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }
    }
}