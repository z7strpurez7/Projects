using Microsoft.AspNetCore.Mvc;
using movieCharacterAPI.Models;
using System.Collections.Generic;
using static movieCharacterAPI.Dto.MovieDtos;
using static movieCharacterAPI.Dto.MovieDtos.MovieTitlesDto;


namespace movieCharacterAPI.Dto
{
    public class CharacterDtos
    {
        ///DTOs
        public record CharacterDto(int CharacterId, string FullName, string? Alias, string Gender, string Picture, List<MovieDto> Movies);
        public record PostCharacterDto( string FullName, string? Alias, string Gender, string Picture, List<AddMovieDto>? moviesDto);
        public record UpdateCharacterCommand(int Id, PostCharacterDto CharacterDto);
        public record CharacterTitle(int CharacterId, string FullName);
        public record AddMovieDto(string Title, string Genre, string ReleaseYear, string Director, string Picture, string Trailer, string FranchiseName, string FranchiseDescription);
    }
}
