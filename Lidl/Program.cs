using Nest;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

var settings = new ConnectionSettings(new Uri("http://arthur-deb:9200"))
    .DisableDirectStreaming()
    .BasicAuthentication("elastic","changeme")
    .DefaultIndex("lidl2"); // Remplacez "nom_de_votre_index" par le nom de votre index Elasticsearch
var client = new ElasticClient(settings);

var cookie = "inSession=true; OptanonAlertBoxClosed=2024-03-04T15:11:22.851Z; kameleoonVisitorCode=9yyyph32km7o62or; LidlID=88033f34-b79e-41b1-90c1-b0e0c35d616a; LidlIDu=true; FPID=FPID2.2.g8N9b3HRv15BTPep%2B8nI4cwmNJ0vvczKSrCkPk3UolE%3D.1709565084; mdLogger=false; kampyle_userid=40cd-6578-7780-76f7-1882-d1e8-8a48-7e39; _gcl_au=1.1.560692149.1709565084; tracking-info=eyJjdXN0b21lclN0YXR1cyI6Ik5FV19DVVNUT01FUiIsImFjY291bnRJZCI6ImMyMDc4NTA0LTkxMWYtNGY2OS04MGE2LWJkNzg4YmM2MTg5NCIsImZpcnN0TmFtZSI6IiIsImN1c3RvbWVyVHlwZSI6IlJFR0lTVEVSRUQifQ; lidlaccountclient-fr-language=fr-FR; XSRF-TOKEN=CfDJ8CVm8fVPDVZGlnoK2tljAHmRbPfKb_hjuKg87YU0BXtnbstjPf-c3LGDRzOt1EsL8IRKGDQj7DUvVr1xwIqySzlpls8hI414eyebSf6XQx2Z531jjXQBuhEx8OvdwicA_q-Q456Fu8bu2EuKSKHdwvTFfz6nlCDw2XjyTX4PnHVAH98sx1n_gtITFCWR9Y8Zvg; FPLC=43bxQHUQh6WKqkUU5jEpEg6oPb3aaxA%2BUdAeJ8UAjaW1XJRUi0Yvf2Ha4N5rLYi1NnqCpRSqdM%2FwMOHqoKiJaeCQjcDHv6rn6lj%2FuFLlVjO%2FpCMPsad%2Fsg%2FjKKBepg%3D%3D; FPGSID=1.1709891923.1709891923.G-MKXZEBQY7Y.DDgA-nOTNngI_1uUfU7H2Q; _gid=GA1.2.1801108432.1709891924; _dc_gtm_UA-66134079-13=1; ldi-user-context=eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6Ijg3NTQ1OTE5MzAwNTY5MDQzIiwiaXNzIjoibGlkbCIsImV4cCI6MTcwOTg5MzcyNH0.CirbaIlmfeT_M6DKqhCANrvD2wUiKQe9Fn2k07O1DjsuPtdk5nP63Wp-JE35aHBHkpGPQinzHIoVIn5WjbqbAQ; authToken=eyJhbGciOiJSUzI1NiIsImtpZCI6IjdBQkE4MkIzRTQ4Qjg4MUI5QTg4MDU3N0ZBQUIwMTNENjIwOEYxMDNSUzI1NiIsInR5cCI6IkpXVCIsIng1dCI6ImVycUNzLVNMaUJ1YWlBVjMtcXNCUFdJSThRTSJ9.eyJuYmYiOjE3MDk4OTE5MjMsImV4cCI6MTcwOTg5NTUyMywiaXNzIjoiaHR0cHM6Ly9hY2NvdW50cy5saWRsLmNvbSIsImF1ZCI6WyJMaWRsLkF1dGhlbnRpY2F0aW9uIiwiaHR0cHM6Ly9hY2NvdW50cy5saWRsLmNvbS9yZXNvdXJjZXMiXSwiY2xpZW50X2lkIjoiRnJhbmNlRWNvbW1lcmNlQ2xpZW50Iiwic3ViIjoiODc1NDU5MTkzMDA1NjkwNDMiLCJhdXRoX3RpbWUiOjE3MDk1NjUxMDksImlkcCI6ImxvY2FsIiwibGVnYWxfdGVybXMiOiJGUiIsInNpZCI6IjBBQTU4QUU3MkREMEMxRDdGNjdENTQyNTdFMzE4M0M0IiwiaWF0IjoxNzA5ODkxOTIzLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiTGlkbC5BdXRoZW50aWNhdGlvbiIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXX0.jgL-JBuGJQyJ1Gsde6aF4E-O5P-py6s8GW281DXbYrKDO9HJqNJ_R1uRNnKFJ59VGOWHEe7SbXkfOQtoK4miTTmAAKSFGWuB1WbbPWeGxBQ_iNhOvCVJC2rYBe6noUfZC6JnlmSq2gSgVt6lZ3oSwRfJwvh3UqtXK9lADXOFDbCs8LHfmsURNs40yP34CfKIgolSlpgbDwu2NrbsBRx6hlpiwFv0QECTduoTSYIiqx7d7TRRFJ5irKVR0UXNLee42K66zI17e-pEWdx3fXJGD_kTe72-f_JddKhN4gsI9NIcDnUKDYfsj-HqoYklxMRdqlnhly8rutcWVdRK97s5-A; ldi-customertoken=eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzc29faWQiOiI4NzU0NTkxOTMwMDU2OTA0MyIsImN1c3RvbWVyX251bWJlciI6IjE1MzE5NzA4MjciLCJjb3VudHJ5X2NvZGUiOiJGUiIsImN1c3RvbWVyX3N0YXR1cyI6Ik5FV19DVVNUT01FUiIsImN1c3RvbWVyX2VtYWlsIjoiY2VjaWxpYS50dXJrZWx0b25AZ21haWwuY29tIiwiY3VzdG9tZXJfdHlwZSI6IlJFR0lTVEVSRUQiLCJpc3MiOiJsaWRsIiwiZXhwIjoxNzA5ODk1NTIzfQ.MD_xqoQQ2qaEQFkJl4R6ObGQJ5Fz7dMnvRSN0NF1ARo6SAXq7PmOCINCAWwUOMGDgVygARiFv89XCRLY8vUelQ; UserVisits=current_visit_date:08.03.2024|last_visit_date:08.03.2024; SearchCollectorSession=FjJFfWS; OptanonConsent=isGpcEnabled=0&datestamp=Fri+Mar+08+2024+10%3A58%3A58+GMT%2B0100+(heure+normale+d%E2%80%99Europe+centrale)&version=202401.2.0&browserGpcFlag=0&isIABGlobal=false&hosts=&consentId=e6038d5a-2f5e-4584-b410-742916e2d212&interactionCount=1&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1%2CC0004%3A1&geolocation=FR%3BPAC&AwaitingReconsent=false; _ga_MKXZEBQY7Y=GS1.1.1709891923.2.1.1709891939.0.1.1229254127; _ga=GA1.2.1799179518.1709565084; kampyleUserSession=1709891939356; kampyleUserSessionsCount=18; kampyleSessionPageCounter=1; kampyleUserPercentile=24.163439663959018";

