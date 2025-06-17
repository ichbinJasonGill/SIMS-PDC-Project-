using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Text;

namespace SIMS__PDC_Project_.Models
{
    public class SupabaseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl = "https://kecmropialgzazhgzeto.supabase.co";
        private readonly string _apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImtlY21yb3BpYWxnemF6aGd6ZXRvIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDk1NjU0MTksImV4cCI6MjA2NTE0MTQxOX0.ozf3CPy-gmB1bobw-D707xClbNDSdMpZSzYlw07l2VY";

        public SupabaseService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", _apiKey);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            var response = await _httpClient.GetAsync($"{_supabaseUrl}/rest/v1/students");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Student>>(json);
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            var jsonContent = JsonConvert.SerializeObject(student);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_supabaseUrl}/rest/v1/Student", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return null; // null means success

            return responseBody; // return error message








            //if (!ModelState.IsValid)
            //{
            //    // Refill campus dropdown so it's not null again
            //    s.CampusOptions = new List<SelectListItem>
            //    {
            //        new SelectListItem { Text = "Main Campus", Value = "Main Campus" },
            //        new SelectListItem { Text = "City Campus", Value = "City Campus" }
            //    };

            //    return View(s);
            //}

            //var result = await _supabaseService.AddStudentAsync(s);

            //if (result == null)
            //{
            //    TempData["Message"] = "Student added successfully!";
            //    return RedirectToAction("Index");
            //}

            //// If there's an error:
            //ModelState.AddModelError("", $"Failed to add student. Supabase says: {result}");

        }





    }

}