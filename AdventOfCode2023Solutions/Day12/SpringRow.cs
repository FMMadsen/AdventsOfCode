using System.Linq;

namespace AdventOfCode2023Solutions.Day12
{
    public class SpringRow
    {
        private const char BROKEN = '#';
        private const char UNKNOWN = '?';
        private const char GOOD = '.';

        public long NumberOfPotentialStates { get; private set; } = 0;
        internal string ConditionRecordString { get; private set; }
        internal char[] Springs { get; private set; }
        internal int NumberOfSprins { get; private set; }
        internal int[] TargetDamagedSpringsGroups { get; private set; }
        internal int NumberOfTargetDamagedSpringsGroups { get; private set; }

        public SpringRow(string conditionRecord)
        {
            var conditionStringSplit = conditionRecord.Split(' ');
            ConditionRecordString = conditionStringSplit[0].Trim() + '.';
            Springs = ConditionRecordString.ToArray();
            NumberOfSprins = Springs.Length;
            TargetDamagedSpringsGroups = conditionStringSplit[1].Trim().Split(',').Select(s => int.Parse(s)).ToArray();
            NumberOfTargetDamagedSpringsGroups = TargetDamagedSpringsGroups.Length;
        }

        public void AnalyzeNumberOfPotentialStates()
        {
            int targetDamagedSprings;
            var springs = Springs;

            for (int c = 0; c < NumberOfTargetDamagedSpringsGroups; c++)
            {
                targetDamagedSprings = TargetDamagedSpringsGroups[0];
                var possibleSectionsForTarget = IdentifySectionsForTarget(targetDamagedSprings, springs);

                for (int s = 0; s < NumberOfSprins; s++)
                {
                    
                }

            }




        }

        public List<SpringSection> IdentifySectionsForTarget()
        {
            return IdentifySectionsForTarget(TargetDamagedSpringsGroups[0], Springs);
        }

        internal List<SpringSection> IdentifySectionsForTarget(int targetDamagedSprings, char[] springs)
        {
            var sectionsForTarget = new List<SpringSection>();

            int springIndex = 0;
            int damagedSpringCounter = 0;
            bool sectionFound = false;

            while(!sectionFound)
            {
                if (springs[springIndex] == BROKEN || springs[springIndex] == UNKNOWN)
                {
                    damagedSpringCounter++;
                }
                else
                {
                    if (damagedSpringCounter > 0)
                        break;  //Section not found
                }
                if(damagedSpringCounter == targetDamagedSprings)
                {
                    sectionFound = true;
                }
            }
            if(sectionFound)
            {
                if(IsNextSpringNonBroken(springs, springIndex))
                {
                    var newSpringSection = new SpringSection();
                    newSpringSection.Springs = springs.Take(springIndex+1).ToArray();
                    newSpringSection.SpringsString = new string(newSpringSection.Springs);
                    sectionsForTarget.Add(newSpringSection);
                }
            }

            //for (int s = 0; s < targetDamagedSprings; s++)
            //{
            //    if (springs[s] == BROKEN || springs[s] == UNKNOWN)
            //        continue;
            //    else
            //        return null;
            //}

            //if (targetDamagedSprings = springs.Length)

            //    if (targetDamagedSprings = springs.Length)
            //{
            //    sectionsForTarget 
            //    return new List<SpringSection>();
            //}


            return sectionsForTarget;
        }

        private bool IsNextSpringNonBroken(char[] springs, int springIndex)
        {

            var nextSpring = springs[springIndex];
            return nextSpring == GOOD || nextSpring == UNKNOWN;
        }
    }
}
