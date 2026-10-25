using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.EquipoDTOs;
using Models.DTOs.PagoDTOs;
using Models.DTOs.TipoGastosDTOs;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;

namespace MVC.Controllers
{
    public class PagoController : Controller
    {

        public string urlBase;

        public string urlTipoGasto;

        public PagoController(IConfiguration configuration)
        {
            urlBase = configuration.GetValue<string>("UrlBase") + "PagoWebApi";
            urlTipoGasto = configuration.GetValue<string>("UrlBase") + "TipoGastoWebApi";
        }


        public IActionResult ObtenerEquipos()
        {

            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol == 2)
            {// solo gerente 
                return View();
            }
            TempData["Error"] = "No tiene permisos para acceder a esta sección.";
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult ObtenerEquipos(decimal monto)
        {
            IEnumerable<ListadoEquipoDTO> equipos = new List<ListadoEquipoDTO>();

            int? id = HttpContext.Session.GetInt32("UsuarioId");
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            string token = HttpContext.Session.GetString("Token");
            if (rol == 2) // solo gerente 
            {
                try
                {

                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea = cliente.GetAsync(urlBase + "/EncontrarEquipo/" + monto);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                        contenidoRespuesta.Wait();
                        string datos = contenidoRespuesta.Result;
                        equipos = JsonConvert.DeserializeObject<IEnumerable<ListadoEquipoDTO>>(datos);
                        if (!equipos.Any())
                        {
                            ViewBag.Mensaje = "No se encontraron equipos con pagos mayores a ese monto";
                        }

                    }
                    else
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                        contenidoRespuesta.Wait();
                        ViewBag.Mensaje = contenidoRespuesta.Result;
                    }


                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error";
                }
                return View(equipos);
            }
            else
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult ObtenerPagos()
        {
            IEnumerable<ListadoPagoDTO> pagos = new List<ListadoPagoDTO>();

            int? id = HttpContext.Session.GetInt32("UsuarioId");
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            string token = HttpContext.Session.GetString("Token");
            if (rol == 2 || rol == 3) // solo gerente o Empleado
            {
                try
                {

                    HttpClient cliente = new HttpClient();

                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    Task<HttpResponseMessage> tarea = cliente.GetAsync(urlBase);
                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                        contenidoRespuesta.Wait();
                        string datos = contenidoRespuesta.Result;
                        pagos = JsonConvert.DeserializeObject<IEnumerable<ListadoPagoDTO>>(datos);

                    }
                    else
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                        contenidoRespuesta.Wait();
                        ViewBag.Mensaje = contenidoRespuesta.Result;
                    }


                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error";
                }
                return View(pagos);
            }
            else
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }


        }


        // GET: PagoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PagoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //GET Listado de Pagos
        [HttpGet]
        public ActionResult ListadoPago()
        {

            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol != 2) // solo gerente
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            return View(new List<ListadoPagoDTO>());
        }


        //*************PAGO-UNICO


        // GET: PagoController/Create
        public ActionResult CreatePagoUnico()
        {

            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            string token = HttpContext.Session.GetString("Token");
            if (rol != 2 && rol != 1 && rol != 3) // cualquier ROL
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }

            int? idLogueado = HttpContext.Session.GetInt32("UsuarioId"); //ID DE USUARIO QUE CREA EL PAGO
            PagoUnicoDTO pagoUnicoDTO = new PagoUnicoDTO();

            /*pagoUnicoDTO.Gasto = CUListadoTipoGastos.Ejecutar();*/

                IEnumerable<ListadoTipoGastoDTO> gastos = new List<ListadoTipoGastoDTO>();
            try
            {

                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea = cliente.GetAsync(urlTipoGasto);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    string datos = contenidoRespuesta.Result;
                    gastos = JsonConvert.DeserializeObject<List<ListadoTipoGastoDTO>>(datos);
                    tarea.Wait();
                    pagoUnicoDTO.Gasto = gastos;
                    pagoUnicoDTO.UsuarioId = (int)idLogueado;
                    return View(pagoUnicoDTO);

                }
                else
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    ViewBag.Mensaje = contenidoRespuesta.Result;
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoUnico(PagoUnicoDTO pagoUnicoDTO)
        {
            int? idLogueado = HttpContext.Session.GetInt32("UsuarioId");
            string token = HttpContext.Session.GetString("Token");
            List<ListadoTipoGastoDTO> gastos = new List<ListadoTipoGastoDTO>();
            try
            {

                HttpClient cliente = new HttpClient();

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea = cliente.GetAsync(urlTipoGasto);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    string datos = contenidoRespuesta.Result;
                    gastos = JsonConvert.DeserializeObject<List<ListadoTipoGastoDTO>>(datos);

                    pagoUnicoDTO.Gasto = gastos;
                    pagoUnicoDTO.UsuarioId = (int)idLogueado;

                }
                else
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    ViewBag.Mensaje = contenidoRespuesta.Result;
                }



                if (ModelState.IsValid)
                {
                    HttpClient cliente1 = new HttpClient();

                    Task<HttpResponseMessage> tarea1 = cliente.PostAsJsonAsync(urlBase + "/unico", pagoUnicoDTO);
                    tarea.Wait();
                    HttpResponseMessage respuesta1 = tarea.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Pago realizado correctamente";
                        return RedirectToAction(nameof(ObtenerPagos));
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest ||
                        (int)respuesta.StatusCode == StatusCodes.Status409Conflict)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        body.Wait();
                        string datos = body.Result;
                        ViewBag.Mensaje = datos;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(pagoUnicoDTO);
        }














        //*************PAGO-RECURRENTE



        // GET: PagoController/Create
        public ActionResult CreatePagoRecurrente()
        {


            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            string token = HttpContext.Session.GetString("Token");
            if (rol != 2 && rol != 1 && rol != 3) // cualquier ROL
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }

            int? idLogueado = HttpContext.Session.GetInt32("UsuarioId"); //ID DE USUARIO QUE CREA EL PAGO
            PagoRecurrenteDTO pagoRecurrenteDTO = new PagoRecurrenteDTO();
            /*pagoUnicoDTO.Gasto = CUListadoTipoGastos.Ejecutar();*/
            List<ListadoTipoGastoDTO> gastos = new List<ListadoTipoGastoDTO>();
            try
            {
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea = cliente.GetAsync(urlTipoGasto);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    string datos = contenidoRespuesta.Result;
                    gastos = JsonConvert.DeserializeObject<List<ListadoTipoGastoDTO>>(datos);
                    //tarea.Wait();
                    pagoRecurrenteDTO.Gasto = gastos;
                    pagoRecurrenteDTO.UsuarioId = (int)idLogueado;
                    return View(pagoRecurrenteDTO);

                }
                else
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    ViewBag.Mensaje = contenidoRespuesta.Result;
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View();


        }




        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePagoRecurrente(PagoRecurrenteDTO pagoRecurrenteDTO)
        {

            int? idLogueado = HttpContext.Session.GetInt32("UsuarioId");
            string token = HttpContext.Session.GetString("Token");
            List<ListadoTipoGastoDTO> gastos = new List<ListadoTipoGastoDTO>();
            try
            {

                HttpClient cliente = new HttpClient();

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Task<HttpResponseMessage> tarea = cliente.GetAsync(urlTipoGasto);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    string datos = contenidoRespuesta.Result;
                    gastos = JsonConvert.DeserializeObject<List<ListadoTipoGastoDTO>>(datos);
                    
                    pagoRecurrenteDTO.Gasto = gastos;
                    pagoRecurrenteDTO.UsuarioId = (int)idLogueado;

                }
                else
                {
                    HttpContent contenido = respuesta.Content;
                    Task<string> contenidoRespuesta = contenido.ReadAsStringAsync();
                    contenidoRespuesta.Wait();
                    ViewBag.Mensaje = contenidoRespuesta.Result;
                }



                if (ModelState.IsValid)
                {
                    HttpClient cliente1 = new HttpClient();

                    Task<HttpResponseMessage> tarea1 = cliente.PostAsJsonAsync(urlBase + "/recurrente", pagoRecurrenteDTO);
                    tarea.Wait();
                    HttpResponseMessage respuesta1 = tarea.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Pago realizado correctamente";
                        return RedirectToAction(nameof(ObtenerPagos));
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest ||
                        (int)respuesta.StatusCode == StatusCodes.Status409Conflict)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        body.Wait();
                        string datos = body.Result;
                        ViewBag.Mensaje = datos;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View(pagoRecurrenteDTO);

        }






    }
}