// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.Controllers.HelpController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Areas.HelpPage.ModelDescriptions;
using m2ostnextservice.Areas.HelpPage.Models;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace m2ostnextservice.Areas.HelpPage.Controllers
{
  public class HelpController : Controller
  {
        private const string ErrorViewName = "Error";

        public HttpConfiguration Configuration
        {
            get;
            private set;
        }

        public HelpController() : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            this.Configuration = config;
        }

        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = this.Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return base.View(apiModel);
                }
            }
            return base.View("Error");
        }

        public ActionResult Index()
        {
            ((dynamic)base.ViewBag).DocumentationProvider = this.Configuration.Services.GetDocumentationProvider();
            return base.View(this.Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult ResourceModel(string modelName)
        {
            ModelDescription modelDescription;
            if (!string.IsNullOrEmpty(modelName) && this.Configuration.GetModelDescriptionGenerator().GeneratedModels.TryGetValue(modelName, out modelDescription))
            {
                return base.View(modelDescription);
            }
            return base.View("Error");
        }
    }
}
