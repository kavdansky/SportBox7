#pragma checksum "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc488b07b377b0664ad8adef34962ad24c4c63d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Editors_Views_Authors_ArticlesForReview), @"mvc.1.0.view", @"/Areas/Editors/Views/Authors/ArticlesForReview.cshtml")]
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
#line 1 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml"
using SportBox7.Areas.Editors.ViewModels.Content;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc488b07b377b0664ad8adef34962ad24c4c63d1", @"/Areas/Editors/Views/Authors/ArticlesForReview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"16aa6c3b10dcf43a2a238b8dd280e0cb8ea3bd2d", @"/Areas/Editors/Views/_ViewImports.cshtml")]
    public class Areas_Editors_Views_Authors_ArticlesForReview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ICollection<AllArticlesViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml"
  
    ViewData["Title"] = "ArticlesForReview";
    Layout = "~/Areas/Editors/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Articles for review</h1>\r\n<hr />\r\n<br />\r\n<table class=\"table table-bordered\">\r\n    <tr>\r\n        <th>Date</th>\r\n        <th>Title</th>\r\n        <th>Category</th>\r\n    </tr>\r\n");
#nullable restore
#line 17 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml"
     foreach (var draft in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 20 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml"
           Write(draft.CreationDate.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 21 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml"
           Write(draft.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 22 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml"
           Write(draft.Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 24 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\ArticlesForReview.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n\r\n<button class=\"btn btn-primary\" onclick=\"window.location=\'/Editors/Authors/Index\'\">Authors Panel</button>\r\n");
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
