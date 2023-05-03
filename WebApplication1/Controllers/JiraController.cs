using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JiraController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateJiraIssue(System.String input)
        {
            string apiUrl = "https://aswecomp680.atlassian.net/rest/api/2/issue/";
            string JIRA_AUTH_TOKEN = "ATATT3xFfGF0vv7_KSrnnufIahHny77O7zre-N4zYwKmOmE-IhhNXdkS_KSfosfnrLs7erUcHkF4zwSLExx6JAjt4DgeQZ56WYF7P1DhDh5h0beSs60dBWgm78ltwzuHuoCTvKXQnmxZpQAo7QS8KHVU2G_M98RTcxZBEuM6BSzm7Ef3PHIROsE=9BF1C64C";
            
            var issueData = new
            {
                fields = new
                {
                    project = new
                    {
                        key = "JIR"
                    },
                    summary = "Test JIRA",
                    description = "This is an example JIRA issue created using REST API",
                    issuetype = new
                    {
                        name = "Task"
                    }
                }
            };

            string issueJson = JsonConvert.SerializeObject(issueData);

            var request = WebRequest.Create(apiUrl) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";            
            request.Headers["Authorization"] =  GetJiraAuthToken();


            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(issueJson);
            }

            using (var response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return Ok();
                }
                else
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string errorMessage = reader.ReadToEnd();
                    }
                    return StatusCode((int)response.StatusCode);
                }
            }
        }

        private string GetJiraAuthToken()
        {
            string username = "nikhila.bikki.205@my.csun.edu";
            string password = "ATATT3xFfGF0vv7_KSrnnufIahHny77O7zre-N4zYwKmOmE-IhhNXdkS_KSfosfnrLs7erUcHkF4zwSLExx6JAjt4DgeQZ56WYF7P1DhDh5h0beSs60dBWgm78ltwzuHuoCTvKXQnmxZpQAo7QS8KHVU2G_M98RTcxZBEuM6BSzm7Ef3PHIROsE=9BF1C64C";
            string authString = $"{username}:{password}";
            byte[] authBytes = Encoding.UTF8.GetBytes(authString);
            string base64AuthString = Convert.ToBase64String(authBytes);
            return $"Basic {base64AuthString}";
        }

    }
}
