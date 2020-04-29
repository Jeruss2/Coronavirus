using CoronavirusModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoronavirusParser
{
    public class CaseParser
    {
        public List<Case> ParseCases(List<CaseBlobs> blobs)
        {
            List<Case> parsedCases = new List<Case>();

            foreach (var blob in blobs)
            {
                var htmlDoc = new HtmlDocument();

                try
                {
                    static string RemoveNonNumberDigitsAndCharacters(string text)
                    {
                        var numericChars = "0123456789,.".ToCharArray();
                        return new String(text.Where(c => numericChars.Any(n => n == c)).ToArray());
                    }

                    htmlDoc = new HtmlDocument();

                    // check on the TotalCasesBlob?

                    htmlDoc.LoadHtml(blob.TotalCasesBlob);
                    htmlDoc.LoadHtml(blob.TotalDeathsBlob);
                    htmlDoc.LoadHtml(blob.TotalRecoveriesBlob);


                    string TotalCases = RemoveNonNumberDigitsAndCharacters(blob.TotalCasesBlob);
                    string TotalDeaths = RemoveNonNumberDigitsAndCharacters(blob.TotalDeathsBlob);
                    string TotalRecoveries = RemoveNonNumberDigitsAndCharacters(blob.TotalRecoveriesBlob);
                    DateTime DateAdded = DateTime.Now;

                    //var nodes = htmlDoc.DocumentNode.AncestorsAndSelf().ToList();

                    //string TotalCases = nodes[0].SelectNodes("//*[@id=\"maincounter-wrap\"]/div/span").ToList()[0].InnerText;

                    //string TotalDeaths = nodes[0].SelectNodes("//*[@id=\"maincounter-wrap\"]/div/span").ToList()[0].InnerText;

                    //string TotalRecoveries = nodes[0].SelectNodes("//*[@id=\"maincounter-wrap\"]/div/span").ToList()[0].InnerText;





                    Case l = new Case()
                    {
                        TotalCases = TotalCases,
                        TotalDeaths = TotalDeaths,
                        TotalRecoveries = TotalRecoveries,
                        DateAdded = DateAdded
                    };

                    parsedCases.Add(l);
                }
                catch (Exception e)
                {
                    File.WriteAllText("parseException_" + DateTime.UtcNow.ToFileTimeUtc(),
                        e.Message + "\r\n\r\n" + htmlDoc.DocumentNode.InnerHtml);
                }


            }

            return parsedCases;
        }
    }
}




