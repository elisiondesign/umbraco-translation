using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Umbraco.Core.Models;

namespace halamek_umbraco.Controllers{

    [PluginController("AddTranslation")]
    public class AddTranslationApiController : UmbracoAuthorizedApiController
    {

        [HttpGet]
        public void createLanguageVersion(int nodeId, string languageCode)
        {
            var contentService = ApplicationContext.Services.ContentService;
            var relationService = ApplicationContext.Services.RelationService;
            var content = contentService.GetById(nodeId);
            var parent = content.Parent();
            var parentId = parent != null ? parent.Id : -1;
            var copy = contentService.Copy(content, parentId, true, true);
            copy.Name = languageCode;
            contentService.Save(copy);
            relationService.Relate(content, copy, "translation");

            var copiedItems = copy.Descendants();

            foreach (var item in copiedItems)
            {
                var originalItemId = relationService.GetByChild(item, "relateDocumentOnCopy").First().Id;
                var originalItem = contentService.GetById(originalItemId);
                relationService.Relate(originalItem, item, "translation");
            }


        }
    }
}
