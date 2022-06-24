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
    public class BaseMovement : IBaseMovement
    {
        private readonly IBoardLayout _layout;
        private readonly int _layoutRows;
        private readonly int _layoutColumns;
        private readonly string[] _prohibitedValues;

        public BaseMovement(IBoardLayout layout, string[] prohibitedValues)
        {
            _layout = layout;
            _layoutRows = _layout.Configuration.GetLength(0);
            _layoutColumns = _layout.Configuration.GetLength(1);
            _prohibitedValues = prohibitedValues;
        }


        public IEnumerable<Positioning> NextPossiblePositions(StandardChessPiece piece, (int, int)[] paths, bool recursive = false)
        {
            var nextPositions = new List<Positioning>();

            foreach (var r in Enumerable.Range(0, _layoutRows))
            {
                foreach (var c in Enumerable.Range(0, _layoutColumns))
                {
                    Positioning nextPosition = new Positioning
                    {
                        Index = _layout.Configuration[r, c].Item1,
                        Number = _layout.Configuration[r, c].Item2
                    };

                    var nextPossiblePositions = recursive ? NextPositionByPresentPositionRecursive(paths, (r, c)).ToList() : NextPositionsByPresentPosition(paths, (r, c)).ToList();

                    if (piece == StandardChessPiece.Pawn && r == 0)
                    {
                        nextPossiblePositions.AddRange(NextPositionsByPresentPosition(paths, (r + 1, c)));
                    }

                    nextPosition.NextPossiblePositions = nextPossiblePositions;
                    nextPositions.Add(nextPosition);
                }
            }

            return nextPositions;
        }

        private IEnumerable<int> NextPositionsByPresentPosition((int, int)[] paths, (int, int) currentPosition)
        {
            var nextValues = new List<int>();

            foreach (var (pathRow, pathColumn) in paths)
            {
                var nextRow = currentPosition.Item1 + pathRow;
                var nextColumn = currentPosition.Item2 + pathColumn;

                if (nextRow >= 0 && nextRow < _layoutRows
                    && nextColumn >= 0 && nextColumn < _layoutColumns
                    && _prohibitedValues.All(vi => vi != _layout.Configuration[nextRow, nextColumn].Item2))
                    nextValues.Add(_layout.Configuration[nextRow, nextColumn].Item1);
            }

            return nextValues;
        }

        private IEnumerable<int> NextPositionByPresentPositionRecursive((int, int)[] paths, (int, int) currentPosition)
        {
            var nextValues = new List<int>();

            foreach (var (pathRow, pathColumn) in paths)
            {
                var nextRow = currentPosition.Item1 + pathRow;
                var nextColumn = currentPosition.Item2 + pathColumn;

                while (nextRow >= 0 && nextRow < _layoutRows && nextColumn >= 0 && nextColumn < _layoutColumns)
                {
                    if (_prohibitedValues.All(vi => vi != _layout.Configuration[nextRow, nextColumn].Item2))
                        nextValues.Add(_layout.Configuration[nextRow, nextColumn].Item1);

                    nextRow = nextRow + pathRow;
                    nextColumn = nextColumn + pathColumn;
                }
            }

            return nextValues;
        }
    }
}

