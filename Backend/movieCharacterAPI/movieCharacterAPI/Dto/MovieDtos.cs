using Microsoft.AspNetCore.Mvc;
using movieCharacterAPI.Models;

using System.Collections.Generic;

using static movieCharacterAPI.Dto.CharacterDtos;
using static movieCharacterAPI.Dto.CharacterDtos.CharacterTitle;


namespace movieCharacterAPI.Dto
{
    public class MovieDtos
    {
        ///DTOs
        public record GetMovieByIdQuery([FromRoute] int Id);
        public record UpdateMovieCommand(int Id, UpdateMovieDto Dto);
        public record MovieTitlesDto(int MovieId, string Title);
        public record MovieDto(int MovieId, string Title,string Genre,string ReleaseYear,string Director,string Picture,string Trailer,List<CharacterTitle> Characters, string FranchiseName);
        public record PostMovieDto(string MovieTitle, string Genre, string ReleaseYear, string Director, string Picture, string Trailer, int? franchiseId, List<CharacterEntity>? Character);
        public record UpdateMovieDto(string MovieTitle, string Genre, string ReleaseYear, string Director, string Picture, string Trailer, int? franchiseId);

    }
}
