using API_WeChip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace API_WeChip.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfertaController : ControllerBase
    {
        private List<Oferta> ofertas = new List<Oferta>();

        [HttpGet]
        public IEnumerable<Oferta> Get()
        {
            ofertas.AddRange( new[] { 
                new Oferta(1,9, new List<int>( new[] { 15, 106 }), "01234567890"),
                new Oferta(2,7, null, "12020889617"),
                new Oferta(3,21, null, "78512365498"),
                new Oferta(4,9, new List<int>( new [] { 1108, 314 }), "15842369885"),
                new Oferta(4,15, null, "15842369885")
            });

            return ofertas.ToArray();
        }
    }
}
