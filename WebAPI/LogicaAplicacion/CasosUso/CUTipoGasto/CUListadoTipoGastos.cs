using CasosDeUso.DTOs.TipoGastosDTOs;
using CasosDeUso.InterfacesCU.CUTipoGastos;
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
    public class CUListadoTipoGastos: ICUListadoTipoGastos
    {
        public IRepositorioTipoGasto RepoTipoGasto { get; set; }
        public CUListadoTipoGastos(IRepositorioTipoGasto repoTipoGasto) 
        {
            RepoTipoGasto = repoTipoGasto;
        }

        public IEnumerable<ListadoTipoGastoDTO> Ejecutar()
        {
            IEnumerable<TipoGasto> TipoGasto = RepoTipoGasto.FindAll();
            return MapperTipoGasto.ListadoTipoGastoToListadoTipoGastoDTO(TipoGasto);
        }
    }
}
