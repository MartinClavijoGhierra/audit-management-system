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
    public class CUAltaTipoGasto : ICUAltaTipoGasto
    {
        public IRepositorioTipoGasto RepoTipoGasto { get; set; }
        public CUAltaTipoGasto (IRepositorioTipoGasto repoTipoGasto)
        {
            RepoTipoGasto = repoTipoGasto;
        }


        public ListadoTipoGastoDTO Ejecutar(TipoGastoDTO tipoGastoDTO)
        {
            TipoGasto tipoGasto = MapperTipoGasto.TipoGastoDTOToTipoGasto(tipoGastoDTO);
            RepoTipoGasto.Add(tipoGasto);

            return MapperTipoGasto.TipoGastoToListadoTipoGastoDTO(tipoGasto);
        }
    }
}
