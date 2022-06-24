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
    public class QueenMovement: BaseMovement, IMovement
    {
        private readonly (int, int)[] _paths = { (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1), (0, 1) };
        private const StandardChessPiece Piece = StandardChessPiece.Queen;

        public QueenMovement(IBoardLayout layout, string[] prohibitedValues) : base(layout, prohibitedValues) { }

        public IEnumerable<Positioning> NextPossiblePositions()
        {
            return NextPossiblePositions(Piece, _paths, true);
        }
    }
}
