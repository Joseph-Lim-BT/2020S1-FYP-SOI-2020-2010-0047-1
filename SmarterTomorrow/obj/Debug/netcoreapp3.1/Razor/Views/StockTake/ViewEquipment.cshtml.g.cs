#pragma checksum "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "046a7f70620581cd828d574b029dcf1714e1df1b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_StockTake_ViewEquipment), @"mvc.1.0.view", @"/Views/StockTake/ViewEquipment.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"046a7f70620581cd828d574b029dcf1714e1df1b", @"/Views/StockTake/ViewEquipment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"53da281d4f50f3315d2c33d5a652b673e9bef19f", @"/Views/_ViewImports.cshtml")]
    public class Views_StockTake_ViewEquipment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<stocktake_equipment>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/css/jquery.dataTables.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/js/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            DefineSection("MoreScripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "046a7f70620581cd828d574b029dcf1714e1df1b4432", async() => {
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
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "046a7f70620581cd828d574b029dcf1714e1df1b5610", async() => {
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
                WriteLiteral(@"

    <script>
        $(document).ready(function () {
            $('#jsStocktakeEquipment').DataTable({
                ordering: true,
                paging: true,
                searching: true,
                info: true,
                lengthChange: true,
                pageLength: 20
            });
        });
    </script>

");
            }
            );
            WriteLiteral("\r\n\r\n<h2> Equipment </h2>\r\n\r\n");
#nullable restore
#line 25 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
 if (TempData["Message"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 649, "\"", 689, 3);
            WriteAttributeValue("", 657, "alert", 657, 5, true);
            WriteAttributeValue(" ", 662, "alert-", 663, 7, true);
#nullable restore
#line 27 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
WriteAttributeValue("", 669, TempData["MsgType"], 669, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n        ");
#nullable restore
#line 28 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
   Write(TempData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 30 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div>

    <table id=""jsStocktakeEquipment"" class=""table"">
        <thead>
            <tr>
                <th scope=""col"">Equipment ID</th>
                <th scope=""col"">Equipment Name</th>
                <th scope=""col"">Equipment Material</th>
                <th scope=""col"">Serial No</th>
                <th scope=""col"">Equipment Type</th>
                <th scope=""col"">Storage Location</th>
                <th scope=""col"">Storage Bin</th>
                <th scope=""col"">Box Lot No</th>
                <th scope=""col"">Butt No</th>
                <th scope=""col"">Quantity</th>
                <th scope=""col"">Status</th>
            </tr>
        </thead>

        <tbody>
");
#nullable restore
#line 52 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
             foreach (stocktake_equipment eq in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 55 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                   Write(eq.EQUIPMENT_ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 56 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                   Write(eq.EQUIPMENT_NAME);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n");
#nullable restore
#line 58 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                         if (eq.ELEMENT_MATERIAL_NO == "XYZ123")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Weapons</p>\r\n");
#nullable restore
#line 61 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else if (eq.ELEMENT_MATERIAL_NO == "XYZ234")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Accessories</p>\r\n");
#nullable restore
#line 65 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Uniform</p>\r\n");
#nullable restore
#line 69 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                    <td>");
#nullable restore
#line 71 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                   Write(eq.SERIAL_NO);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n");
#nullable restore
#line 73 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                         if (eq.EQUIPMENT_TYPE_ID == 1)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Assault Rifle</p>\r\n");
#nullable restore
#line 76 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else if (eq.EQUIPMENT_TYPE_ID == 2)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>SMG</p>\r\n");
#nullable restore
#line 80 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else if (eq.EQUIPMENT_TYPE_ID == 3)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Sniper</p>\r\n");
#nullable restore
#line 84 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else if (eq.EQUIPMENT_TYPE_ID == 4)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Melee</p>\r\n");
#nullable restore
#line 88 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else if (eq.EQUIPMENT_TYPE_ID == 5)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Others</p>\r\n");
#nullable restore
#line 92 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                    <td>");
#nullable restore
#line 94 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                   Write(eq.STORAGE_LOCATION);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>Armstoke ");
#nullable restore
#line 95 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                            Write(eq.STORAGE_BIN);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 96 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                   Write(eq.BOX_LOT_NO);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 97 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                   Write(eq.BUTT_NO);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 98 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                   Write(eq.QUANTITY);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n");
#nullable restore
#line 100 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                         if (eq.STATUS == 0)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Unavailable</p>\r\n");
#nullable restore
#line 103 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else if (eq.STATUS == 1)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Available</p>\r\n");
#nullable restore
#line 107 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else if (eq.STATUS == 2)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Assigned</p>\r\n");
#nullable restore
#line 111 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Loaned</p>\r\n");
#nullable restore
#line 115 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 119 "C:\Users\18015081\Source\Repos\SmarterTomorrowTest\SmarterTomorrow\Views\StockTake\ViewEquipment.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n\r\n\r\n    </table>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<stocktake_equipment>> Html { get; private set; }
    }
}
#pragma warning restore 1591