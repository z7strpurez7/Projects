namespace movieCharacterAPI.Models
{
    public class FranchiseEntity
    {
        public int FranchiseId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }


        //Navigationproperty
        //Franchise has Many movies relation
        public ICollection<MovieEntity> Movies { get; set; }
    }
}
