using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace SIP.Reportes.Factura
{
    class VistaImpresa
    {
        public static string ExportarFAC(string path_XML, string tipoComprobante, string cveCliente, string remitido, String condiciones, String Pedido, String Agente, String surtidoPor, Decimal comision)
        {
            string _result;

            rptXML _rptXML = new rptXML();
            bool tieneIVA = false;
            DataSet ds = new DataSet();
            DataSet dsTimbre = new DataSet();
            //Dim cadOriginal As String
            string totalLetra = "";
            DataSet _ds_subtotal = new DataSet();
            DataSet _ds_total = new DataSet();
            DataSet _ds_conceptos = new DataSet();
            DataSet _ds_traslado = new DataSet();
            string DivAux = null;

            try
            {


                //ds.ReadXmlSchema(path_XSD);
                ds.ReadXml(path_XML);

                //dsTimbre.ReadXmlSchema(pathTimbrado);
                dsTimbre.ReadXml(path_XML);
                //PARA EL CFDI 

                _rptXML.Subreports["rptEncabezado.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);
                _rptXML.Subreports["rptEncabezado.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);

                _rptXML.Subreports["rptEmisor.rpt"].Database.Tables["Emisor"].SetDataSource(ds.Tables["Emisor"]);

                //_rptXML.Subreports["rptEncabezado2.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);
                //_rptXML.Subreports["rptEncabezado2.rpt"].Database.Tables["Emisor"].SetDataSource(ds.Tables["Emisor"]);
                //_rptXML.Subreports["rptEncabezado2.rpt"].Database.Tables["Receptor"].SetDataSource(ds.Tables["Receptor"]);

                _rptXML.Subreports["rptReceptor.rpt"].Database.Tables["Receptor"].SetDataSource(ds.Tables["Receptor"]);
                _rptXML.Subreports["rptReceptor.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);
                _rptXML.Subreports["rptReceptor.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);

                _rptXML.Subreports["rptConceptos.rpt"].Database.Tables["Concepto"].SetDataSource(ds.Tables["Concepto"]);





                _rptXML.Subreports["rptTotales.rpt"].Database.Tables["Impuestos"].SetDataSource(ds.Tables["Impuestos"]);

                //_rptXML.Subreports["rptTotales.rpt"].Database.Tables["Traslado"].SetDataSource(ds.Tables["Traslado"]);

                DivAux = ds.Tables["Comprobante"].Rows[0]["Moneda"].ToString().Trim();

                _rptXML.Subreports["rptTotales.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);
                totalLetra = Cantidad_Letras(Convert.ToDecimal(ds.Tables["Comprobante"].Rows[0]["total"]), DivAux);



                //PARA EL TIMBRE
                _rptXML.Subreports["rptEncabezado.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);

                //PARA EL QRCODE

                ThoughtWorks.QRCode.Codec.QRCodeEncoder qrCodeEncoder = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
                System.Drawing.Bitmap qrCodeImg = default(System.Drawing.Bitmap);

                string cadenaQrCode = "";

                cadenaQrCode = "?re=" + ds.Tables["Emisor"].Rows[0]["rfc"].ToString();
                cadenaQrCode += "&rr=" + ds.Tables["Receptor"].Rows[0]["rfc"].ToString();
                cadenaQrCode += "&tt=" + ds.Tables["Comprobante"].Rows[0]["total"].ToString();
                cadenaQrCode += "&id=" + dsTimbre.Tables["TimbreFiscalDigital"].Rows[0]["UUID"].ToString();

                qrCodeImg = qrCodeEncoder.Encode(cadenaQrCode);


                Dataset1 moDS = new Dataset1();
                DataRow moDR = default(DataRow);
                moDS = new Dataset1();
                moDR = null;
                moDR = moDS.Varios.NewRow();
                moDR["Image"] = Image2Bytes(qrCodeImg);

                moDS.Varios.Rows.Add(moDR);

                _rptXML.Subreports["rptQrCode.rpt"].SetDataSource(moDS);
                _rptXML.Subreports["rptQrCode.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);
                _rptXML.Subreports["rptQrCode.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);

                //CARGAMOS EL LOGOTIPO


                DataSet logoEmisor = new DataSet();



                //using (ComunInfo100_FinalEntities BDF = new ComunInfo100_FinalEntities(variablesGlobales.modelConexion.ToString()))
                //{
                //    Configuraciones miLogo = BDF.Configuraciones.FirstOrDefault(c => c.ID == id_Contribuyente);

                //    if (miLogo != null)
                //    {

                //        moDS = new Dataset1();
                //        moDR = moDS.Varios.NewRow();
                //        moDR["Image"] = miLogo.Logo;
                //        moDS.Varios.Rows.Add(moDR);
                //        _rptXML.Subreports["rptLogo.rpt"].Database.Tables["Varios"].SetDataSource(moDS);
                //    }
                //    else
                //    {
                //        moDS = new Dataset1();
                //        _rptXML.Subreports["rptLogo.rpt"].Database.Tables["Varios"].SetDataSource(moDS);
                //    }
                //}

                //PASAMOS LOS PARAMETROS                
                _rptXML.SetParameterValue("TasaOCuota", 0);
                _rptXML.SetParameterValue("Importe", 0);
                //IMPUESTOS
                if (ds.Tables["Traslado"] != null)
                {

                    foreach (DataRow oRow in ds.Tables["Traslado"].Rows)
                    {
                        if (string.IsNullOrEmpty(oRow["Base"].ToString()))
                        {
                            _rptXML.SetParameterValue("TasaOCuota", oRow["TasaOCuota"]);
                            _rptXML.SetParameterValue("Importe", oRow["Importe"]);
                        }
                    }
                }
                _rptXML.SetParameterValue("receptorNoCliente", cveCliente);
                _rptXML.SetParameterValue("remitido", remitido);
                _rptXML.SetParameterValue("pedido", Pedido);
                _rptXML.SetParameterValue("agente", Agente);
                _rptXML.SetParameterValue("surtidoPor", surtidoPor);
                _rptXML.SetParameterValue("condiciones", condiciones);
                _rptXML.SetParameterValue("comision", comision);
                _rptXML.SetParameterValue("totalEnLetra", totalLetra.ToUpper());


                ReportDocument rp = new ReportDocument();
                rp = _rptXML;
                string pathpdf = null;
                pathpdf = path_XML;
                pathpdf = pathpdf.Substring(0, pathpdf.Length - 3);
                pathpdf = pathpdf + "pdf";

                rp.ExportToDisk(ExportFormatType.PortableDocFormat, pathpdf);
                rp.Close();

                _result = "";
            }
            catch (Exception ex)
            {
                //Interaction.MsgBox("Codigo Error:" + ex.Message, MsgBoxStyle.Critical, "Error al generar Vista Impresa.");
                _result = "Error al generar la vista Impresa: " + Environment.NewLine +
                    ex.Message;
            }

            return _result;
        }
        public static string ExportarNC(string path_XML, string tipoComprobante, string cveCliente, string remitido, String condiciones, String Pedido, String OC, String Agente, Decimal comision, String observaciones)
        {
            string _result;

            rptXMLNC _rptXML = new rptXMLNC();
            bool tieneIVA = false;
            DataSet ds = new DataSet();
            DataSet dsTimbre = new DataSet();
            //Dim cadOriginal As String
            string totalLetra = "";
            DataSet _ds_subtotal = new DataSet();
            DataSet _ds_total = new DataSet();
            DataSet _ds_conceptos = new DataSet();
            DataSet _ds_traslado = new DataSet();
            string DivAux = null;

            try
            {


                //ds.ReadXmlSchema(path_XSD);
                ds.ReadXml(path_XML);

                //dsTimbre.ReadXmlSchema(pathTimbrado);
                dsTimbre.ReadXml(path_XML);
                //PARA EL CFDI 

                _rptXML.Subreports["rptEncabezado.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);
                _rptXML.Subreports["rptEncabezado.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);

                _rptXML.Subreports["rptEmisor.rpt"].Database.Tables["Emisor"].SetDataSource(ds.Tables["Emisor"]);

                //_rptXML.Subreports["rptEncabezado2.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);
                //_rptXML.Subreports["rptEncabezado2.rpt"].Database.Tables["Emisor"].SetDataSource(ds.Tables["Emisor"]);
                //_rptXML.Subreports["rptEncabezado2.rpt"].Database.Tables["Receptor"].SetDataSource(ds.Tables["Receptor"]);

                _rptXML.Subreports["rptReceptor.rpt"].Database.Tables["Receptor"].SetDataSource(ds.Tables["Receptor"]);
                _rptXML.Subreports["rptReceptor.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);
                _rptXML.Subreports["rptReceptor.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);

                _rptXML.Subreports["rptConceptos.rpt"].Database.Tables["Concepto"].SetDataSource(ds.Tables["Concepto"]);





                _rptXML.Subreports["rptTotales.rpt"].Database.Tables["Impuestos"].SetDataSource(ds.Tables["Impuestos"]);

                //_rptXML.Subreports["rptTotales.rpt"].Database.Tables["Traslado"].SetDataSource(ds.Tables["Traslado"]);

                DivAux = ds.Tables["Comprobante"].Rows[0]["Moneda"].ToString().Trim();

                _rptXML.Subreports["rptTotales.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);
                totalLetra = Cantidad_Letras(Convert.ToDecimal(ds.Tables["Comprobante"].Rows[0]["total"]), DivAux);



                //PARA EL TIMBRE
                _rptXML.Subreports["rptEncabezado.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);

                //PARA EL QRCODE

                ThoughtWorks.QRCode.Codec.QRCodeEncoder qrCodeEncoder = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
                System.Drawing.Bitmap qrCodeImg = default(System.Drawing.Bitmap);

                string cadenaQrCode = "";

                cadenaQrCode = "?re=" + ds.Tables["Emisor"].Rows[0]["rfc"].ToString();
                cadenaQrCode += "&rr=" + ds.Tables["Receptor"].Rows[0]["rfc"].ToString();
                cadenaQrCode += "&tt=" + ds.Tables["Comprobante"].Rows[0]["total"].ToString();
                cadenaQrCode += "&id=" + dsTimbre.Tables["TimbreFiscalDigital"].Rows[0]["UUID"].ToString();

                qrCodeImg = qrCodeEncoder.Encode(cadenaQrCode);


                Dataset1 moDS = new Dataset1();
                DataRow moDR = default(DataRow);
                moDS = new Dataset1();
                moDR = null;
                moDR = moDS.Varios.NewRow();
                moDR["Image"] = Image2Bytes(qrCodeImg);

                moDS.Varios.Rows.Add(moDR);

                _rptXML.Subreports["rptQrCode.rpt"].SetDataSource(moDS);
                _rptXML.Subreports["rptQrCode.rpt"].Database.Tables["TimbreFiscalDigital"].SetDataSource(dsTimbre);
                _rptXML.Subreports["rptQrCode.rpt"].Database.Tables["Comprobante"].SetDataSource(ds.Tables["Comprobante"]);

                //CARGAMOS EL LOGOTIPO


                DataSet logoEmisor = new DataSet();



                //using (ComunInfo100_FinalEntities BDF = new ComunInfo100_FinalEntities(variablesGlobales.modelConexion.ToString()))
                //{
                //    Configuraciones miLogo = BDF.Configuraciones.FirstOrDefault(c => c.ID == id_Contribuyente);

                //    if (miLogo != null)
                //    {

                //        moDS = new Dataset1();
                //        moDR = moDS.Varios.NewRow();
                //        moDR["Image"] = miLogo.Logo;
                //        moDS.Varios.Rows.Add(moDR);
                //        _rptXML.Subreports["rptLogo.rpt"].Database.Tables["Varios"].SetDataSource(moDS);
                //    }
                //    else
                //    {
                //        moDS = new Dataset1();
                //        _rptXML.Subreports["rptLogo.rpt"].Database.Tables["Varios"].SetDataSource(moDS);
                //    }
                //}

                //PASAMOS LOS PARAMETROS                
                _rptXML.SetParameterValue("TasaOCuota", 0);
                _rptXML.SetParameterValue("Importe", 0);
                //IMPUESTOS
                if (ds.Tables["Traslado"] != null)
                {

                    foreach (DataRow oRow in ds.Tables["Traslado"].Rows)
                    {
                        if (string.IsNullOrEmpty(oRow["Base"].ToString()))
                        {
                            _rptXML.SetParameterValue("TasaOCuota", oRow["TasaOCuota"]);
                            _rptXML.SetParameterValue("Importe", oRow["Importe"]);
                        }
                    }
                }
                _rptXML.SetParameterValue("receptorNoCliente", cveCliente);
                _rptXML.SetParameterValue("remitido", remitido);
                _rptXML.SetParameterValue("pedido", Pedido);
                _rptXML.SetParameterValue("oc", OC);
                _rptXML.SetParameterValue("agente", Agente);
                _rptXML.SetParameterValue("condiciones", condiciones);
                _rptXML.SetParameterValue("comision", comision);
                _rptXML.SetParameterValue("totalEnLetra", totalLetra.ToUpper());
                _rptXML.SetParameterValue("observaciones", observaciones);


                ReportDocument rp = new ReportDocument();
                rp = _rptXML;
                string pathpdf = null;
                pathpdf = path_XML;
                pathpdf = pathpdf.Substring(0, pathpdf.Length - 3);
                pathpdf = pathpdf + "pdf";

                rp.ExportToDisk(ExportFormatType.PortableDocFormat, pathpdf);
                rp.Close();

                _result = "";
            }
            catch (Exception ex)
            {
                //Interaction.MsgBox("Codigo Error:" + ex.Message, MsgBoxStyle.Critical, "Error al generar Vista Impresa.");
                _result = "Error al generar la vista Impresa: " + Environment.NewLine +
                    ex.Message;
            }

            return _result;
        }
        public static byte[] Image2Bytes(System.Drawing.Bitmap img)
        {
            string sTemp = System.IO.Path.GetTempFileName();
            System.IO.FileStream fs = new System.IO.FileStream(sTemp, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);

            fs.Position = 0;
            //
            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }
        //CANTIDAD LETRAS
        public static String Cantidad_Letras(Decimal Cifra, String Divisa)
        {

            int millones;
            long miles;
            int cienes;
            Decimal decimales;
            String dec;
            String letras;
            String PALABRA;
            String moneda_palabra;

            millones = (int)(Cifra / 1000000);
            miles = (int)((Cifra - (millones * 1000000)) / 1000);
            cienes = (int)(Cifra - (millones * 1000000) - (miles * 1000));
            decimales = ((Math.Round(Cifra, 2) - (int)(Cifra)) * 100);
            dec = "";
            letras = "";
            if (millones > 0)
            {
                if (millones == 1)
                {
                    letras = letras + " un millon ";
                }
                else
                {
                    letras = letras + cantidad((millones)) + " millones ";
                }

            }
            if (miles > 0)
            {
                if (miles == 1)
                {
                    letras = letras + " un mil ";
                }
                else
                {
                    letras = letras + cantidad((miles)) + " mil ";
                }
            }
            if (cienes > 0)
            {
                letras = letras + cantidad((cienes));
            }
            if (decimales > 0)
            {
                //dec = Right(Round(cifra, 2), 2)
                dec = Convert.ToString(decimales.ToString().Trim().Split('.')[0]).PadLeft(2, '0');
            }
            else
            {
                dec = "00";
            }


            PALABRA = "";
            switch (Divisa)
            {
                case "MXP":
                    moneda_palabra = " Pesos Mexicanos ";
                    PALABRA = " M.N.";
                    break;

                case "USD":
                    moneda_palabra = " Dolares ";
                    PALABRA = "USD";
                    break;

                default:
                    moneda_palabra = " Pesos Mexicanos ";
                    PALABRA = " M.N.";
                    break;
            }



            //letras = letras & " pesos " & dec & "/100 M.N."
            letras = letras + moneda_palabra + dec + "/100 " + PALABRA;
            //miel = letras


            return letras;
        }
        public static String cantidad(Decimal numero)
        {


            int centena;
            int decena;
            int unidad;
            Decimal num_cen;
            Decimal num_dec;
            String cientos;
            String decadas;
            String unicos;
            String cantidad = "";


            centena = (int)(numero / 100);
            decena = (int)((numero - (centena * 100)) / 10);
            unidad = (int)(numero - (centena * 100) - (decena * 10));
            num_cen = numero;
            num_dec = (decena * 10) + unidad;
            cientos = "";
            decadas = "";
            unicos = "";

            if (centena == 1)
            {

                if (num_cen == 100)
                {
                    cientos = "cien";
                }
                else
                {
                    cientos = "ciento";
                }
            }

            if (centena == 2)
            {
                cientos = "doscientos";
            }

            if (centena == 3)
            {
                cientos = "trescientos";
            }

            if (centena == 4)
            {
                cientos = "cuatrocientos";
            }
            if (centena == 5)
            {
                cientos = "quinientos";
            }

            if (centena == 6)
            {

                cientos = "seiscientos";

            }

            if (centena == 7)
            {

                cientos = "setecientos";

            }

            if (centena == 8)
            {

                cientos = "ochocientos";

            }

            if (centena == 9)
            {

                cientos = "novecientos";

            }



            if (decena == 1)
            {

                if (num_dec == 10)
                {

                    decadas = "diez";
                }
                else
                {

                    if (num_dec > 15)
                    {

                        //decadas = "diez y"

                        decadas = "dieci";


                    }
                    else
                    {

                        decadas = "";

                    }

                }

            }

            if (decena == 2)
            {

                if (num_dec > 20)
                {

                    //decadas = "veinte y"

                    decadas = "veinti";
                }
                else
                {

                    decadas = "veinte";

                }

            }

            if (decena == 3)
            {

                if (num_dec > 30)
                {

                    decadas = "treinta y";
                }
                else
                {

                    decadas = "treinta";

                }

            }

            if (decena == 4)
            {

                if (num_dec > 40)
                {

                    decadas = "cuarenta y";
                }
                else
                {

                    decadas = "cuarenta";

                }

            }

            if (decena == 5)
            {

                if (num_dec > 50)
                {

                    decadas = "cincuenta y";
                }
                else
                {

                    decadas = "cincuenta";

                }

            }

            if (decena == 6)
            {

                if (num_dec > 60)
                {

                    decadas = "sesenta y";
                }
                else
                {

                    decadas = "sesenta";

                }

            }

            if (decena == 7)
            {

                if (num_dec > 70)
                {

                    decadas = "setenta y";
                }
                else
                {

                    decadas = "setenta";

                }

            }

            if (decena == 8)
            {

                if (num_dec > 80)
                {

                    decadas = "ochenta y";
                }
                else
                {

                    decadas = "ochenta";

                }

            }

            if (decena == 9)
            {

                if (num_dec > 90)
                {

                    decadas = "noventa y";
                }
                else
                {

                    decadas = "noventa";

                }

            }

            if (unidad == 1)
            {
                if (num_dec == 11)
                {

                    unicos = "once";
                }
                else
                {

                    unicos = "un";

                }

            }

            if (unidad == 2)
            {

                if (num_dec == 12)
                {

                    unicos = "doce";
                }
                else
                {

                    unicos = "dos";

                }

            }

            if (unidad == 3)
            {

                if (num_dec == 13)
                {
                    unicos = "trece";
                }
                else
                {
                    unicos = "tres";

                }

            }

            if (unidad == 4)
            {

                if (num_dec == 14)
                {

                    unicos = "catorce";
                }
                else
                {

                    unicos = "cuatro";

                }

            }

            if (unidad == 5)
            {

                if (num_dec == 15)
                {

                    unicos = "quince";
                }
                else
                {

                    unicos = "cinco";

                }

            }

            if (unidad == 6)
            {
                unicos = "seis";
            }

            if (unidad == 7)
            {
                unicos = "siete";
            }

            if (unidad == 8)
            {
                unicos = "ocho";
            }

            if (unidad == 9)
            {
                unicos = "nueve";
            }



            if (decena > 2)
            {
                cantidad = cientos + " " + decadas + " " + unicos;
            }
            else
            {

                cantidad = cientos + " " + decadas + unicos;

            }


            return cantidad;

        }
    }
}
