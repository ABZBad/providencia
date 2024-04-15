using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.SIPNegocio;

namespace ulp_bl.Versiones
{
    public class ControlVersionesSIP
    {
        public StringBuilder DevuelveDetalleVersiones(double DiasDesde)
        {
            DateTime Hoy = DateTime.Today;
            DateTime FechaInicial = Hoy.AddDays(-DiasDesde);
            DateTime FechaFinal = Hoy;

            using (var dataBaseContext = new SIPNegocioContext())
            {
                List<string> DetallesVersion = new List<string>();

                StringBuilder sb = new StringBuilder();
                string CurrentVersion = DevuelveMaximaVersion();
                sb.Append("Current Version [" + CurrentVersion + "]" + Environment.NewLine);

                var query = from VerPrincipal in dataBaseContext.VersionesPrincipals
                            join VerDetalle in dataBaseContext.VersionesDetalles
                            on VerPrincipal.Id equals VerDetalle.VersionesPrincipalId
                            //where VerPrincipal.VersionFecha >= FechaInicial & VerPrincipal.VersionFecha <= FechaFinal
                            orderby VerPrincipal.Id descending, VerDetalle.Id
                            select new { VerID = VerPrincipal.Id, VerVersion = VerPrincipal.VersionesVersion, VerDet = VerDetalle.VersionesDescripcion };

                int VersionID = 0;
                foreach (var V in query)
                {
                    if (VersionID == V.VerID)
                    {
                        sb.Append("\t·" + V.VerDet + Environment.NewLine);
                    }
                    else
                    {
                        
                        if (VersionID != 0)
                        {
                            sb.Append("------------------" + Environment.NewLine);
                            sb.Append("[" + V.VerVersion + "]" + Environment.NewLine);
                        }
                        sb.Append("\t·" + V.VerDet + Environment.NewLine);
                        VersionID = V.VerID; 
                    }
                }


                /*
                sb.Append("\t·Se agrega pantalla: \"Información de este quipo\" " + Environment.NewLine);
                sb.Append("\t·Se agrega funcionalidad: \"Búsqueda de clientes (Pestaña Solicitud de pedidos)\" " + Environment.NewLine);
                sb.Append("\t·Se agrega reporte: \"Reporte diario de Facturación\\Folios anteriores\"" + Environment.NewLine);
                sb.Append("\t·Se agrega reporte: \"Reporte diario de Facturación\\Folios nuevos\"" + Environment.NewLine);
                sb.Append("\t·Se agrega reporte: \"Exporta UpPedidos a Excel\\Con base a fecha SIP\"" + Environment.NewLine);
                sb.Append("\t·Se agrega reporte: \"Exporta UpPedidos a Excel\\Con base a fecha SAE\"" + Environment.NewLine);
                sb.Append("------------------" + Environment.NewLine);
                */

                return sb;
            }
        }

        public string DevuelveMaximaVersion()
        {
            using (var dataBaseContext = new SIPNegocioContext())
            {
                List<string> DetallesVersion = new List<string>();

                int intId = dataBaseContext.VersionesPrincipals.Max(u => u.Id);

                var query = from Ver in dataBaseContext.VersionesPrincipals
                                           where (Ver.Id == intId)
                                           select Ver.VersionesVersion;

                return query.Single().ToString();
            }
        }
        
    }
}
