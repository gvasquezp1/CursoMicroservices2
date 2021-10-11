using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.CarritoCompra.RemoteInterface;
using TiendaServicios.CarritoCompra.RemoteModel;

namespace TiendaServicios.CarritoCompra.RemoteServices
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory httpclient,ILogger<LibrosService> logger)
        {
            _httpClient = httpclient;
            _logger = logger;
        }


        public async Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid id)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Libros");
                var response=await cliente.GetAsync($"api/Libreria/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<LibroRemote>(content, options);

                    return (true, result, "");
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message + ex.InnerException);
            }
        }
    }
}
