using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameOnTelephoneKeypadServices.Models
{
    public class Positioning
    {
        public int Index { get; set; }

        public string Number { get; set; }

        public IEnumerable<int> NextPossiblePositions { get; set; }
    }
}
