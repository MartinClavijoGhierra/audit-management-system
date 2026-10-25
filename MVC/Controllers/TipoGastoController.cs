using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.AuditoriaDTOs;
using Models.DTOs.TipoGastosDTOs;

namespace MVC.Controllers
{
    public class TipoGastoController : Controller
    {

        public string urlBase;

        public TipoGastoController(IConfiguration configuration)
        {
            urlBase = configuration.GetValue<string>("UrlBase") + "TipoGastoWebApi";
        }
        //PARA EL DELETE/ELIMINAR DEL CRUDS.. NO SERIA TODO DENTRO DEL MISMO EDIT"?
        //SINO EN EL DELETE/ELIMINAR, PONER SOLO LA OPCION DE DEJAR INACTIVO Y SACAR DEL EDIT LA OPCION DE MODIFICAR SI ES ACTIVO O NO.
        //Hacer nuevo DTO ? 




        // GET: TipoGastoController
        /* public ActionResult Index()
         {
             //VALIDACIONES DE SESION
             IEnumerable<ListadoTipoGastoDTO> gastos = new List<ListadoTipoGastoDTO>();
             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }
             try
             {
                 gastos = CUListadoTipoGastos.Ejecutar();
             }
             catch (Exception ex) 
             {
                 ViewBag.msg = "error";
             }
             return View(gastos);
         }

         // GET: TipoGastoController/Details/5
         public ActionResult Details(int id)

         {
             //voy a trabajar con LISTADOTIPOGASTOS pero no con lista, sino voy a reciclar ese DTO.

             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }
             ListadoTipoGastoDTO detalleGasto = new ListadoTipoGastoDTO();
             try
             {
                 detalleGasto = CUBuscarGasto.Ejecutar(id);
             }
             catch (TipoGastoException ex) 
             {
                 ViewBag.msg = ex.Message;

             }catch (Exception ex)
             {
                 ViewBag.msg = "error";
             }
             return View(detalleGasto);
         }





         // GET: TipoGastoController/Create
         public ActionResult Create()
         {
             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }
             return View();
         }

         // POST: TipoGastoController/Create
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create(TipoGastoDTO tipoGasto)
         {

             //int UserInt = HttpContext.Session.GetInt32("UsuarioRol", userLog.Rol);
             int? idLogueado = HttpContext.Session.GetInt32("UsuarioId");
             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }


             try
             {
                 if (ModelState.IsValid) 
                 {
                    ListadoTipoGastoDTO listadoTipoGastoDTO= CUAltaTipoGasto.Ejecutar(tipoGasto);
                     AuditoriaDTO auditoriaDTO = new AuditoriaDTO
                     {
                         UsuarioId = (int)idLogueado,
                         Accion="Create",
                         TipoGasto= listadoTipoGastoDTO.Id,

                     };
                     CUAltaAuditoria.Ejecutar(auditoriaDTO);

                     return RedirectToAction("Index", "TipoGasto") ;
                 }

             }
             catch (TipoGastoException ex) 
             {
                     ViewBag.msg = ex.Message;

             }
             catch (ArgumentException e)
             {
                 ViewBag.msg = e.Message;
             }
             catch (Exception ex) 
             {
                 ViewBag.msg = "Error";
             }
             return View();
         }





         // GET: TipoGastoController/Edit/5
         public ActionResult Edit(int id)
         {
             ListadoTipoGastoDTO listadoGastoDTO = new ListadoTipoGastoDTO();
             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }


             try
             {

                 listadoGastoDTO = CUBuscarGasto.Ejecutar(id);

             }
             catch (TipoGastoException ex)
             {
                 ViewBag.msg=ex.Message;
             }catch(Exception ex)
             {
                 ViewBag.msg = "Error";
             }
             return View(listadoGastoDTO);
         }

         // POST: TipoGastoController/Edit/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(int id, ListadoTipoGastoDTO listadoTipoGastoDTO)
         {
             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }
             try

             {

                 int? idLogueado = HttpContext.Session.GetInt32("UsuarioId");

                 if (id>0 && ModelState.IsValid)
                 {
                     CUModificarGasto.Ejecutar(id, listadoTipoGastoDTO);


                     AuditoriaDTO auditoriaDTO = new AuditoriaDTO
                     {
                         UsuarioId = (int)idLogueado,
                         Accion="Edit",
                         TipoGasto = id,
                     };
                     CUAltaAuditoria.Ejecutar(auditoriaDTO);

                     return RedirectToAction(nameof(Index));
                 }
                 else
                 {
                     throw new ArgumentException("Error en datos");
                 }
             }
             catch (TipoGastoException ex)
             {
                 ViewBag.msg = ex.Message;
             }catch(ArgumentException e)
             {
                 ViewBag.msg = e.Message;
             }catch(Exception exx)
             {
                 ViewBag.msg = "Error";
             }
             return View(listadoTipoGastoDTO);
         }








         // GET: TipoGastoController/Delete/5
         public ActionResult Delete(int id)
         {
             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }
             ListadoTipoGastoDTO gasto = new ListadoTipoGastoDTO();
             try
             {
                 gasto = CUBuscarGasto.Ejecutar(id);
             }catch (Exception ex)
             {
                 ViewBag.msg = "Error";
             }
             return View(gasto);
         }




         //AUN NO CREO VISTA
         //PENSAR SI CORRESPONDE ELIMINAR O SOLO MODIFICAR CON EL EDITE EL ESTADO.

         // POST: TipoGastoController/Delete/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Delete(int id, ListadoTipoGastoDTO listadoTipoGastoDTO)
         {
             int? rol = HttpContext.Session.GetInt32("UsuarioRol");
             if (rol != 1) // solo Administrador
             {
                 TempData["Error"] = "No tiene permisos para acceder a esta sección.";
                 return RedirectToAction("Index", "Home");
             }
             try
             {
                 int? idLogueado = HttpContext.Session.GetInt32("UsuarioId");


                 if (id>0)
                 {


                     CUEliminarGasto.Ejecutar(id);
                     AuditoriaDTO auditoriaDTO = new AuditoriaDTO
                     {
                         UsuarioId =(int)idLogueado,
                         Accion = "Delete",
                         TipoGasto = id,
                     };
                     CUAltaAuditoria.Ejecutar(auditoriaDTO);



                     return RedirectToAction(nameof(Index));

                 }
                 else
                 {
                     throw new ArgumentException("El id no es correcto");
                 }
             }
             catch(TipoGastoException ex)
             {
                 ViewBag.msg = ex.Message;
             }
             catch (ArgumentException ex)
             {
                 ViewBag.msg = ex.Message;
             }
             catch (Exception ex)
             {
                ViewBag.msg = "Error";
             }
             return View(listadoTipoGastoDTO);
         }*/
    }
}
