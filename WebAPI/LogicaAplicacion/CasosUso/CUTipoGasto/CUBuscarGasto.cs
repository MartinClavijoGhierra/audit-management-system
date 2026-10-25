using CasosDeUso.DTOs.TipoGastosDTOs;
using CasosDeUso.InterfacesCU.CUTipoGastos;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUTipoGasto
{
    public class CUBuscarGasto : ICUBuscarGasto
    {
        public IRepositorioTipoGasto RepoTipoGasto { get; set; }

        public CUBuscarGasto(IRepositorioTipoGasto repoTipoGasto)
        {
            RepoTipoGasto = repoTipoGasto;
        }

        public ListadoTipoGastoDTO Ejecutar(int id)
        {
            TipoGasto gasto = RepoTipoGasto.FindById(id);
            if(gasto != null)
            {
                return MapperTipoGasto.TipoGastoToListadoTipoGastoDTO(gasto);
            }else {
                throw new TipoGastoException("No se pudo encontrar el gasto");
            }
        }
    }
}
