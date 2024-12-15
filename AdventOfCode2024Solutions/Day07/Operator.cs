using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day07
{
    public class Operator
    {
        public long TestValue { get; set; }
        public int[]  Numbers { get; set; }

        protected bool CombinationFoundValue = false;
        public bool CombinationFound { get { return CombinationFoundValue; } }
        protected bool TestedValue = false; 
        public bool Tested { get { return TestedValue; } }

        protected OperatorEnum[][] CombinationsValue = Array.Empty<OperatorEnum[]>();
        protected int CombinationsIndex = 0;
        protected OperatorEnum[] Options = [OperatorEnum.Add, OperatorEnum.Multiply];
        protected OperatorEnum[] Options2 = [OperatorEnum.Concat, OperatorEnum.Add, OperatorEnum.Multiply];
        protected OperatorEnum[] CurrentCombo = Array.Empty<OperatorEnum>();

        public Operator() 
        {
            TestValue = -1;
            Numbers = Array.Empty<int>();
        }

        public bool TestCombinations(bool use2 = false)
        {
            // fill combinatins first
            if (use2)
            {
                CombinationsValue = new OperatorEnum[(int)Math.Pow(Options2.Length, Numbers.Length - 1)][];
                CurrentCombo = new OperatorEnum[Numbers.Length - 1];
                FillCombo(CurrentCombo.Length - 1, Options2);
            }
            else
            {
                CombinationsValue = new OperatorEnum[(int)Math.Pow(Options.Length, Numbers.Length - 1)][];
                CurrentCombo = new OperatorEnum[Numbers.Length - 1];
                FillCombo(CurrentCombo.Length - 1, Options);
            }
            

            // test combinations
            foreach (OperatorEnum[] combo in CombinationsValue)
            {
                if (TestCombo(combo))
                {
                    CombinationFoundValue = true;
                    break;
                }
            }


            TestedValue = true;

            return CombinationFound;
        }

        protected void FillCombo(int comboIndex, OperatorEnum[] options)
        {
            for(int optionsIndex = options.Length-1; -1 < optionsIndex; optionsIndex--)
            {
                CurrentCombo[comboIndex] = options[optionsIndex];

                if (0 < comboIndex)
                {
                    FillCombo(comboIndex-1, options);
                }
                else
                {
                    CombinationsValue[CombinationsIndex] = (OperatorEnum[])CurrentCombo.Clone();
                    CombinationsIndex++;
                }
            }
        }

        protected bool TestCombo(OperatorEnum[] aCombo)
        {
            long result = Numbers[0];

            for (int i = 0; i < aCombo.Length; i++)
            {
                if (OperatorEnum.Multiply == aCombo[i])
                {
                    result *= Numbers[i + 1];
                }
                else if(OperatorEnum.Add == aCombo[i])
                {
                    result += Numbers[i + 1];
                }
                else if (OperatorEnum.Concat == aCombo[i])
                {
                    result = Int64.Parse(result.ToString() + Numbers[i + 1].ToString());
                }

                if (TestValue < result)
                {
                    return false;
                }
            }

            if (TestValue == result)
            {
                return true;
            }

            return false;
        }

    }
}
