using CasosDeUso.DTOs.TipoGastosDTOs;
using LogicaNegocio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mappers
{
    public class MapperTipoGasto
    {
        public static TipoGasto TipoGastoDTOToTipoGasto(TipoGastoDTO tipoGastoDTO)
        {
            return new TipoGasto(tipoGastoDTO.Nombre, tipoGastoDTO.Description);//falta agregar que esta activo?
        }


        public static IEnumerable<ListadoTipoGastoDTO> ListadoTipoGastoToListadoTipoGastoDTO(IEnumerable<TipoGasto> TipoGasto)
        {
            List<ListadoTipoGastoDTO> listadoTipoGasto = new List<ListadoTipoGastoDTO>();
            foreach (TipoGasto tipoGasto in TipoGasto)
            {
                listadoTipoGasto.Add(new ListadoTipoGastoDTO()
                {
                    Id = tipoGasto.Id,
                    Nombre = tipoGasto.Nombre,
                    Description = tipoGasto.Description,
                    Activo = tipoGasto.Activo,
                });
            }
            return listadoTipoGasto;
        }

        public static ListadoTipoGastoDTO TipoGastoToListadoTipoGastoDTO(TipoGasto gasto)
        {
            if(gasto == null)
            {
                throw new ArgumentNullException("Error");
            }
            return new ListadoTipoGastoDTO() {

                Id = gasto.Id,
                Nombre = gasto.Nombre,
                Description = gasto.Description,
                Activo = gasto.Activo,
            };
        }




        //REVISAR LUEGO PARA QUE LO USAMOS
        public static TipoGasto ListadoTipoGastoDTOToGasto(ListadoTipoGastoDTO gastoDTO, int id)
        {
           if(gastoDTO == null)
            {
                throw new ArgumentNullException("Error");
            }
            TipoGasto gasto = new TipoGasto(gastoDTO.Nombre, gastoDTO.Description);

            gasto.Activo = gastoDTO.Activo;
            //ID PRIVATE, NO SE SETEA CREO
            return gasto;
            ;
        }
    }
}
