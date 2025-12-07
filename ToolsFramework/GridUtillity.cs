namespace ToolsFramework
{
    public static class GridUtillity
    {

        /// <summary>
        /// Returns a transposed copy of the given 2D array (rows <-> columns).
        /// For an array of size [rows, cols], the result is [cols, rows].
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="grid">The source 2D array.</param>
        /// <returns>A new 2D array where result[r, c] = grid[c, r].</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="grid"/> is null.</exception>
        public static T[,] Transpose<T>(T[,] grid)
        {
            if (grid is null) throw new ArgumentNullException(nameof(grid));

            int dim0 = grid.GetLength(0); // first dimension
            int dim1 = grid.GetLength(1); // second dimension

            var result = new T[dim1, dim0];

            // Copy with swapped indices
            for (int i = 0; i < dim0; i++)
            {
                for (int j = 0; j < dim1; j++)
                {
                    // Note: swap i and j in destination
                    result[j, i] = grid[i, j];
                }
            }

            return result;
        }
    }
}