﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CP380_B1_BlockList.Models;
using Microsoft.AspNetCore.Mvc;

namespace CP380_B3_BlockBlazor.Data
{
    public class BlockService
    {
        // TODO: Add variables for the dependency-injected resources
        //       - httpClient
        //       - configuration
        //
        static HttpClient _clientFactory;
        public IConfiguration Configuration { get; }

        //
        // TODO: Add a constructor with IConfiguration and IHttpClientFactory arguments
        //
        public BlockService() { }
        public BlockService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory.CreateClient();
            Configuration = configuration.GetSection("BlockService");
        }

        //
        // TODO: Add an async method that returns an IEnumerable<Block> (list of Blocks)
        //       from the web service
        //
        public async Task<IEnumerable<Block>> ListBlocks()
        {
            var response = await _clientFactory.GetAsync(Configuration["url"]);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Block>>(await response.Content.ReadAsStreamAsync());
            }
            return Array.Empty<Block>();
        }
    }
}
