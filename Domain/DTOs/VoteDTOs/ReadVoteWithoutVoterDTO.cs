using Domain.DTOs.MovieDTOs;
using Domain.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.VoteDTOs
{
    public class ReadVoteWithoutVoterDTO
    {
        public virtual ReadMovieReferencelessDTO Movie { get; set; } = default!;
        public int Rating { get; set; }
    }
}
