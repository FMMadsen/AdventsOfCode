using System.Text;

namespace AdventOfCode2024Solutions.Day04
{
    public class Matrix
    {
        public string[] Rows { get; private set; }
        public string[] Columns { get; private set; }
        public string[] DiagonalsDown { get; private set; }
        public string[] DiagonalsUp { get; private set; }

        public Matrix(string[] rows)
        {
            Rows = rows;
            Columns = RowsToColumns(rows);
            DiagonalsDown = RowsToDiagonalsDown(rows);
            DiagonalsUp = RowsToDiagonalsUp(rows);
        }

        //public List<MatrixPoint> FindDiagonalDownMatches(string searchString)
        //{
        //    List<MatrixPoint>? matrixPoints = null;

        //    Regex regex = new Regex(searchString);

        //    for (int d = 0; d < DiagonalsDown.Length; d++)
        //    {

        //        var matches = regex.Matches(DiagonalsDown[d]);
        //        if (matches.Count > 0)
        //        {
        //            if(matrixPoints == null)
        //                matrixPoints = new List<MatrixPoint>();

        //            foreach ( Match match in matches)
        //            {
        //                var matchDiagonal = d;
        //                var matchIndex = match.Index;
        //                var matrixPoint = new MatrixPoint();
        //                matrixPoint.X = 


        //            }
        //        }

        //    }

        //}

        private string[] RowsToColumns(string[] rows)
        {
            var numOfColumns = rows.Max(x => x.Length);
            var numOfRows = rows.Length;

            string[] columns = new string[numOfColumns];

            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < numOfColumns; c++)
            {
                for (int r = 0; r < numOfRows; r++)
                {
                    if (c < rows[r].Length)
                        sb.Append(rows[r][c]);
                }
                columns[c] = sb.ToString();
                sb.Clear();
            }

            return columns;
        }

        private string[] RowsToDiagonalsDown(string[] rows)
        {
            var numOfColumns = rows[0].Length;
            var numOfRows = rows.Length;
            var numOfDiagonals = numOfColumns + numOfRows - 1;

            string[] diagonals = new string[numOfDiagonals];

            StringBuilder sb = new StringBuilder();

            //Turning into diagonals DOWN, require to initiate an array from first all columns and first row. Then from the following rows.
            var columnInitiator = 0;
            var rowInitiator = 0;

            for (int d = 0; d < numOfDiagonals; d++)
            {
                if (d < numOfColumns)
                {
                    for (int c = columnInitiator, r = rowInitiator; c < numOfColumns && r < numOfRows; c++, r++)
                    {
                        sb.Append(rows[r][c]);
                    }
                    columnInitiator++;
                    diagonals[d] = sb.ToString();
                    sb.Clear();
                }
                if (d == numOfColumns)
                {
                    rowInitiator = 1;
                }
                if (d >= numOfColumns && rowInitiator < numOfRows)
                {
                    columnInitiator = 0;

                    for (int c = columnInitiator, r = rowInitiator; c < numOfColumns && r < numOfRows; c++, r++)
                    {
                        sb.Append(rows[r][c]);
                    }
                    rowInitiator++;
                    diagonals[d] = sb.ToString();
                    sb.Clear();
                }
            }

            return diagonals;
        }

        private string[] RowsToDiagonalsUp(string[] rows)
        {
            var numOfColumns = rows[0].Length;
            var numOfRows = rows.Length;
            var numOfDiagonals = numOfColumns + numOfRows - 1;

            string[] diagonals = new string[numOfDiagonals];

            StringBuilder sb = new StringBuilder();

            //Turning into diagonals UP, require to initiate an array from first all columns and first row. Then from the following rows.
            var columnInitiator = 0;
            var rowInitiator = 0;

            for (int d = 0; d < numOfDiagonals; d++)
            {
                if (d < numOfColumns)
                {
                    for (int c = columnInitiator, r = rowInitiator; c >= 0 && r < numOfRows; c--, r++)
                    {
                        sb.Append(rows[r][c]);
                    }
                    columnInitiator++;
                    diagonals[d] = sb.ToString();
                    sb.Clear();
                }
                if (d == numOfColumns)
                {
                    rowInitiator = 1;
                }
                if (d >= numOfColumns && rowInitiator < numOfRows)
                {
                    columnInitiator = numOfColumns - 1;

                    for (int c = columnInitiator, r = rowInitiator; c >= 0 && r < numOfRows; c--, r++)
                    {
                        sb.Append(rows[r][c]);
                    }
                    rowInitiator++;
                    diagonals[d] = sb.ToString();
                    sb.Clear();
                }
            }

            return diagonals;
        }
    }
}
