﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SatisServer.UI.Data;
using SatisServer.UI.Data.API;
using SatisServer.UI.Data.Enums;

namespace SatisServer.UI.Logic;

/// <summary>API client for the dedicated server.</summary>
public sealed class DedicatedServerApiClient
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    private static DedicatedServerApiClient? _instance;
    public static DedicatedServerApiClient Instance => _instance ??= new DedicatedServerApiClient(SatisConfig.Instance.GetBaseUrl(), true);

    /// <summary>Constructor</summary>
    public DedicatedServerApiClient(string baseUrl, bool acceptInvalidCertificate = false)
    {
        var handler = new HttpClientHandler();
        if (acceptInvalidCertificate)
        {
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        }

        _httpClient = new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
    }

    /// <summary>Set the authentication token to use for requests.</summary>
    public void SetAuthToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    /// <summary>Sends a request to the server.</summary>
    /// <remarks>Throws an exception if the request fails.</remarks>
    /// <param name="function">The function to call.</param>
    /// <param name="data">The data to send.</param>
    /// <exception cref="ApiException">Thrown if the request fails.</exception>
    /// <exception cref="JsonException">Thrown if the response cannot be deserialized.</exception>
    /// <exception cref="HttpRequestException">Thrown if the request fails.</exception>
    /// <returns>The response data.</returns>
    internal async Task<T> SendRequest<T>(ApiCallName function, object? data)
    {
        var request = new ApiRequest { Function = function.ToString(), Data = data };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("", content);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ApiResponse<object>>(errorContent, _jsonOptions);
            HandleApiError.HandleError(errorResponse);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(responseContent))
        {
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, _jsonOptions)
                ?? throw new ApiException("unexpected_response", "Unexpected response content.");

            if (apiResponse.ErrorCode != null)
            {
                throw new ApiException(apiResponse.ErrorCode, $"API Error: {apiResponse.ErrorMessage}");
            }

            return apiResponse.Data!;
        }

        return response.StatusCode switch
        {
            System.Net.HttpStatusCode.OK
                or System.Net.HttpStatusCode.Created
                or System.Net.HttpStatusCode.Accepted
                or System.Net.HttpStatusCode.NoContent => default!,// These are all considered successful outcomes
            _ => throw new ApiException("unexpected_response", $"Unexpected response status: {response.StatusCode}"),
        };
    }

    /// <summary>Downloads a file from the server.</summary>
    /// <remarks>Throws an exception if the request fails.</remarks>
    /// <param name="function">The function to call.</param>
    /// <param name="data">The data to send.</param>
    /// <exception cref="ApiException">Thrown if the request fails.</exception>
    /// <exception cref="JsonException">Thrown if the response cannot be deserialized.</exception>
    /// <exception cref="HttpRequestException">Thrown if the request fails.</exception>
    /// <returns>The response message.</returns>
    internal async Task<HttpResponseMessage> DownloadSave(ApiCallName function, object data)
    {
        var request = new ApiRequest { Function = function.ToString(), Data = data };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("", content);

        // Check for errors
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ApiResponse<object>>(errorContent, _jsonOptions);
            HandleApiError.HandleError(errorResponse);
        }

        // Check if the response is a file
        if (response.Content.Headers.ContentType?.MediaType != "application/octet-stream")
        {
            throw new ApiException("unexpected_response", "Expected a file response but received a different content type.");
        }

        return response;
    }

    /// <summary>Uploads a file to the server.</summary>
    /// <remarks>Throws an exception if the request fails.</remarks>
    /// <param name="function">The function to call.</param>
    /// <param name="data">The data to send.</param>
    /// <param name="fileStream">The file stream to upload.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <exception cref="ApiException">Thrown if the request fails.</exception>
    /// <exception cref="JsonException">Thrown if the response cannot be deserialized.</exception>
    /// <exception cref="HttpRequestException">Thrown if the request fails.</exception>
    internal async Task UploadSaveFile(ApiCallName function, object data, Stream fileStream, string fileName)
    {
        using var content = new MultipartFormDataContent();

        // Add the JSON part
        var apiRequest = new ApiRequest { Function = function.ToString(), Data = data };
        var jsonContent = new StringContent(JsonSerializer.Serialize(apiRequest), Encoding.UTF8, "application/json");
        content.Add(jsonContent, "data");

        // Add the file part
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(fileContent, "saveGameFile", fileName);

        // Send the request
        var response = await _httpClient.PostAsync("", content);

        // Check for errors
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ApiResponse<object>>(errorContent, _jsonOptions);
            HandleApiError.HandleError(errorResponse);
        }

        AppLog.Write($"Response Status: {response.StatusCode}");
        var responseContent = await response.Content.ReadAsStringAsync();
        AppLog.Write($"Response Content: {responseContent}");

        // Handle specific status codes
        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
            case System.Net.HttpStatusCode.Created:
            case System.Net.HttpStatusCode.Accepted:
            case System.Net.HttpStatusCode.NoContent:
                // These are all considered successful outcomes
                break;
            default:
                throw new ApiException("unexpected_response", $"Unexpected response status: {response.StatusCode}");
        }
    }
}