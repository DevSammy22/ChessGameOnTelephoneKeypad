using ChessGameOnTelephoneKeypadServices.Enums;
using ChessGameOnTelephoneKeypadServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameOnTelephoneKeypadServices.Services.Implementations
{
    public class Counter
    {
        private readonly IBoardLayout _boardLayout;
        private readonly IEnumerable<StandardChessPiece> _chessPieces;
        private IMovement _movement;

        public Counter(IBoardLayout boardLayout, IEnumerable<StandardChessPiece> chessPieces)
        {
            _boardLayout = boardLayout;
            _chessPieces = chessPieces;
        }

        public List<(StandardChessPiece, double)> Count(string[] cannotStartWith, string[] cannotContain, int lengthOfPhoneNumber)
        {
            var countByChessPiece = new List<(StandardChessPiece, double)>();

            var rows = _boardLayout.Configuration.GetLength(0);
            var columns = _boardLayout.Configuration.GetLength(1);
            var totalPositions = _boardLayout.Configuration.Length;

            foreach (var chessPiece in _chessPieces)
            {
                _movement = InstantiateMoves(chessPiece, cannotContain);
                var currentRow = new double[totalPositions];

                foreach (var r in Enumerable.Range(0, rows))
                {
                    foreach (var c in Enumerable.Range(0, columns))
                    {
                        if (cannotStartWith.All(vi => vi != _boardLayout.Configuration[r, c].Item2))
                            currentRow[_boardLayout.Configuration[r, c].Item1] = 1;
                    }
                }

                foreach (var _ in Enumerable.Range(1, lengthOfPhoneNumber - 1))
                {
                    var previousRow = currentRow;
                    currentRow = new double[totalPositions];

                    var nextPositions = _movement.NextPossiblePositions();

                    foreach (var position in nextPositions)
                    {
                        foreach (var nextPosition in position.NextPossiblePositions)
                        {
                            currentRow[nextPosition] += previousRow[position.Index];
                        }
                    }
                }

                countByChessPiece.Add((chessPiece, currentRow.Sum()));
            }

            return countByChessPiece;
        }

        private IMovement InstantiateMoves(StandardChessPiece chessPiece, string[] cannotContain)
        {
            return chessPiece switch
            {
                StandardChessPiece.King => new KingMovement(_boardLayout, cannotContain),
                StandardChessPiece.Queen => new QueenMovement(_boardLayout, cannotContain),
                StandardChessPiece.Bishop => new BishopMovement(_boardLayout, cannotContain),
                StandardChessPiece.Knight => new KnightMovement(_boardLayout, cannotContain),
                StandardChessPiece.Rook => new RookMovement(_boardLayout, cannotContain),
                StandardChessPiece.Pawn => new PawnMovement(_boardLayout, cannotContain),
                _ => throw new ArgumentOutOfRangeException(nameof(chessPiece), chessPiece, null),
            };
        }
    }
}
