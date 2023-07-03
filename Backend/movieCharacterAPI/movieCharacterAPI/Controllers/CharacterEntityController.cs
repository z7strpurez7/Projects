﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieCharacterAPI.Context;
using movieCharacterAPI.Dto;
using movieCharacterAPI.Models;
using System.Net;
using static movieCharacterAPI.Dto.CharacterDtos;
using static movieCharacterAPI.Dto.MovieDtos;

namespace movieCharacterAPI.Controllers
{
    [Route("[controller]")]
    public class CharacterEntityController : ControllerBase
    {
        private readonly MovieContext _context;
        public CharacterEntityController(MovieContext context)
        {
            _context = context;
        }

        // Prints out All characters, all the movies the act in + every character in the movie 
        [HttpGet("GetAllCharacterRelatedDetails")]
        public async Task<IActionResult> GetAllCharacterDetails()
        {
            List<CharacterDto> characters = null;
            try
            {
                characters = await _context.Characters.Include(e => e.Movies).Select(e => new CharacterDto(
                    e.CharacterId,
                    e.Name,
                    e.Alias,
                    e.Gender,
                    e.Picture,
                    e.Movies.Select(e => new MovieDto(e.MovieId,
                    e.Title,
                    e.Genre,
                    e.ReleaseYear,
                    e.Director,
                    e.Picture,
                    e.Trailer,
                    e.Character.Select(e => new CharacterTitle(e.CharacterId, e.Name)).ToList(),
                    e.Franchise.Name)).ToList())).ToListAsync();

                if (characters == null) { return NotFound(); }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(characters);
        }
        ///Initializes a variable as a list of the Dto
        ///Retrieves data from Charactertable from context
        ///maps the data to characterDto
        ///Returns a list of CharacterDTO,
        ///If something goes wrong, returns bad Request
        ///if Characters table is empty returns Not Found
     

        //Prints out all Character names with Id
        [HttpGet("CharacterTitles")]
        public async Task<IActionResult> GetAllCharacterTitles()
        {
            List<CharacterTitle> characters = null;
            try
            {
                characters = await _context.Characters.Select(e => new CharacterTitle(
                e.CharacterId, e.Name)).ToListAsync();
                if (characters == null) { return NotFound(); }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(characters);
        }
        //Retrieves data from CharacterTable and maps it to CharacterTitle
        //in order to show just titles and id
        //Returns a list of CharacterTitle
        ///If something goes wrong, returns bad Request
        ///if Characters list is empty returns notfound



        //Prints out character by Id, prints out details aswell(but only characterTitles)
        [HttpGet("Character/{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            CharacterDto? result = null;
            try
            {
                result = await _context.Characters.Where(e => e.CharacterId == id).Include(e => e.Movies).Select(e => new CharacterDto(
                    e.CharacterId,
                    e.Name,
                    e.Alias,
                    e.Gender,
                    e.Picture,
                   e.Movies.Select(e => new MovieDto(e.MovieId,
                    e.Title,
                    e.Genre,
                    e.ReleaseYear,
                    e.Director,
                    e.Picture,
                    e.Trailer,
                    e.Character.Select(e => new CharacterTitle(e.CharacterId, e.Name)).ToList(),
                    e.Franchise.Name)).ToList())).FirstOrDefaultAsync();
                if (result == null) { return NotFound(); }
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(result);
        }
        /// gets Param id from request
        /// Creates a var and gets the data from specified id
        /// returns notfound if empty
        /// returns ok with the Dto


        //Add a regular CharacterEntity
        [HttpPost]
        public async Task<IActionResult> AddCharacter(PostCharacterDto characterDto)
        {
            if (characterDto is null) { return NoContent(); }

            CharacterEntity character = null;
            try {
                character = new CharacterEntity
                {
                    Name = characterDto.FullName,
                    Alias = characterDto.Alias,
                    Gender = characterDto.Gender,
                    Picture = characterDto.Picture
                };
            
                await _context.Characters.AddAsync(character);
                await _context.SaveChangesAsync();
            
            }catch (Exception ex) { }
            return new ObjectResult(character.CharacterId) { StatusCode = (int)HttpStatusCode.Created};
        }
         //Takes a parameter characterDto with data from input
         //returns nocontent if characterDto is empty
         //Takes the values from characterDto into character
         //ads the data to Character and saves db changes
         //returns objectResult with the the id and sets the statuscode to
         //"Created" which returns 201 and indicates that the character was successfully created



        //Edit a Character and its movies
             [HttpPut]
        public async Task<IActionResult> UpdateCharacter([FromBody] UpdateCharacterCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var character = await _context.Characters.FirstOrDefaultAsync(e => e.CharacterId == command.Id);
            try
            {
                character.Name = command.CharacterDto.FullName;
                character.Alias = command.CharacterDto.Alias;
                character.Gender = command.CharacterDto.Gender;
                character.Picture = command.CharacterDto.Picture;
                character.Movies = new List<MovieEntity>();

                await _context.SaveChangesAsync();
            }
            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }
            return new ObjectResult(character.CharacterId) { StatusCode = (int)HttpStatusCode.Accepted };
        }
        /// <summary>
        /// takes in parameter  UpdateCharacterCommand that contains characterDto and id 
        /// finds the character with the Id specified from user
        /// Changes the values from the request body
        ///  returns "Accepted" which indicates that the character update was accepted

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterById(int id)
        {
            try
            {
                var character = await _context.Characters.FindAsync(id);
                if(character == null) { return NotFound();}

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

             
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //Identifies and deletes the Character with the given Id
        //returns not found if not found
        //Returns OK()
        //
    }
}


