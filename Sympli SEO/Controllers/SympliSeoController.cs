using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SympliSeo.FormModels;
using SympliSeo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpliSeo.Controllers
{
    [Route("sympliseo")]
    [ApiController]
    public class SympliSeoController : ControllerBase
    {
        //Dependency injecting repository
        private ISympliSeoRepository _simpliSeoRepository;
        private readonly IMemoryCache _memoryCache;
        public SympliSeoController(ISympliSeoRepository simpliSeoRepository, IMemoryCache memoryCache)
        {
            _simpliSeoRepository = simpliSeoRepository;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Return model with search count
        /// </summary>
        /// <param name="inputSearchData">Model to provide search input data</param>
        /// <returns>Returns model with count of url appeared in search</returns>
        [HttpPost("search")]
       public async Task<ActionResult<OutputSearchResultData>> SearchKeywords(InputSearchData inputSearchData)
        {
            try
            {
                string search_company_url_in_results = "";
                string search_engine_url = "";
                string[] search_keywords = new string[] { };

                search_keywords = inputSearchData.search_keywords;
                search_company_url_in_results = inputSearchData.domain_url;
                search_engine_url = inputSearchData.search_engine_url;

                if (search_keywords.Length ==0)
                {
                    return BadRequest("Search keywords data is empty");
                }

                // Check company url is not empty or valid
                if (string.IsNullOrEmpty(search_company_url_in_results))
                {
                    return BadRequest("Organization url is empty");
                }
                else if (search_company_url_in_results == @"https://www.sympli.com.au")
                {
                    inputSearchData.domain_url = search_company_url_in_results;

                }
                else
                {
                    return BadRequest("Invalid organization url");

                }



                if (string.IsNullOrEmpty(search_engine_url))
                {
                    return BadRequest("Search engine url is empty");
                }
                else if (search_engine_url == @"https://www.google.com.au")
                {
                    inputSearchData.search_engine_url = search_engine_url + "/search?q=";
                }
                else if (search_engine_url == @"https://www.bing.com")
                {
                    inputSearchData.search_engine_url = search_engine_url + "/search?q=";
                }
                else
                {
                    return BadRequest("Invalid search engine url");

                }
                var cacheKey = "output_cache_data";
                List<OutputSearchResultData> lstoutputSearchResultData = new List<OutputSearchResultData>();
                lstoutputSearchResultData = await _simpliSeoRepository.GetSearchKeywordsCount(inputSearchData);
                //checks if cache entries exists
                if (!_memoryCache.TryGetValue(cacheKey, out List<OutputSearchResultData> output_cache_data))
                {
                    lstoutputSearchResultData = await _simpliSeoRepository.GetSearchKeywordsCount(inputSearchData);
                    if (lstoutputSearchResultData.Count==0)
                    {
                        return BadRequest("Count is empty.Url is not resulted in search");
                    }
                    //setting up cache options
                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromHours(1)
                    };
                    //setting cache entries
                    _memoryCache.Set(cacheKey, output_cache_data, cacheExpiryOptions);
                }


                   

                //return count of url appeared in search
                return Ok(lstoutputSearchResultData);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message.ToString());
            }
           
        }
    }
}
