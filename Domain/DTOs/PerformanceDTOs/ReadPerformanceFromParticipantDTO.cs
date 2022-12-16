using Domain.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PerformanceDTOs
{
    public class ReadPerformanceFromParticipantDTO
    {
        public string? CharacterName { get; set; }
        public virtual ReadMovieReferencelessDTO? Movie { get; set; }
    }
}
