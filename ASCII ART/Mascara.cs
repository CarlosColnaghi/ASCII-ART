using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_ART
{
    class Mascara
    {
        public static int[,] getLaplace(int tipo = 0)
        {
            int[,] mascara = null;
            switch (tipo)
            {
                case 0:
                    mascara = new int[,]
                    {
                        {0, -1, 0},
                        {-1, 4, -1},
                        {0, -1, 0},
                    };
                    break;
                case 1:
                    mascara = new int[,]
                    {
                        {-1, -1, -1},
                        {-1, 8, -1},
                        {-1, -1, -1},
                    };
                    break;
            }
            return mascara;
        }
    }
}
