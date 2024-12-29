using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Artokai.AOC.CliTool.Utils;

public class AocClient {
    private string? _sessionCookie;

    public AocClient(IConfiguration configuration)
    {
        _sessionCookie = configuration["SessionCookie"];;
    }

    public async Task<Stream?> GetAsync(string relativeUrl)
    {        
        if (string.IsNullOrEmpty(_sessionCookie)) {
            throw new CliToolException(
                "SessionCookie cookie not found in configuration.",
                "Please copy the session cookie from your browser to the configuration."
            );
        }

        var baseUri = new Uri("https://adventofcode.com");
        var handler = new HttpClientHandler();
        handler.CookieContainer = new CookieContainer();
        handler.CookieContainer.Add(baseUri, new Cookie("session", _sessionCookie));
        var client = new HttpClient(handler);
        client.BaseAddress = baseUri;

        var response = await client.GetAsync(relativeUrl);
        if (!response.IsSuccessStatusCode) {            
            return null;
        }
        return  await response.Content.ReadAsStreamAsync();
    }

    public async Task<Stream?> FetchInputAsync(int year, int day) => await GetAsync($"/{year}/day/{day}/input");

    public async Task<Dictionary<string,string>?> FetchPuzzleMetadataAsync(int year, int day) {
        using var stream = await GetAsync($"/{year}/day/{day}");
        if (stream == null) { return null; }

        using var sr = new StreamReader(stream);
        var contents = sr.ReadToEnd();

        var metadata = new Dictionary<string, string>();
        metadata["YYYY"] = year.ToString();
        metadata["D"] = day.ToString();
        metadata["DD"] = day.ToString("D2");
        metadata["TITLE"] = Regex.Match(contents, @"<h2>---\s*Day\s+\d+:\s+(.+)\s+---</h2>").Groups[1].Value;
        return metadata;
    } 
}