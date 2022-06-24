using ChessGameOnTelephoneKeypadServices.Enums;
using ChessGameOnTelephoneKeypadServices.Models;
using ChessGameOnTelephoneKeypadServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameOnTelephoneKeypadServices.Services.Implementations
{
    public class KnightMovement : BaseMovement, IMovement
    {
        private readonly (int, int)[] _paths = { (2, 1), (2, -1), (-2, 1), (-2, -1), (1, 2), (1, -2), (-1, 2), (-1, -2) };
        private const StandardChessPiece Piece = StandardChessPiece.Knight;

        public KnightMovement(IBoardLayout layout, string[] prohibitedValues) : base(layout, prohibitedValues) { }

        public IEnumerable<Positioning> NextPossiblePositions()
        {
            return NextPossiblePositions(Piece, _paths);
        }
    }
}
