using CasosDeUso.DTOs.PagoDTOs;
using CasosDeUso.InterfacesCU.CUPago;
using ExcepcionesPropias.ExcepcionesEntidades;
using LogicaAplicacion.Mappers;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUAltaPagoUnico : ICUAltaPagoUnico
    {
        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioTipoGasto RepoTipoGasto { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUAltaPagoUnico(IRepositorioPago repoPago,
            IRepositorioTipoGasto repoTipoGasto,
            IRepositorioUsuario repoUsuario)
        {
            RepoPago = repoPago;
            RepoTipoGasto = repoTipoGasto;
            RepoUsuario = repoUsuario;
        }

        public int Ejecutar(PagoUnicoDTO pagoUnicoDTO)
        {
            TipoGasto tipoGasto = RepoTipoGasto.FindById(pagoUnicoDTO.TipoGastoID);
            Usuario usuario = RepoUsuario.FindById(pagoUnicoDTO.UsuarioId);

            //VER SI CONVIENE VERIFICAR AQUI SI EL GASTO ESTA ACTIVO O NO
            if (tipoGasto != null)
            {

                PagoUnico pagoUnico = MapperPago.PagoUnicoDTOToPagoUnico(pagoUnicoDTO, tipoGasto, usuario);
                RepoPago.Add(pagoUnico);
                return pagoUnico.Id;
            }
            else
            {
                throw new PagoException("No se encontró un Tipo de gasto");
            }
        }

    }

}

