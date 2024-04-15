using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulp_bl
{
    public class Enumerados
    {
        public enum TipoBorrado
        {
            Fisico, Logico
        }
        public enum TipoPermiso
        {
            PuedeEntrar,
            PuedeInsertar,
            PuedeModificar,
            PuedeBorrar
        }
        //enumerados para reportes
        public enum TipoReporteFoliosAnt
        {
            Credito,
            Mostrador,
            Ambos
        }
        public enum TipoReporteFolios
        {
            Anteriores,
            Nuevos
        }
        public enum TipoReportePedido
        {
            SinFechaSurtido, ConFechaSurtido
        }
        public enum TipoPedido
        {
            NoAplica,
            Pedido,
            OrdenTrabajo,
            PedidoDAT,
            PedidoMOS,
            PedidoEC,
            PedidoMOSCP,
            PedidoDividido
        }
        public enum TipoReporteExistencias
        {
            AlmGeneral, AlmSurtido, AlmMostrador
        }
        public enum TipoReporteExistenciasBase
        {
            SMAX, SMIN, SMINsinPP
        }
        public enum TipoReporteClientesRecuperados
        {

            Foraneo, Metropolitano, Ninguno
        }
        public enum TipoTareaProcesamiento
        {
            ProcesoGeneral,
            ReporteEstadoCuenta,
            ReporteLogotipos
        }
        public enum TipoReporteCrystal
        {
            Logotipos,
            Pedidos,
            OrdenTrabajo,
            DatosFacturaBordado,
            EtiquetasEmpaque,
            PedidoCapturaSAE,
            TransferenciaXModelo,
            Precarga,
            Requisicion,
            NotificacionesSIVO,
            RequisicionMostrador
        }
        public enum AreasEmpresa
        {
            Ventas,
            Almacen,
            Compras,
            Operaciones,
            Sistemas,
            Credito,
            Cliente,
            Contabilidad
        }
        public enum EstandaresPedidos
        {
            ADVO,   //1.-ADMINISTRATIVO
            LIB,    //2.-LIBERACIÓN
            SUR,    //3.-SURTIDO
            COR,    //4.-CORTE
            EST,    //5.-?
            BOR,    //6.-BORDADO
            INI,    //7.-?
            COS,    //8.-COSTURA
            EMP,    //9.-EMPAQUE
            EMB,    //10.-EMBARQUE
            ESP     //11.-ESPECIAL
        }
        public enum TipoFechaReporteDesempeños
        {
            FechasCumplidas, FechasAdelantadas, FechasNoCumplidas, FechasNoEntregadas
        }

        public enum Procesos
        {
            /// <summary>
            /// Flete
            /// </summary>
            F,
            /// <summary>
            /// Costura
            /// </summary>
            C,
            /// <summary>
            /// Estampado
            /// </summary>
            E
        }
        public enum TipoOrdenProduccion
        {
            Liberada = 1,
            NoLiberada = 2
        }
        public enum TipoCajaTextoInputBox
        {
            Numerica,
            Texto,
            Fecha
        }
        public enum TipoBusqueda
        {
            General,
            Pedidos,
            OrdenesTrabajo
        }

        public enum TipoSimulador
        {
            SimuladorDeCostos,
            EstructuraDeProducto
        }

        public enum TipoReporteFiltroConta
        {
            RecYCosteoDeInvMP_SMO,
            RecYCosteoDeInvPP_SMO,
            RecYCosteoDeInvPT_SMO,
            CostoVendidoEntreFechas_SMO,
            RecYCosteoDeMermas_SMO,
            RecYCosteoDeInvPT_CMO,
            CostoVendidoEntreFechas_CMO,
            RecYCosteoDeMermas_CMO
        }

        public enum FormatosNPOI
        {
            /// <summary>
            /// Regresa un formato tipo moneda, ejemplo:
            /// $ 516,320.39
            /// </summary>
            MONEDA,
            /// <summary>
            /// Regresa un formato numérico con separador de miles SIN decimales, ejemplo:
            /// 516,320
            /// </summary>
            MILES,
            /// <summary>
            /// Regresa un formato numérico con separador de miles, ejemplo:
            /// 516,320.39
            /// </summary>
            MILES2DECIMALES
        }
    }
}