for (var i = 1; i < 10; i++)
{
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.lidl.fr/mre/api/v1/tickets?country=FR&page={i}");
    request.Method = "GET";
    request.Headers.Add("Accept", "application/json");
    request.Headers.Add("Cookie", cookie);

    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
    {
        using (Stream stream = response.GetResponseStream())
        {
            StreamReader reader = new StreamReader(stream);
            string responseFromServer = reader.ReadToEnd();

            if (responseFromServer != null)
            {
                var roots = JsonConvert.DeserializeObject<RootObject>(responseFromServer);

                foreach (var rootObject in roots.items)
                {
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create($"https://www.lidl.fr/mre/api/v1/tickets/{rootObject.id}?country=FR&languageCode=fr-FR");
                    request2.Method = "GET";
                    request2.Headers.Add("Accept", "application/json");
                    request2.Headers.Add("Cookie", cookie);

                    using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
                    {
                        using (Stream stream2 = response2.GetResponseStream())
                        {
                            StreamReader reader2 = new StreamReader(stream2, Encoding.UTF8);
                            string responseFromServer2 = reader2.ReadToEnd();


                            if (!string.IsNullOrEmpty(responseFromServer2))
                            {
                                var roots2 = JsonConvert.DeserializeObject<Root>(responseFromServer2);
                                var html = roots2.ticket.htmlPrintedReceipt;
                                string pattern = @"<span id=""purchase_list_line_\d+""[^>]*data-unit-price=""([^""]+)""[^>]*data-art-description=""([^""]+)""[^>]*>(.*?)</span>";
                                Regex regex = new Regex(pattern);
                                MatchCollection matches = regex.Matches(html);


                                foreach (Match match in matches)
                                {
                                    string unitPrice = match.Groups[1].Value;
                                    var text = match.Groups[3].Value.Split("  ")[0];

                                    text = match.Groups[2].Value;
                                    var date = roots2.ticket.date;

                                    var indexResponse = client.IndexDocument(new Ligne { Date = date, PrixUnitaire = Double.Parse(unitPrice), Libelle = text });
                                    if (indexResponse.IsValid)
                                        Console.WriteLine("Données indexées avec succès !");
                                    else
                                        Console.WriteLine($"Erreur lors de l'indexation : {indexResponse.DebugInformation}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

public class Ligne
{
    public DateTime Date { get; set; }
    public double PrixUnitaire { get; set; }
    public string Libelle { get; set; }
}