using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OgcApi.Net.Crs;
using OgcApi.Net.Options;
using OgcApi.Net.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgcApi.Net.Controllers
{
    public partial class CollectionsController
    {
        [HttpGet("{collectionId}/queryables")]
        [Produces("application/schema+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetQueryables(string collectionId)
        {
            var baseUri = Utils.GetBaseUrl(Request);

            _logger.LogTrace("Get collection queryables with parameters {query}", Request.QueryString);

            var collectionOptions = _apiOptions.Collections.Items.Find(x => x.Id == collectionId);
            if (collectionOptions != null)
            {
                return Ok(GetCollectionQueryables(new Uri(baseUri, $"collections/{collectionOptions.Id}/queryables"), collectionOptions));
            }

            return NotFound();
        }

        private Queryables GetCollectionQueryables(Uri uri, CollectionOptions collectionOptions)
        {
            Dictionary<string, QueryableProperty> properties = collectionOptions.Features.Queryables.Properties
                .ToDictionary(p => p.Title, p => p);

            var queryables = new Queryables
            {
                Id = uri.AbsoluteUri,
                Type = "object",
                Schema = "http://json-schema.org/draft/2019-09/schema",
                Title = collectionOptions.Title,
                Properties = properties
            };
            return queryables;
        }
    }
}
