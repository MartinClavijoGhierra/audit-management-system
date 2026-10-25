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
    public class CUModificarGasto : ICUModificarGasto
    {
        public IRepositorioTipoGasto RepoTipoGasto { get; set; }


        public CUModificarGasto(IRepositorioTipoGasto repoTipoGasto)
        {
            RepoTipoGasto = repoTipoGasto;
        }

        
        public void Ejecutar(int id, ListadoTipoGastoDTO gasto)
        {
           
            TipoGasto tipoGasto = MapperTipoGasto.ListadoTipoGastoDTOToGasto(gasto, id);
            RepoTipoGasto.Update(id, tipoGasto);
        }
    }
}
