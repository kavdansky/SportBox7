#pragma checksum "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d079f797cf7796d991df32cc52fe76c9e567a9d3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Editors_Views_Authors_AllDrafts), @"mvc.1.0.view", @"/Areas/Editors/Views/Authors/AllDrafts.cshtml")]
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
#line 1 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
using SportBox7.Areas.Editors.ViewModels.Content;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d079f797cf7796d991df32cc52fe76c9e567a9d3", @"/Areas/Editors/Views/Authors/AllDrafts.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"16aa6c3b10dcf43a2a238b8dd280e0cb8ea3bd2d", @"/Areas/Editors/Views/_ViewImports.cshtml")]
    public class Areas_Editors_Views_Authors_AllDrafts : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ICollection<AllArticlesViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
  
    ViewData["Title"] = "AllDrafts";
    Layout = "~/Areas/Editors/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""container-fluid col-12"">
        <h1>Your Drafts</h1>
        <hr />
        <button class=""btn btn-primary"" onclick=""window.location='/Editors/Authors/AddNewDraft'"">Add new draft</button>
        <br />
        <br />
        <table class=""table table-bordered"">
            <tr>
                <th>Date</th>
                <th>Title</th>
                <th>Category</th>
                <th>Actions</th>
            </tr>
");
#nullable restore
#line 20 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
             foreach (var draft in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 23 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
                   Write(draft.CreationDate.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 24 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
                   Write(draft.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 25 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
                   Write(draft.Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n                        <a");
            BeginWriteAttribute("href", " href=\"", 946, "\"", 991, 2);
            WriteAttributeValue("", 953, "/Editors/Authors/DeleteDraft/", 953, 29, true);
#nullable restore
#line 27 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
WriteAttributeValue("", 982, draft.Id, 982, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Delete</a> | <a");
            BeginWriteAttribute("href", " href=\"", 1008, "\"", 1051, 2);
            WriteAttributeValue("", 1015, "/Editors/Authors/EditDraft/", 1015, 27, true);
#nullable restore
#line 27 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
WriteAttributeValue("", 1042, draft.Id, 1042, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a>\r\n                        | <a");
            BeginWriteAttribute("href", " href=\"", 1091, "\"", 1138, 2);
            WriteAttributeValue("", 1098, "/Editors/Authors/SendForReview/", 1098, 31, true);
#nullable restore
#line 28 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
WriteAttributeValue("", 1129, draft.Id, 1129, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Send for review</a>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 31 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Authors\AllDrafts.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </table>\r\n\r\n        <button class=\"btn btn-primary\" onclick=\"window.location=\'/Editors/Authors/Index\'\">Authors Panel</button>\r\n\r\n</div>");
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
