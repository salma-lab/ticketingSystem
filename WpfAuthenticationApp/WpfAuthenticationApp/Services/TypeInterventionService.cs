using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WpfAuthenticationApp.Models; // Assure-toi d'avoir la bonne namespace

namespace WpfAuthenticationApp.Services
{
    public class TypeInterventionService
    {
        private readonly HttpClient _httpClient;

        public TypeInterventionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Méthode pour récupérer les types d'intervention
        public async Task<List<TypeIntervention>> GetTypesInterventionAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TypeIntervention>>("https://localhost:7046/api/TypesInterventionController");
            return response;
        }

        // Méthode pour ajouter un nouveau type d'intervention
        public async Task AddTypeInterventionAsync(TypeIntervention typeIntervention)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7046/api/TypesInterventionController", typeIntervention);
            response.EnsureSuccessStatusCode(); // Vérifie si la requête a réussi
        }
    }
}

