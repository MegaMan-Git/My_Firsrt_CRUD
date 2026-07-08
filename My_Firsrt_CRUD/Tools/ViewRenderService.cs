using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

namespace My_Firsrt_CRUD.Tools
{
    public class ViewRenderService
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> RenderToStringAsync(ControllerContext controllerContext, string viewName, object model)
        {
            var viewResult = _viewEngine.FindView(controllerContext, viewName, false);

            if (!viewResult.Success)
                throw new FileNotFoundException($"View '{viewName}' not found.");

            var viewData = new ViewDataDictionary(
                new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
            {
                Model = model
            };

            using var sw = new StringWriter();

            var viewContext = new ViewContext(
                controllerContext,
                viewResult.View,
                viewData,
                new TempDataDictionary(controllerContext.HttpContext, _tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);

            return sw.ToString();
        }
    }


}
