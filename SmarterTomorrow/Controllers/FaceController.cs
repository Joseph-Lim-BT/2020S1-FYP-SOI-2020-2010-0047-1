using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SmarterTomorrow.Models;

namespace SmarterTomorrow.Controllers
{
    public class FaceController : Controller
    {
        private IWebHostEnvironment _env;
        const string faceApiKey = "150e5f7f2f304442a0dd80bb15851a5b";
        const string faceApiEndPoint = "https://southeastasia.api.cognitive.microsoft.com/face/v1.0";

        private const string LOGIN_SQL =
 @"SELECT * FROM usr
            WHERE NRIC = '{0}'";
        private const string LOGIN_SQLFACE =
 @"SELECT * FROM usr
            WHERE PERSON_ID= '{0}'";
        private const string LASTLOGIN_SQL =
   @"UPDATE usr SET DTE_TIME_LAST_LOGIN=GETDATETIME() WHERE NRIC='{0}'";


        private const string ROLE_COL = "GROUP_ID";
        private const string NAME_COL = "NRIC";

        private const string REDIRECT_CNTR = "Loan";
        private const string REDIRECT_ACTN = "Index";

        private const string LOGIN_VIEW = "UserLogin";
        public FaceController(IWebHostEnvironment environment)
        {
            _env = environment;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public string FaceLogin(IFormFile upimage)
        {
            string fullpath = Path.Combine(_env.WebRootPath, "login/user.jpg");
            using (FileStream fs = new FileStream(fullpath, FileMode.Create))
            {
                upimage.CopyTo(fs);
                fs.Close();
            }

            string imagePath = @"/login/user.jpg";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", faceApiKey);
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";
            string requestParameters2 = "returnFaceId=true&returnFaceRectangle=false";
            string uri = faceApiEndPoint + "/detect?" + requestParameters;
            string uri2 = faceApiEndPoint + "/detect?" + requestParameters2;
            string uriIdentify = faceApiEndPoint + "/identify";
            var fileInfo = _env.WebRootFileProvider.GetFileInfo(imagePath);
            var byteData = GetImageAsByteArray(fileInfo.PhysicalPath);
            string contentStringFace = string.Empty;


            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                // Execute the REST API call.
                var response = client.PostAsync(uri2, content).Result;


                // Get the JSON response.
                contentStringFace = response.Content.ReadAsStringAsync().Result;

            }
            string faceId = contentStringFace.Substring(12, 36);
            string str = "{\"personGroupId\": \"supervisor\", \"faceIds\": [\"" + faceId + "\"]}";
            var buffer = System.Text.Encoding.UTF8.GetBytes(str);
            using (ByteArrayContent content2 = new ByteArrayContent(buffer))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".

                content2.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Execute the REST API call.
                var response2 = client.PostAsync(uriIdentify, content2).Result;


                // Get the JSON response.
                contentStringFace = response2.Content.ReadAsStringAsync().Result;

            }
            string personId = contentStringFace.Substring(77, 36);
            TempData["json"] = personId;
            TempData.Keep("json");

            return personId;
        }

        public IActionResult Next()
        {
            return View();
        }

        public IActionResult Fail()
        {
            TempData["Message"] = "There is no face associated with any user in our database. Please try again.";
            TempData["MsgType"] = "warning";
            return View();
        }

        public IActionResult Pass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Next(string personID)
        {

            if (!AuthenticateUserFace(personID, out ClaimsPrincipal principal))
            {

                ViewData["Message"] = "You are not an authorised user. Please try again.";
                ViewData["MsgType"] = "warning";
                return View("Index");

            }
            else
            {

                ViewData["Message"] = "You are not an authorised user. Please try again.";
                ViewData["MsgType"] = "warning";
                HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   principal);
                if (TempData["returnUrl"] != null)
                {
                    string returnUrl = TempData["returnUrl"].ToString();
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                }

                return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);


            }
        }
        private bool AuthenticateUserFace(string personId, out ClaimsPrincipal principal)
        {
            principal = null;

            DataTable ds = DBUtl.GetTable(LOGIN_SQLFACE, personId);
            if (ds.Rows.Count == 1)
            {
                principal =
                   new ClaimsPrincipal(
                      new ClaimsIdentity(
                         new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier,personId ),
                        new Claim(ClaimTypes.Name, ds.Rows[0][NAME_COL].ToString()),
                        new Claim(ClaimTypes.Role, ds.Rows[0][ROLE_COL].ToString())
                         }, "Basic"
                      )
                   );
                return true;
            }
            return false;
        }
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] bytes = binaryReader.ReadBytes((int)fileStream.Length);
            binaryReader.Close();
            fileStream.Close();
            return bytes;
        }

        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            string INDENT_STRING = "    ";
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < json.Length; i++)
            {
                var ch = json[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && json[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }


    }
}
