using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;

namespace halamek_umbraco.Controllers
{
    public class TranslateActionController : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);
            TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;
        }

        private void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            if (sender != null && sender.TreeAlias == "content")
            {
                int nodeId;
                Int32.TryParse(e.NodeId, out nodeId);
                if (nodeId > 0)
                {
                    var documentTypeName = ApplicationContext.Current.Services.ContentService.GetById(nodeId).Name;
                    if (documentTypeName == "cs")
                    {
                        var context = new HttpContextWrapper(HttpContext.Current);
                        var urlHelper = new UrlHelper(new RequestContext(context, new RouteData()));
                        var menuItem = new MenuItem("addTranslation", "Add Site Translation");
                        menuItem.Icon = "globe-alt";
                        menuItem.LaunchDialogView(urlHelper.Content("~/App_Plugins/AddTranslation/addtranslation.html"), "Add Translation");
                        e.Menu.Items.Add(menuItem);
                    }
                }
            }
        }
    }
}