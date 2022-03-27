using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SympliSeo.DbModels
{
    //Model to load the data into the data table.
    public class DbSearchData
    {
        //Auto increment of primary key Id.
        [Key]
        public int Id { get; set; }
        public string[] SearchKeywords { get; set; }
        public string Url { get; set; }
        public DateTime SearchedDatetime { get; set; }

    }
}
