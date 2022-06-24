using ChessGameOnTelephoneKeypadServices.Enums;
using ChessGameOnTelephoneKeypadServices.Models;
using System.Collections.Generic;

namespace ChessGameOnTelephoneKeypadServices.Services.Implementations
{
    public interface IBaseMovement
    {
        IEnumerable<Positioning> NextPossiblePositions(StandardChessPiece piece, (int, int)[] paths, bool recursive = false);
    }
}