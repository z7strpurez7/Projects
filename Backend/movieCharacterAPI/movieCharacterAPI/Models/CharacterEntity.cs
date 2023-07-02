using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace movieCharacterAPI.Models
{
    public class CharacterEntity
    {
        //Id and properties
        public int CharacterId { get; set; }
        public required string Name { get; set; }
        public string? Alias { get;  set; }
        public required string Gender{ get; set; }
        public required string Picture { get; set; }
        //set relation a Character has one or more movies
        public ICollection<MovieEntity> Movies { get; set; }
    }
}
