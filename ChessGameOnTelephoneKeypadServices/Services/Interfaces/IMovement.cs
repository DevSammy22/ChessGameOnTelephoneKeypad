using ChessGameOnTelephoneKeypadServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameOnTelephoneKeypadServices.Services.Interfaces
{
    public interface IMovement
    {
        IEnumerable<Positioning> NextPossiblePositions();
    }
}
