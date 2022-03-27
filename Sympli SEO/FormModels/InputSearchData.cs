using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SympliSeo.FormModels
{
    //Input data requested by the user.
    public class InputSearchData
    {
        //Search keyword entered by the user. 
        public string[] search_keywords { get; set; }
        //Organisation URL
        public string domain_url { get; set; }
        //Search Engine
        public string search_engine_url { get; set; }

    }
}
