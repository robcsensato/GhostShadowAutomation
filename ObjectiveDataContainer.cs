using System;
//using System.Collections.Generic;
//using System.Text;
using System.Collections;

namespace Automation
{
    public class ObjectiveDataContainer
    {
        ArrayList ObjectiveArray = new ArrayList();

        public void ClearArray()
        {
            ObjectiveArray.Clear();
        }

        public void AddToArrayList(string objectivename, int count)
        {
            ObjectiveArray.Add(objectivename);
            ObjectiveArray.Add(count);
        }

        public int ReturnCountForObjective(string objectivename)
        {
            int NumberOfObjectivesStored = 0;
            int Count = 0;

            if (ObjectiveArray.Count > 0)
            { 
                NumberOfObjectivesStored = ObjectiveArray.Count;
            }

            for (int I=0; I < NumberOfObjectivesStored; I=I+2)
            {
                string NameFromArray = ObjectiveArray[I].ToString();

                if (NameFromArray == objectivename)
                {
                    string CountString = ObjectiveArray[I + 1].ToString();
                    Count = Convert.ToInt16(CountString);
                    break;
                }
            }

            return Count;
        }

        public int ReturnNumberOfObjectivesInArray()
        {
            return ObjectiveArray.Count;
        }

        public string ReturnObjectiveBasedOnIndex(int index)
        {
            return ObjectiveArray[index].ToString();
        }

         
    }
}
