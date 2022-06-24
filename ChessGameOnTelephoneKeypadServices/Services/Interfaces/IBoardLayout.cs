using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameOnTelephoneKeypadServices.Services.Interfaces
{
    public interface IBoardLayout
    {
        (int, string)[,] Configuration { get; set; }
    }
}
