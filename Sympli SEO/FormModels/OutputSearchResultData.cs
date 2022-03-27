using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SympliSeo.FormModels
{
    public class OutputSearchResultData
    {
        // Number of occurrences in google
        public string found_in_google_results_at { get; set; }

        // Number of occurrences in bing
        public string found_in_bing_results_at { get; set; }
    }
}
