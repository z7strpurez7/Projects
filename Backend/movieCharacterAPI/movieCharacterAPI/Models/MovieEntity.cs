namespace movieCharacterAPI.Models { 

    public class MovieEntity
    {
        //Properties
        public int MovieId { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string ReleaseYear { get; set; }
        public required string Director { get; set; }
        public required string Picture { get; set; }
        public required string Trailer { get; set; }
        public int? FranchiseId { get; set; }
        //Navigationproperty
        //Movie has many Characters and one Franchise
        public ICollection<CharacterEntity>? Character { get; set; }
        public FranchiseEntity? Franchise { get; set; }
    }
}
