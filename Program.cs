using CoronavirusModels;
using System;
using System.Collections.Generic;
using Virus.Data.Layer;

namespace CoronavirusParser
{
    class Program
    {
        static void Main(string[] args)
        {
            VirusDAL virusDal = new VirusDAL();

            CaseParser parser = new CaseParser();

            try
            {
                var caseBlobs = virusDal.GetUnprocessedCaseBlobs();

                List<Case> parsedCases = parser.ParseCases(caseBlobs);

                virusDal.SaveCases(parsedCases);


                foreach (var caseBlob in caseBlobs)
                {
                    virusDal.MarkProcessed(caseBlob.casesBlobId);
                }



                //mark as processed





                //after the information is parsed
                // mark processed as "1" and then move on
                //update the method
            }
            catch (Exception e)
            {
                var error = new VirusErrorLog();

                error.ApplicationName = "VirusScraper";
                error.ExceptionMessage = e.Message;
                error.StackTrace = e.StackTrace;

                virusDal.SaveErrorLog(error);
            }
        }
    }
}