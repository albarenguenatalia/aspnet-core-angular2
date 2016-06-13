using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.ViewModels
{
    public class RoundViewModel
    {
        public int Id;
        [Required]
        public int RoundNo;
        [Required]
        public int GameId;
        [Required]
        public string Move1;
        [Required]
        public string Move2;
    }
}
