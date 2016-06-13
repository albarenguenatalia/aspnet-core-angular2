using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.ViewModels
{
    public class GameViewModel
    {
        public int Id;
        [Required]
        public string Player1Name;
        [Required]
        public string Player2Name;
        public string Settings;
        public DateTime StartedAt;
    }
}
