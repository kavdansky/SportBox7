#pragma checksum "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Admins\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "11f0f5d45a70c42e64a5587ee1d510243619d3dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Editors_Views_Admins_Index), @"mvc.1.0.view", @"/Areas/Editors/Views/Admins/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"11f0f5d45a70c42e64a5587ee1d510243619d3dd", @"/Areas/Editors/Views/Admins/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"16aa6c3b10dcf43a2a238b8dd280e0cb8ea3bd2d", @"/Areas/Editors/Views/_ViewImports.cshtml")]
    public class Areas_Editors_Views_Admins_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Admins\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Editors/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Admin Panel</h1>


<button class=""btn btn-primary"" onclick=""window.location='/Editors/Users/AllUsers'"">Users Administrtion</button>
<button class=""btn btn-primary"" onclick=""window.location=''"">Content Management</button>
<button class=""btn btn-primary"" onclick=""window.location=''"">Site Management</button>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591