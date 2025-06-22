using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Supabase_Example.Services
{
    public class Supabase_DB_Services
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl = "https://rzoxldbppzaupntqntvf.supabase.co"; 
        private readonly string _apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InJ6b3hsZGJwcHphdXBudHFudHZmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDk1Mjc4NzQsImV4cCI6MjA2NTEwMzg3NH0.qg5q031KhVTE6z_eRUpns7qsyWMjRh3zX0Uvu22z7vU"; 
        private readonly JsonSerializerOptions _jsonOptions;

        public Supabase_DB_Services()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_supabaseUrl);
            _httpClient.DefaultRequestHeaders.Add("apikey", _apiKey);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<(bool Success, string ErrorMessage)> AddAsync<T>(T data, string table)
        {
            try
            {
                //string json = JsonSerializer.Serialize(data, _jsonOptions);
                var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"/rest/v1/{table}", content);

                if (response.IsSuccessStatusCode)
                    return (true, null);

                string error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(List<T> Data, string ErrorMessage)> GetAsync<T>(string table)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/rest/v1/{table}?select=*");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);
                    return (data, null);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return (null, error);
                }
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(T Data, string ErrorMessage)> GetByIdAsync<T>(string table, string column, string value)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/rest/v1/{table}?{column}=eq.{value}&select=*");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);

                    //if (list != null && list.Count > 0)
                    return (list[0], null);  // Return first (and usually only) match
                    //else
                    //    return (default, "No record found.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return (default, error);
                }
            }
            catch (Exception ex)
            {
                return (default, ex.Message);
            }
        }

        public async Task<(T Data, string ErrorMessage)> LoginAsync<T>(string table, string usernameColumn, string usernameValue, string passwordColumn, string passwordValue)
        {
            try
            {
                string requestUri = $"/rest/v1/{table}?{usernameColumn}=eq.{Uri.EscapeDataString(usernameValue)}&{passwordColumn}=eq.{Uri.EscapeDataString(passwordValue)}&select=*";

                var response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);

                    if (list != null && list.Count > 0)
                        return (list[0], null); // Successful match
                    else
                        return (default, "Invalid username or password.");
                }

                var error = await response.Content.ReadAsStringAsync();
                return (default, $"API error: {response.StatusCode} - {error}");
            }
            catch (Exception ex)
            {
                return (default, $"Exception: {ex.Message}");
            }
        }


        public async Task<(List<T> Data, string ErrorMessage)> GetByFilterAsync<T>(string table, string column, string value)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/rest/v1/{table}?{column}=eq.{value}&select=*");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);

                    return (list, null); // Could be empty list if no matches
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return (null, error);
                }
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }



        public async Task<(bool Success, string ErrorMessage)> UpdateAsync<T>(string table, string matchColumn, string matchValue, T updatedData)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(updatedData, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"/rest/v1/{table}?{matchColumn}=eq.{matchValue}")
                {
                    Content = content
                };
                request.Headers.Add("Prefer", "return=representation");

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return (true, null);

                string error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(string table, string matchColumn, string matchValue)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"/rest/v1/{table}?{matchColumn}=eq.{matchValue}");
                request.Headers.Add("Prefer", "return=representation");

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return (true, null);

                string error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
