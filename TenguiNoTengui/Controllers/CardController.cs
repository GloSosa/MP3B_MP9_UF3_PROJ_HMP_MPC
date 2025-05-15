using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using TenguiNoTengui.Data;
using TenguiNoTengui.Models;

namespace TenguiNoTengui.Controllers
{
    [Route("Card")]
    public class CardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string apiKey = "36e2b3fb-417c-4549-8fd8-dbc112d7d14a";

        public CardController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // Obtener una carta aleatoria de la API
        [HttpGet("GetRandomCard")]
        public async Task<IActionResult> GetRandomCard()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var random = new Random();
            int randomPage = random.Next(1, 1000); // Número aleatorio entre 1 y 1000

            var response = await client.GetAsync($"https://api.pokemontcg.io/v2/cards?pageSize=1&page={randomPage}");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var cardResponse = JsonConvert.DeserializeObject<PokemonApiResponse>(json);

            return Json(cardResponse.Data.FirstOrDefault());
        }


        // Guardar carta como Tengui
        [HttpPost("Save")]
        public IActionResult Save([FromBody] CardDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CardId))
                return BadRequest();

            var card = new Card
            {
                CardId = dto.CardId,
                UserId = "demoUser" // Simulado por ahora
            };

            _context.Cards.Add(card);
            _context.SaveChanges();
            return Ok();
        }

        // Obtener todas las cartas guardadas del usuario
        [HttpGet("MyCards")]
        public IActionResult MyCards()
        {
            var cards = _context.Cards
                .Where(c => c.UserId == "demoUser")
                .ToList();

            return Json(cards);
        }

        // Obtener una carta específica desde la API
        [HttpGet("GetCard/{id}")]
        public async Task<IActionResult> GetCard(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var response = await client.GetAsync($"https://api.pokemontcg.io/v2/cards/{id}");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            
            // ✅ Esta es la corrección: usamos un objeto contenedor, no una lista
            var dataWrapper = JsonConvert.DeserializeObject<SingleCardApiResponse>(json);
            
            return Json(dataWrapper.Data);
        }


        [HttpGet("View")]
        public IActionResult ViewCards()
        {
            return View("MyCards");
        }

    }

    public class CardDto
    {
        public string CardId { get; set; }
    }
}
