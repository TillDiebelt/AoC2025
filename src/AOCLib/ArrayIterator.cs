using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCLib
{
    public static class ArrayIterator
    {
        public static IEnumerable<(int x, int y)> GetAllCoords<T>(this T[,] array)
        {
            for (int y = 0; y <= array.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= array.GetUpperBound(1); x++)
                {
                    yield return (x, y);
                }
            }
        }

        public static IEnumerable<(int x, int y)> GetAllCoords<T>(this T[][] array)
        {
            for (int y = 0; y <= array.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= array.GetUpperBound(1); x++)
                {
                    yield return (x, y);
                }
            }
        }
    }
}
