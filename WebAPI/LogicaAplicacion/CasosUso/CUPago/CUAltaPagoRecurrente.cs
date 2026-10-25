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
using static LogicaAplicacion.CasosUso.CUPago.CUAltaPagoRecurrente;

namespace LogicaAplicacion.CasosUso.CUPago
{
    public class CUAltaPagoRecurrente : ICUAltaPagoRecurrente
    {

        public IRepositorioPago RepoPago { get; set; }
        public IRepositorioTipoGasto RepoTipoGasto { get; set; }
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUAltaPagoRecurrente (IRepositorioPago repoPago, 
            IRepositorioTipoGasto repoTipoGasto,
            IRepositorioUsuario repoUsuario)
        {
            RepoPago = repoPago;
            RepoTipoGasto = repoTipoGasto;
            RepoUsuario = repoUsuario;
        }
        public int Ejecutar(PagoRecurrenteDTO pagoRecurrenteDTO)
        {
            TipoGasto tipoGasto= RepoTipoGasto.FindById(pagoRecurrenteDTO.TipoGastoID);
            Usuario usuario = RepoUsuario.FindById(pagoRecurrenteDTO.UsuarioId);

            //VER SI CONVIENE VERIFICAR AQUI SI EL GASTO ESTA ACTIVO O NO
            if (tipoGasto != null)
            {
                
                PagoRecurrente pagoRecurrente= MapperPago.PagoRecurrenteDTOToPagoRecurrente(pagoRecurrenteDTO, tipoGasto, usuario);
                RepoPago.Add(pagoRecurrente);
                return pagoRecurrente.Id;
            }
            else
            {
                throw new PagoException("No se encontró un Tipo de gasto");
            }
        }

        }


    }

