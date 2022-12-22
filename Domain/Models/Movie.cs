using Domain.Models.Abstract;

namespace Domain.Models
{
    public class Movie : IdentifiableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; } = null;
        public int? Duration { get; set; } = 0;
        public int? Quantity_Votes { get; private set; } = 0;
        public double? Rating { get; private set; } = 0;
        public virtual List<Vote> Votes { get; set; } = new List<Vote>();
        public virtual List<Genre> Genres { get; set; } = new List<Genre>();
        public virtual List<Performance> Cast { get; set; } = new List<Performance>();
        public virtual List<Direction> Direction { get; set; } = new List<Direction>();

        // Method to increment Quantity_Votes and Rating
        public void AddVote(Vote vote)
        {
            Votes.Add(vote);
            Quantity_Votes++;
            Rating = (Rating * (Quantity_Votes - 1) + vote.Rating) / Quantity_Votes;
        }
    }
}
