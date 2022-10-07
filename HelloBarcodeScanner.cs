using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

// Create a HTTP Client
var client = new HttpClient();
var baseUrl = @"https://api.accusoft.com";
client.BaseAddress = new Uri(baseUrl);
var apiKey = System.Environment.GetEnvironmentVariable("ACCUSOFT_CLOUD_KEY") ?? "YourAPIKeyHere...";
client.DefaultRequestHeaders.Add("acs-api-key", apiKey);

// Upload image which contains the barcodes we wish to scan
var workfileApi = @"/PCCIS/V1/WorkFile?FileExtension=bmp";
var content = new StreamContent(File.OpenRead(@"./testimage.bmp"));
content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");
var response = await client.PostAsync(workfileApi, content);

// Extract fileId from the upload response.
string responseBody = await response.Content.ReadAsStringAsync();
var responseData = JsonNode.Parse(responseBody)!;
string fileId = responseData["fileId"]!.ToString();

// Build JSON for our process request
var requestJson = new
{
    input = new
    {
        source = new
        {
            fileId
        },
        barcodeTypes = new[]
        {
            "code39Barcode"
        }
    }
};

// Start barcode scanner process
var api = "/barcode/api/v1/scanners";
var processRequestContent = new StringContent(JsonSerializer.Serialize(requestJson), Encoding.UTF8, "application/json");
var processRequest = await client.PostAsync(api, processRequestContent); 

// After our POST request, the response should contain a processId that we will use later.
string processRequestResponse = await processRequest.Content.ReadAsStringAsync();
string processId = JsonNode.Parse(processRequestResponse)!["processId"]!.ToString();

// Wait for the process to be complete
string processStatusResponse = await client.GetStringAsync(api + "/" + processId);
while (JsonNode.Parse(processStatusResponse)!["state"]!.ToString() == "processing")
{
    Thread.Sleep(1000);
    processStatusResponse = await client.GetStringAsync(api + "/" + processId);
}

// Beautify Output Json

var responseObject = JsonNode.Parse(processStatusResponse);
var options = new JsonSerializerOptions(){
    WriteIndented = true
};
var formattedJson = JsonSerializer.Serialize(responseObject, options);

Console.Write(formattedJson);


