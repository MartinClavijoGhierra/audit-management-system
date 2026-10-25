using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.EquipoDTOs;
using Models.DTOs.PagoDTOs;
using Models.DTOs.TipoGastosDTOs;
using MVC.Models.DTOs.AuditoriaDTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;

namespace MVC.Controllers
{
    public class AuditoriaController : Controller
    {

        public string urlBase;
        public string urlTipoGasto;

        public AuditoriaController(IConfiguration configuration)
        {
            urlBase = configuration.GetValue<string>("UrlBase") + "AuditoriaWebApi/";

            urlTipoGasto = configuration.GetValue<string>("UrlBase")+ "TipoGastoWebApi";

        }


        // POST: AuditoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        
        public IActionResult ListadoAuditorias()
        {

            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol == 1)
            {// solo Administrador 
               
                string token = HttpContext.Session.GetString("Token");
               

                /*pagoUnicoDTO.Gasto = CUListadoTipoGastos.Ejecutar();*/
                IEnumerable<ListadoTipoGastoDTO> tipoGastos = new List<ListadoTipoGastoDTO>();
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
                        tipoGastos = JsonConvert.DeserializeObject<List<ListadoTipoGastoDTO>>(datos);                       
                        ViewBag.TiposGastos = tipoGastos;
                        return View();

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
            TempData["Error"] = "No tiene permisos para acceder a esta sección.";
            return RedirectToAction("Index", "Home");
        }




        [HttpPost]
        public IActionResult ListadoAuditorias(int idTipoGasto)
            {
            IEnumerable<ListadoAuditoriaDTO> auditorias = new List<ListadoAuditoriaDTO>();
            
            int? id = HttpContext.Session.GetInt32("UsuarioId");
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            string token = HttpContext.Session.GetString("Token");
            if (rol == 1) // solo Administrador 
            {
                try
                {


                    IEnumerable<ListadoTipoGastoDTO> tipoGastos = new List<ListadoTipoGastoDTO>();
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
                        tipoGastos = JsonConvert.DeserializeObject<List<ListadoTipoGastoDTO>>(datos);
                        ViewBag.TiposGastos = tipoGastos;


                        Task<HttpResponseMessage> tarea1 = cliente.GetAsync(urlBase + idTipoGasto);
                        tarea1.Wait();
                        HttpResponseMessage respuesta1 = tarea1.Result;
                        if (respuesta1.IsSuccessStatusCode)
                        {
                            HttpContent contenido1 = respuesta1.Content;
                            Task<string> contenidoRespuesta1 = contenido1.ReadAsStringAsync();
                            contenidoRespuesta1.Wait();
                            string datos1 = contenidoRespuesta1.Result;
                            auditorias = JsonConvert.DeserializeObject<IEnumerable<ListadoAuditoriaDTO>>(datos1);
                            if (!auditorias.Any())
                            {
                                ViewBag.Mensaje = "No se encontraron Auditorias para ese Tipo de Gasto";
                            }

                        }
                        else
                        {
                            HttpContent contenido1 = respuesta1.Content;
                            Task<string> contenidoRespuesta1 = contenido1.ReadAsStringAsync();
                            contenidoRespuesta1.Wait();
                            ViewBag.Mensaje = contenidoRespuesta1.Result;
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
                return View(auditorias);
            }
            else
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }

        }


    }
}