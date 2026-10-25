
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Models.DTOs.UsuarioDTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;

namespace MVC.Controllers
{
    public class UsuarioController : Controller
    {

        public string urlBase;

        public UsuarioController(IConfiguration configuration)
        {
            urlBase = configuration.GetValue<string>("UrlBase") + "UsuarioWebApi";
        }
        // GET: UsuarioController

        // GET: Usuario/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Sesión cerrada correctamente.";
            return RedirectToAction("Login", "Usuario");
        }



        // POST: Usuario/Login
        [HttpPost]
        public IActionResult Login(LoguinDTO loguinDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient cliente = new HttpClient();
                    Task<HttpResponseMessage> tarea = cliente.PostAsJsonAsync(urlBase + "/login", loguinDTO);

                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;
                    if (respuesta.IsSuccessStatusCode)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        string datos = body.Result;
                        // Deserializamos la respuesta a UsuarioLogueadoDTO
                        UsuarioLogueadoDTO userLog = JsonConvert.DeserializeObject<UsuarioLogueadoDTO>(datos);
                        if(userLog != null)
                        {
                            // Guardamos los datos en sesión
                            HttpContext.Session.SetInt32("UsuarioId", userLog.Id);
                            HttpContext.Session.SetString("UsuarioNombre", userLog.Nombre);
                            HttpContext.Session.SetInt32("UsuarioRol", userLog.Rol);
                            HttpContext.Session.SetString("Token", userLog.Token);

                            TempData["Success"] = $"¡Bienvenido {userLog.Nombre}!";

                            return RedirectToAction("Index", "Home");

                        }

                    }
                    else
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        string datos = body.Result;
                        ViewBag.Mensaje = datos;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error";
            }
            return View();

        }

        public IActionResult ModificarContrasenia()
        {
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol != 1) // solo  admin
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificarContrasenia(string email)
        {
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            string token = HttpContext.Session.GetString("Token");
            if (rol != 1) // solo  admin
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if(email != null)
                {
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea = cliente.PutAsync(urlBase +"/"+ email, null);

                    tarea.Wait();
                    HttpResponseMessage respuesta = tarea.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        body.Wait();
                        string datos = body.Result;
                        ViewBag.exito = datos;

                        return View();
                    }
                    else
                    {
                        HttpContent contenido = respuesta.Content;
                        Task<string> body = contenido.ReadAsStringAsync();
                        body.Wait();
                        string datos = body.Result;
                        ViewBag.error = datos;
                    }
                }
                else
                {
                    throw new ArgumentException("Error en los datos");
                }
            }
            catch (ArgumentException ex)
            {
                ViewBag.error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.error = "Error";
            }
          
            return View();
        }



      



        /*}
        catch (ArgumentNullException ex)
        {
            ViewBag.msg = ex.Message;
        }
        catch (Exception ex)
        {
            ViewBag.msg = "ERROR LOGIN: " + ex.Message;
        }

        return View(loguinDTO);
    }*/


        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //SOLAMENTE PODRAN CREAR ADMINISTRADORES-GERENTES
        //FILTRAR CON VARIABLES DE SESION

        // GET: UsuarioController/Create
        /*public ActionResult Create()
        {
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol != 2 && rol!= 1) // solo gerente o admin
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            usuarioDTO.Equipos = CUListadoEquipo.Ejecutar();
            usuarioDTO.Roles= CUListadoRol.Ejecutar();
            return View(usuarioDTO);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDTO usuarioDTO)
        {
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol != 2 && rol != 1) // solo gerente o admin
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                if (ModelState.IsValid && usuarioDTO.Rol != 0)
                {
                    CUAltaUsuario.Ejecutar(usuarioDTO);
                    TempData["Success"] = "Usuario creado correctamente.";
                    return RedirectToAction("Index", "Home");

                }
                else
                {

                    throw new UsuarioException("Verifique los datos, debe seleccionar Tipo de empleado");
                }
                
            }
            catch (UsuarioException ex)
            {
                ViewBag.msg = ex.Message;

            }catch (ArgumentNullException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Error";
            }
            usuarioDTO.Equipos = CUListadoEquipo.Ejecutar();
            usuarioDTO.Roles = CUListadoRol.Ejecutar();
            return View(usuarioDTO);
        }
            
        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
        //GET Listado de Pagos
        [HttpGet]
        public ActionResult MayorPago()
        {


            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol != 2) // solo gerente 
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            return View(new List<ObtenerUsuarioDTO>());
        }


        // POST: PagoController/Create
        [HttpPost]
        public ActionResult MayorPago(decimal monto)
        {
            int? rol = HttpContext.Session.GetInt32("UsuarioRol");
            if (rol != 2) // solo gerente 
            {
                TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                return RedirectToAction("Index", "Home");
            }
            if(monto <= 0)
            {
                ViewBag.Mensaje = "El monto debe ser mayor a 0.";
                return View(new List<ObtenerUsuarioDTO>());
            }
            try
            {
                IEnumerable<ObtenerUsuarioDTO> usuarios = CUUsuarioPago.Ejecutar(monto);

                if (usuarios == null || !usuarios.Any())
                {
                    ViewBag.Mensaje = "No existen usuarios con pagos mayores al indicado.";
                    return View(new List<ObtenerUsuarioDTO>());
                }

                return View(usuarios);
            }
            catch (ArgumentNullException)
            {
                ViewBag.Mensaje = "Ocurrió un error al obtener el listado de usuarios.";
                return View(new List<ObtenerUsuarioDTO>());
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Ocurrió un error al obtener el listado de usuarios.";
                return View(new List<ObtenerUsuarioDTO>());
            }
        }*/
    }
}

