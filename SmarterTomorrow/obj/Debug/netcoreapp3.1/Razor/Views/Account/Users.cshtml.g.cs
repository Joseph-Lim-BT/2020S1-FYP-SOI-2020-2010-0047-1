#pragma checksum "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4fe0e8c362edb6844f75d311f9ff64029695df62"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Users), @"mvc.1.0.view", @"/Views/Account/Users.cshtml")]
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
#line 1 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\_ViewImports.cshtml"
using System.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\_ViewImports.cshtml"
using SmarterTomorrow.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4fe0e8c362edb6844f75d311f9ff64029695df62", @"/Views/Account/Users.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"53da281d4f50f3315d2c33d5a652b673e9bef19f", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Users : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<User>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/css/jquery.dataTables.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/js/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap/css/bootstrap.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap/js/bootstrap.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Update", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h2>List of Users</h2>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4fe0e8c362edb6844f75d311f9ff64029695df627084", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fe0e8c362edb6844f75d311f9ff64029695df628198", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4fe0e8c362edb6844f75d311f9ff64029695df629237", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fe0e8c362edb6844f75d311f9ff64029695df6210351", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<style>
    a {
        display: inline-block;
    }
</style>


<script>
    $(document).ready(function () {
        $('#jsUserTable').DataTable({
            ordering: true,
            paging: true,
            searching: true,
            info: true,
            lengthChange: true,
            lengthMenu: [[6, 10, 20, -1], [6, 10, 20, ""All""]]
        });
    });
     
    /**/</script>

");
#nullable restore
#line 29 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
 if (TempData["Message"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 789, "\"", 829, 3);
            WriteAttributeValue("", 797, "alert", 797, 5, true);
            WriteAttributeValue(" ", 802, "alert-", 803, 7, true);
#nullable restore
#line 31 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
WriteAttributeValue("", 809, TempData["MsgType"], 809, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n        ");
#nullable restore
#line 32 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
   Write(TempData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>  ");
#nullable restore
#line 33 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"inline-buttons\">\r\n");
#nullable restore
#line 35 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
     if (User.IsInRole("0"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fe0e8c362edb6844f75d311f9ff64029695df6213348", async() => {
                WriteLiteral("Create New User Record");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 38 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>

<div class=""container"">
    <table class=""table table-condensed table-hover"" id=""jsUserTable"">
        <thead>

            <tr>
                <th scope=""col"">NRIC</th>
                <th scope=""col"">Full Name</th>
                <th scope=""col"">Unit</th>
                <th scope=""col"">Rank</th>
                <th scope=""col"">Role</th>
                <th scope=""col"">Last Login</th>
                <th scope=""col"">DateTime Created</th>
                <th scope=""col"">DateTime Modified</th>
                <th scope=""col"">Created By</th>
                <th scope=""col"">Modified By</th>
");
#nullable restore
#line 56 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                 if (User.IsInRole("0"))

                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <th scope=\"col\">Action</th>\r\n");
#nullable restore
#line 60 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tr>\r\n\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 65 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
             foreach (User user in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 68 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
               Write(user.NRIC);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 69 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
               Write(user.FULL_NAME);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n");
#nullable restore
#line 71 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                 if (user.UNIT.Equals(""))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>No Unit</td>\r\n");
#nullable restore
#line 74 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>");
#nullable restore
#line 77 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                   Write(user.UNIT);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 78 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 80 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                 if (user.RANK.Equals(""))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>No Rank</td>\r\n");
#nullable restore
#line 83 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else if (user.RANK.Equals("PR"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Private</td>\r\n");
#nullable restore
#line 87 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else if (user.RANK.Equals("SRGT"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Seargant</td>\r\n");
#nullable restore
#line 91 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else if (user.RANK.Equals("PC"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Platoon Commander</td>\r\n");
#nullable restore
#line 95 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else if (user.RANK.Equals("OC"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Officer Commander</td>\r\n");
#nullable restore
#line 99 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Commanding Officer</td>\r\n");
#nullable restore
#line 103 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 105 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                 if (user.GROUP_ID == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Admin</td>\r\n");
#nullable restore
#line 108 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else if (user.GROUP_ID == 1)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Store Supervisor</td>\r\n");
#nullable restore
#line 112 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else if (user.GROUP_ID == 2)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Store IC</td>\r\n");
#nullable restore
#line 116 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else if (user.GROUP_ID == 3)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Storemen</td>\r\n");
#nullable restore
#line 120 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>Servicemen</td>\r\n");
#nullable restore
#line 124 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                <td>");
#nullable restore
#line 127 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
               Write(String.Format("{0:yyyy-MM-dd hh:mm:ss}", user.DTE_TIME_LAST_LOGIN));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n\r\n                <td>");
#nullable restore
#line 130 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
               Write(user.DTE_TIME_CR);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 131 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
               Write(user.DTE_TIME_LAST_LOGIN);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 132 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
               Write(user.CREATED_BY);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 133 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
               Write(user.MODIFIED_BY);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n\r\n");
#nullable restore
#line 136 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                 if (User.IsInRole("0"))

                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fe0e8c362edb6844f75d311f9ff64029695df6224127", async() => {
                WriteLiteral("\r\n                            Edit\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 142 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                             WriteLiteral(user.NRIC);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        |\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4fe0e8c362edb6844f75d311f9ff64029695df6226601", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 148 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                             WriteLiteral(user.NRIC);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "onclick", 6, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4434, "return", 4434, 6, true);
            AddHtmlAttributeValue(" ", 4440, "confirm(\'Delete", 4441, 16, true);
            AddHtmlAttributeValue(" ", 4456, "User", 4457, 5, true);
            AddHtmlAttributeValue(" ", 4461, "[", 4462, 2, true);
#nullable restore
#line 149 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
AddHtmlAttributeValue("", 4463, user.NRIC, 4463, 10, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 4473, "]\')", 4473, 3, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </td>\r\n");
#nullable restore
#line 151 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tr>\r\n");
#nullable restore
#line 153 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\Account\Users.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<User>> Html { get; private set; }
    }
}
#pragma warning restore 1591
