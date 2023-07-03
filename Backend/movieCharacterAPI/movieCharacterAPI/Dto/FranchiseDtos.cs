using static movieCharacterAPI.Dto.MovieDtos;

namespace movieCharacterAPI.Dto
{
    public class FranchiseDtos
    {
        ///DTOs
        public record FranchiseDto(int FranchiseId, string Name, string Description, List<MovieTitlesDto> Movies);
        public record PostFranchiseDto(string Name, string Description);
        
        public record UpdateFranchiseCommand(int Id, PostFranchiseDto FranchiseDto);
    }
}
