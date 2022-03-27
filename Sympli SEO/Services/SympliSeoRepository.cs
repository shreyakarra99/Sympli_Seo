using SympliSeo.DbModels;
using SympliSeo.FormModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Web;

namespace SympliSeo.Services
{
    public interface ISympliSeoRepository
    {
        Task<List<OutputSearchResultData>> GetSearchKeywordsCount(InputSearchData inputSearchData);
    }
    /// <summary>
    /// 
    /// </summary>
    public class SympliSeoRepository : ISympliSeoRepository
    {
        private OutputSearchResultData outputSearchResultData=new OutputSearchResultData();


        //Method to perform seo and extract the count
        public async Task<List<OutputSearchResultData>> GetSearchKeywordsCount(InputSearchData inputSearchData)
        {
          
            string search_company_url_in_results = "";
            string search_engine_url = "";
            string[] search_keywords = new string[] { };

           List<OutputSearchResultData> lstOutputSearchResults = new List<OutputSearchResultData>();

            search_keywords = inputSearchData.search_keywords;
            search_company_url_in_results = inputSearchData.domain_url;
            search_engine_url = inputSearchData.search_engine_url;

          

            foreach (string keyword in search_keywords)
            {

                string uri = search_engine_url + HttpUtility.UrlEncode(keyword);

                //string tag = "<div id=\"resultStats\">";

            string tag = "url ? q ="+ search_company_url_in_results;
            int index;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader s = new StreamReader(resp.GetResponseStream(), Encoding.ASCII);
            string result = s.ReadToEnd();
            index = result.IndexOf(tag) + tag.Length;
            result = result.Substring(index, 100);
                if(index<=100)
                { 
                outputSearchResultData.found_in_google_results_at = index.ToString();
                }
                
                index = result.IndexOf("About ") + 6;
                result = result.Substring(index);
                index = result.IndexOf(" ");
                result = result.Substring(0, index);
                lstOutputSearchResults.Add(outputSearchResultData);
            }
            return lstOutputSearchResults;
        }
    }



}
