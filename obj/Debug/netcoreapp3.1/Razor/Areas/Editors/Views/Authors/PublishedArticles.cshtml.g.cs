#pragma checksum "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e26defd33d202cea9f9554adccc699e10dc6b9e4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Editors_Views_Authors_PublishedArticles), @"mvc.1.0.view", @"/Areas/Editors/Views/Authors/PublishedArticles.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\_ViewImports.cshtml"
using SportBox7;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\_ViewImports.cshtml"
using SportBox7.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml"
using SportBox7.Areas.Editors.ViewModels.Content;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e26defd33d202cea9f9554adccc699e10dc6b9e4", @"/Areas/Editors/Views/Authors/PublishedArticles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"16aa6c3b10dcf43a2a238b8dd280e0cb8ea3bd2d", @"/Areas/Editors/Views/_ViewImports.cshtml")]
    public class Areas_Editors_Views_Authors_PublishedArticles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ICollection<AllArticlesViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml"
  
    ViewData["Title"] = "PublishedArticles";
    Layout = "~/Areas/Editors/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""container-fluid col-12"">
        <h1>Published Articles</h1>
        <hr />
        <br />
        <table class=""table table-bordered"">
            <tr>
                <th>Date</th>
                <th>Title</th>
                <th>Category</th>
            </tr>
");
#nullable restore
#line 17 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml"
             foreach (var draft in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 20 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml"
                   Write(draft.CreationDate.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 21 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml"
                   Write(draft.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 22 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml"
                   Write(draft.Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 24 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\PublishedArticles.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </table>\r\n\r\n        <button class=\"btn btn-primary\" onclick=\"window.location=\'/Editors/Authors/Index\'\">Authors Panel</button>\r\n    </div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ICollection<AllArticlesViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
