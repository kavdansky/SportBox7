#pragma checksum "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Users\ActFail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8e81b940b5bb0b0ee4169b870d6bd6454f59b776"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Editors_Views_Users_ActFail), @"mvc.1.0.view", @"/Areas/Editors/Views/Users/ActFail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e81b940b5bb0b0ee4169b870d6bd6454f59b776", @"/Areas/Editors/Views/Users/ActFail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"16aa6c3b10dcf43a2a238b8dd280e0cb8ea3bd2d", @"/Areas/Editors/Views/_ViewImports.cshtml")]
    public class Areas_Editors_Views_Users_ActFail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SportBox7.Areas.Editors.ViewModels.Users.DeactivateUserViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Users\ActFail.cshtml"
  
    ViewData["Title"] = "Deactivate User Fail";
    Layout = "~/Areas/Editors/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"container-fluid col-12\">\r\n");
#nullable restore
#line 7 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Users\ActFail.cshtml"
         if (Model.IsActive)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h1>Deactivation ");
#nullable restore
#line 9 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Users\ActFail.cshtml"
                        Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" failed!</h1>\r\n");
#nullable restore
#line 10 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Users\ActFail.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h1>Activation ");
#nullable restore
#line 13 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Users\ActFail.cshtml"
                      Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" failed!</h1>\r\n");
#nullable restore
#line 14 "C:\Users\Liubo PC\SportBox7\SportBox7\SportBox7\Areas\Editors\Views\Users\ActFail.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        <a href=\"/Editors/Users/AllUsers\">Go to all users</a>\r\n\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SportBox7.Areas.Editors.ViewModels.Users.DeactivateUserViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
