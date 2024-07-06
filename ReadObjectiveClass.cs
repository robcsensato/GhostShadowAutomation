using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class ReadObjectiveClass
    { // Beginning of ObjectiveClass

        public void StartReadingObjectives()
        {
            int maxindex = 0;

            string CurrentObjectiveName = "";
            int NumberOfTotalElementsStored = 0;
            bool FirstTimeInLoop = false;

      //      int NumberOfTotalElementsStored = ObjectDataInfo.ReturnNumberOfObjectivesInArray(); //--> Need Event. Return int

      //      int TotalNumberOfObjectives = NumberOfTotalElementsStored / 2;

            // Go Through List Of Stored Objectives set into Array

            for (int A = 0; A < NumberOfTotalElementsStored; A = A + 2)
            {
                FirstTimeInLoop = true;

      //          CurrentObjectiveName = ObjectDataInfo.ReturnObjectiveBasedOnIndex(A); //--> Need Event. Return String
      //          maxindex = ObjectDataInfo.ReturnCountForObjective(CurrentObjectiveName); //--> Need Event. Return int

      //          LogWritePlayBackEvents(""); //--> LogwriteRelayEvent
      //          LogWritePlayBackEvents("Starting Objective: " + CurrentObjectiveName); //--> LogwriteRelayEvent
      //          LogWritePlayBackEvents("Loop Index Set to: " + maxindex.ToString()); //--> LogwriteRelayEvent
      //          LogWritePlayBackEvents(""); //--> LogwriteRelayEvent

                if (maxindex == 0)
                {
      //              LogWritePlayBackEvents("Skipping Test: " + CurrentObjectiveName); //--> LogwriteRelayEvent
      //              LogWritePlayBackEvents(""); //--> LogwriteRelayEvent
                }

                for (int I = 0; I < maxindex; I++)
                {
                    if (I > 0)
                    {
                        if (I == 1)
                        {
      //                      LogWritePlayBackEvents(""); //--> LogwriteRelayEvent
      //                      LogWritePlayBackEvents("First Loop.."); //--> LogwriteRelayEvent
                        }

      //                  LogWritePlayBackEvents("Loop Index: " + I.ToString()); //--> LogwriteRelayEvent
                    }

      //              if (HaltReadingXMLValue)  //--> Need Event..Returns bool
      //                  break;

      //              GlobalIndexLoop = I; //--> Need Event. Need to update GlobalIndevLoop within MainClass...---> UpdateGlobalIndexLoopEvent(I)
                    //  StartPlayBackThread = new Thread(new ThreadStart(StartReadingXMLWithObjectives));
                    //  StartPlayBackThread.SetApartmentState(ApartmentState.STA);
                    //  StartPlayBackThread.Start();

      //              Playback.StartReadingXML(objDom, CurrentObjectiveName, FirstTimeInLoop); //--> Create Event to have MainClass trigger this
                    //--> RelayStartReadingXMLEvent(objDom, CurrentObjectiveName, FirstTimeInLoop);
                    FirstTimeInLoop = false;
                }
            }

        } // End of StartReadingObjectives Method


    } /// End of ReadObjectiveClass
} // End of Automation NameSpace
