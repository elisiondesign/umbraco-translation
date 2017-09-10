using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Services;
using Umbraco.Core.Publishing;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace halamek_umbraco.Controllers
{
    public class TranslationController : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Saved += ContentService_Saved;
            ContentService.Deleted += ContentService_Deleted;
        }

        private void ContentService_Saved(IContentService sender, SaveEventArgs<Umbraco.Core.Models.IContent> e)
        {
            var savedEntity = e.SavedEntities?.First();
            if (savedEntity.IsNewEntity()) {
                createDocumentTranslation(savedEntity);
            }
        }

        private void ContentService_Deleted(IContentService sender, DeleteEventArgs<Umbraco.Core.Models.IContent> e) {
            var relationService = ApplicationContext.Current.Services.RelationService;
            foreach (var deletedEntity in e.DeletedEntities) {
                var translations = relationService.GetByParentOrChildId(deletedEntity.Id);
                foreach (var translation in translations) {
                    relationService.Delete(translation);
                }
            }
        }


        private void createDocumentTranslation(IContent content)
        {
            var contentService = ApplicationContext.Current.Services.ContentService;
            var relationService = ApplicationContext.Current.Services.RelationService;
            
            var parentItems = content.Ancestors();
            var directParent = parentItems.LastOrDefault();
            var translations = relationService.GetByParent(directParent,"translation");
            if (translations.Count() > 0) {
                foreach (var translation in translations)
                {
                    var copy = contentService.Copy(content, translation.ChildId, false);
                    relationService.Relate(content, copy, "translation");
                }
            }

            return;
        }
    }
}