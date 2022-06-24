using ChessGameOnTelephoneKeypadServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameOnTelephoneKeypadServices.Services.Implementations
{
    public class BoardLayout : IBoardLayout
    {
        public BoardLayout(int rows, int columns, string[] values)
        {
            if (values.Length != rows * columns)
            {
                throw new ArgumentException($"Number of rows ({ rows }) and columns ({ columns }) specified incompatible with number of values ({ values.Length }) provided.");
            }

            Configuration = new (int, string)[rows, columns];
            int counter = 0;

            foreach (var row in Enumerable.Range(0, rows))
            {
                foreach (var column in Enumerable.Range(0, columns))
                {
                    Configuration[row, column] = new(counter, values[counter]);

                    counter++;
                }
            }
        }

        public (int, string)[,] Configuration { get; set; }
    }
}
