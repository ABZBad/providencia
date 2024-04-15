using CRYPTO100;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using CFDI33.Clases.Generales;

namespace CFDI33.Clases.CFDI
{
    public class Comprobante_33
    {
        public const string _Version = "1.0";
        public int NumeroCuenta { get; set; }
        public string NombreCliente { get; set; }
        public string Periodo { get; set; }
        public string Sucursal { get; set; }

        #region<ATRIBUTOS Y CONSTRUCTORES>
        public Comprobante_33()
        {
            this.NumeroCuenta = 0;
            this.NombreCliente = "";
            this.Periodo = "";
            this.Sucursal = "";
            Serie = string.Empty;
            Folio = string.Empty;
            Fecha = DateTime.MinValue;
            Sello = string.Empty;
            FormaPago = string.Empty;
            NoCertificado = string.Empty;
            Certificado = string.Empty;
            CondicionesPago = string.Empty;
            SubTotal = 0;
            Descuento = 0;
            Moneda = string.Empty;
            TipoCambio = 0;
            Total = 0;
            TipoDeComprobante = string.Empty;
            MetodoPago = string.Empty;
            LugarExpedicion = string.Empty;
            Confirmacion = string.Empty;
            //DATOS GENERALES EDO CUENTA SOLO PARA FINSUR
            CAT = 0;
            tasaInteresMoratoria = 0;
            tasaInteresOrdinaria = 0;
            numeroPagosRealizados = string.Empty;
            ProximoPagoCapital = string.Empty;
            ProximoPagoInteresOrdinario = string.Empty;
            ProximoPagoInteresMoratorio = string.Empty;
            numeroPagosPactado = string.Empty;
            saldoRestanteFecha = 0;
            capitalinsolutoFecha = 0;
            interesesMoratorios = 0;
            saldoVencidoFecha = 0;
            montoUltimoPagoReg = 0;
            fechaUltimoPagoReg = string.Empty;
            tasaTIIELIBOR = 0;
            diasFinanciamientoAdicionales = string.Empty;
            fechaPagoFinal = string.Empty;
            fechaPrimerPago = string.Empty;
            fechaDisposicion = string.Empty;
            montoCalculoIntereses = 0;
            montoCredito = 0;
            fechaCorte = string.Empty;
            fechaLimitePago = string.Empty;
            periodo = string.Empty;
            credito = string.Empty;
            //FIN DE DATOS GENERALES EDO CUENTA SOLO PARA FINSUR
            CfdiRelacionados = new CfdiRelacionados();
            Emisor = new Emisor();
            Receptor = new Receptor();
            Conceptos = new List<Concepto>();
            Impuestos = new Impuestos();

            CadenaOriginal = string.Empty;
        }


        private const string Version = "3.3";


        private string _serie;
        private string _folio;
        private DateTime _fecha;
        private string _sello;
        private string _formaPago;
        private string _noCertificado;
        private string _certificado;
        private string _condicionesDePago;
        private decimal _subTotal;
        private decimal _descuento;
        private string _moneda;
        private decimal _tipoCambio;
        private decimal _total;
        private string _tipoDeComprobante;
        private string _metodoPago;
        private string _lugarExpedicion;
        private string _confirmacion;

        private decimal _CAT;
        private decimal _tasaInteresMoratoria;
        private decimal _tasaInteresOrdinaria;
        private string _numeroPagosRealizados;
        private string _numeroPagosPactado;

        private string _proximoPagoCapital;
        private string _proximoPagoInteresOrdinario;
        private string _proximoPagoInteresMoratorio;

        private decimal _saldoRestanteFecha;
        private decimal _capitalinsolutoFecha;
        private decimal _interesesMoratorios;
        private decimal _saldoVencidoFecha;
        private decimal _montoUltimoPagoReg;
        private string _fechaUltimoPagoReg;
        private decimal _tasaTIIELIBOR;
        private string _diasFinanciamientoAdicionales;
        private string _fechaPagoFinal;
        private string _fechaPrimerPago;
        private string _fechaDisposicion;
        private decimal _montoCalculoIntereses;
        private decimal _montoCredito;
        private string _fechaCorte;
        private string _fechaLimitePago;
        private string _periodo;
        private string _credito;

        private CfdiRelacionados _cfdiRelacinados;
        private Emisor _emisor;
        private Receptor _receptor;
        private List<Concepto> _conceptos;
        private Impuestos _impuestos;
        private string _cadenaOriginal;

        //METODOS GET SET DE FINSUR
        public decimal CAT
        {
            get { return _CAT; }
            set { _CAT = value; }
        }
        public decimal tasaInteresMoratoria
        {
            get { return _tasaInteresMoratoria; }
            set { _tasaInteresMoratoria = value; }
        }
        public decimal tasaInteresOrdinaria
        {
            get { return _tasaInteresOrdinaria; }
            set { _tasaInteresOrdinaria = value; }
        }
        public string numeroPagosRealizados
        {
            get { return _numeroPagosRealizados; }
            set { _numeroPagosRealizados = value; }
        }

        public string ProximoPagoCapital
        {
            get { return _proximoPagoCapital; }
            set { _proximoPagoCapital = value; }
        }

        public string ProximoPagoInteresOrdinario
        {
            get { return _proximoPagoInteresOrdinario; }
            set { _proximoPagoInteresOrdinario = value; }
        }

        public string ProximoPagoInteresMoratorio
        {
            get { return _proximoPagoInteresMoratorio; }
            set { _proximoPagoInteresMoratorio = value; }
        }

        public string numeroPagosPactado
        {
            get { return _numeroPagosPactado; }
            set { _numeroPagosPactado = value; }
        }
        public decimal saldoRestanteFecha
        {
            get { return _saldoRestanteFecha; }
            set { _saldoRestanteFecha = value; }
        }
        public decimal capitalinsolutoFecha
        {
            get { return _capitalinsolutoFecha; }
            set { _capitalinsolutoFecha = value; }
        }
        public decimal interesesMoratorios
        {
            get { return _interesesMoratorios; }
            set { _interesesMoratorios = value; }
        }
        public decimal saldoVencidoFecha
        {
            get { return _saldoVencidoFecha; }
            set { _saldoVencidoFecha = value; }
        }
        public decimal montoUltimoPagoReg
        {
            get { return _montoUltimoPagoReg; }
            set { _montoUltimoPagoReg = value; }
        }
        public string fechaUltimoPagoReg
        {
            get { return _fechaUltimoPagoReg; }
            set { _fechaUltimoPagoReg = value; }
        }
        public decimal tasaTIIELIBOR
        {
            get { return _tasaTIIELIBOR; }
            set { _tasaTIIELIBOR = value; }
        }
        public string diasFinanciamientoAdicionales
        {
            get { return _diasFinanciamientoAdicionales; }
            set { _diasFinanciamientoAdicionales = value; }
        }
        public string fechaPagoFinal
        {
            get { return _fechaPagoFinal; }
            set { _fechaPagoFinal = value; }
        }
        public string fechaPrimerPago
        {
            get { return _fechaPrimerPago; }
            set { _fechaPrimerPago = value; }
        }
        public string fechaDisposicion
        {
            get { return _fechaDisposicion; }
            set { _fechaDisposicion = value; }
        }
        public decimal montoCalculoIntereses
        {
            get { return _montoCalculoIntereses; }
            set { _montoCalculoIntereses = value; }
        }
        public decimal montoCredito
        {
            get { return _montoCredito; }
            set { _montoCredito = value; }
        }
        public string fechaCorte
        {
            get { return _fechaCorte; }
            set { _fechaCorte = value; }
        }
        public string fechaLimitePago
        {
            get { return _fechaLimitePago; }
            set { _fechaLimitePago = value; }
        }
        public string periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }
        public string credito
        {
            get { return _credito; }
            set { _credito = value; }
        }
        //FIN DE METODOS GET SET DE FINSUR
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        public string Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public string Sello
        {
            get { return _sello; }
            set { _sello = value; }
        }
        public string FormaPago
        {
            get { return _formaPago; }
            set { _formaPago = value; }
        }
        public string NoCertificado
        {
            get { return _noCertificado; }
            set { _noCertificado = value; }
        }
        public string Certificado
        {
            get { return _certificado; }
            set { _certificado = value; }
        }
        public string CondicionesPago
        {
            get { return _condicionesDePago; }
            set { _condicionesDePago = value; }
        }
        public decimal SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }
        public decimal Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }
        public string Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }
        public decimal TipoCambio
        {
            get { return _tipoCambio; }
            set { _tipoCambio = value; }
        }
        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }
        public string TipoDeComprobante
        {
            get { return _tipoDeComprobante; }
            set { _tipoDeComprobante = value; }
        }
        public string MetodoPago
        {
            get { return _metodoPago; }
            set { _metodoPago = value; }
        }
        public string LugarExpedicion
        {
            get { return _lugarExpedicion; }
            set { _lugarExpedicion = value; }
        }
        public string Confirmacion
        {
            get { return _confirmacion; }
            set { _confirmacion = value; }
        }


        public CfdiRelacionados CfdiRelacionados
        {
            get { return _cfdiRelacinados; }
            set { _cfdiRelacinados = value; }
        }
        public Emisor Emisor
        {
            get { return _emisor; }
            set { _emisor = value; }
        }
        public Receptor Receptor
        {
            get { return _receptor; }
            set { _receptor = value; }
        }
        public List<Concepto> Conceptos
        {
            get { return _conceptos; }
            set { _conceptos = value; }
        }
        public Impuestos Impuestos
        {
            get { return _impuestos; }
            set { _impuestos = value; }
        }
        public string CadenaOriginal
        {
            get { return _cadenaOriginal; }
            set { _cadenaOriginal = value; }
        }

        #endregion
        #region<METODOS>
        /// <summary>
        /// //Metodo encargado de generar un archivo xml (cfdi) retornando un valor de tipo stream
        /// </summary>
        /// <param name="byteCer"></param>
        /// <param name="byteKey"></param>
        /// <param name="_pass"></param>
        /// <returns></returns>
        public Stream GeneraXML_Factura(byte[] byteCer, byte[] byteKey, string _pass, int nRedondeo)
        {
            string _error = "";
            MemoryStream mem = new MemoryStream();
            Stream streamXML = null;
            //VALIDAMOS LOS DATOS DEL COMPROBANTE
            _error = valida();

            if (!string.IsNullOrEmpty(_error))
            {
                //REGRESAMOS ERROR
                streamXML = FuncionesGlobales.GenerateStreamFromString("Error " + _error);

                return streamXML;
            }
            //CORTAMOS LOS ESPACIOS DE TODOS LOS STRINGS
            _error = mondar();
            if (!string.IsNullOrEmpty(_error))
            {
                //REGRESAMOS ERROR
                streamXML = FuncionesGlobales.GenerateStreamFromString("Error " + _error);

                return streamXML;
            }
            //AQUI VA EL CALCULO DEL CFDI
            calculo(nRedondeo);
            //GENERAMOS EL XML BASICO
            streamXML = GetXMLBasico(nRedondeo);

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(streamXML);

            //VALIDAMOS QUE LOS COMPLEMENTOS ESTEN BIEN
            /*
            if (!generarComplementos(xmlDoc))
            {
                streamXML = FuncionesGlobales.GenerateStreamFromString(_error);

                return streamXML;
            }

            if (!generarAddendaECB(xmlDoc))
            {
                //REGRESAMOS ERROR
                streamXML = FuncionesGlobales.GenerateStreamFromString(_error);

                return streamXML;
            }
            */

            _error = generarCadenaOriginal(xmlDoc.InnerXml);

            if (_error.Contains("Error"))
            {
                streamXML = FuncionesGlobales.GenerateStreamFromString(_error);

                return streamXML;
            }

            string[] sello = null;

            sello = SelloDigital.getSelloDigital(byteCer, byteKey, _pass, CadenaOriginal, TipoAlgoritmo.SHA256);

            if (sello[0] != "")
            {
                Sello = sello[0];

                XmlNode oNodo;

                oNodo = xmlDoc.DocumentElement;

                if (oNodo.Name == "cfdi:Comprobante")
                    oNodo.Attributes["Sello"].Value = Sello;

                streamXML = new MemoryStream();

                xmlDoc.Save(streamXML);

                streamXML.Flush();
                streamXML.Position = 0;

                return streamXML;
            }
            else
            {
                streamXML = FuncionesGlobales.GenerateStreamFromString("Error al generar el sello.|");

                return streamXML;
            }
        }
        /// <summary>
        /// //Metodo encargado de generar un archivo xml (cfdi) retornando un valor de tipo string[]
        /// </summary>
        /// <param name="_pathSalida"></param>
        /// <param name="byteCer"></param>
        /// <param name="byteKey"></param>
        /// <param name="_pass"></param>
        /// <returns></returns>
        public string[] GeneraXML_Factura(string _pathSalida, byte[] byteCer, byte[] byteKey, string _pass, int nRedondea)
        {

            string[] stringXML = new string[2];

            stringXML[0] = "";
            stringXML[1] = "";

            //VALIDAMOS LOS DATOS DEL COMPROBANTE
            stringXML[1] = valida();

            if (!string.IsNullOrEmpty(stringXML[1]))
            {
                //REGRESAMOS ERROR
                stringXML[0] = "";

                return stringXML;
            }
            //CORTAMOS LOS ESPACIOS DE TODOS LOS STRINGS
            stringXML[1] = mondar();
            if (!string.IsNullOrEmpty(stringXML[1]))
            {
                //REGRESAMOS ERROR
                stringXML[0] = "";

                return stringXML;
            }

            calculo(nRedondea);

            stringXML[0] = GetXMLBasico(_pathSalida);

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(stringXML[0]);

            //VALIDAMOS QUE LOS COMPLEMENTOS ESTEN BIEN
            /*
            if (!generarComplementos(xmlDoc))
            {
                //REGRESAMOS ERROR
                stringXML[0] = "";
                stringXML[1] = ""; //ERROR
                return stringXML;
            }
             * 
             */
            //VALIDAMOS QUE LAS ADDENDAS ESTEN BIEN
            /*
            if (!generarAddendaECB(xmlDoc))
            {
                //REGRESAMOS ERROR
                stringXML[0] = "";
                stringXML[1] = ""; //ERROR
                return stringXML;
            }*/
            //GENERAMOS LA CADENA ORIGINAL
            stringXML[1] = generarCadenaOriginal(xmlDoc.InnerXml);

            if (!string.IsNullOrEmpty(stringXML[1]))
            {
                stringXML[0] = "";

                return stringXML;
            }

            string[] sello = null;


            sello = SelloDigital.getSelloDigital(byteCer, byteKey, _pass, CadenaOriginal, TipoAlgoritmo.SHA256);

            if (sello[0] != "")
            {
                Sello = sello[0];

                XmlNode oNodo;

                oNodo = xmlDoc.DocumentElement;

                if (oNodo.Name == "cfdi:Comprobante")
                    oNodo.Attributes["Sello"].Value = Sello;

                xmlDoc.Save(stringXML[0]);

                return stringXML;
            }
            else
            {
                stringXML[0] = "";
                stringXML[1] = "Error al generar el sello.|";

                return stringXML;
            }
        }

        /// <summary>
        /// //metodo encargado de generar un xml con estructura basica retornando un valor de tipo stream  
        /// </summary>
        /// <returns></returns>
        private Stream GetXMLBasico(int nRedondeo)
        {
            int redondeo = nRedondeo == 0 ? 6 : nRedondeo;
            Stream streamXML = new MemoryStream();

            streamXML = CreaXML_Basico(redondeo);

            return streamXML;
        }
        private Stream GetXMLBasico()
        {
            Stream streamXML = new MemoryStream();

            streamXML = CreaXML_Basico();

            return streamXML;
        }

        /// <summary>
        /// //metodo encargado de generar un xml con estructura basica retornando un valor de tipo string 
        /// </summary>
        /// <param name="_PathSalida"></param>
        /// <returns></returns>
        private string GetXMLBasico(string _PathSalida)
        {
            string rutaXML = _PathSalida + "\\" + Emisor.RFC + Serie + Folio.ToString() + ".xml";

            Stream streamXML = new MemoryStream();

            streamXML = CreaXML_Basico();

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(streamXML);

            xmlDoc.Save(rutaXML);

            return rutaXML;
        }
        /// <summary>
        /// //Metodo encargado de crear estructura xml
        /// </summary>
        /// <param name="streamXML"></param>
        /// <returns></returns>
        /// 

        private Stream CreaXML_Basico()
        {
            Stream streamXML = new MemoryStream();
            Boolean exentoT = false;
            Boolean exentoR = false;

            XmlTextWriter mywriter = new XmlTextWriter(streamXML, Encoding.UTF8);
            mywriter.Indentation = 4;
            mywriter.IndentChar = ' ';
            mywriter.Formatting = Formatting.Indented;

            mywriter.WriteStartDocument();
            //*********************************************************COMPROBANTE*******************************************************************                       

            mywriter.WriteStartElement("cfdi:Comprobante");
            mywriter.WriteAttributeString("xmlns:cfdi", "http://www.sat.gob.mx/cfd/3");
            mywriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            mywriter.WriteAttributeString("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd");

            mywriter.WriteAttributeString("Version", Version);

            if (!string.IsNullOrEmpty(Serie))
                mywriter.WriteAttributeString("Serie", Serie);

            if (!string.IsNullOrEmpty(Folio))
                mywriter.WriteAttributeString("Folio", Folio);

            mywriter.WriteAttributeString("Fecha", FuncionesGlobales.getString("SAT", Fecha));

            mywriter.WriteAttributeString("Sello", Sello);

            if (!string.IsNullOrEmpty(FormaPago))
                mywriter.WriteAttributeString("FormaPago", FormaPago);

            mywriter.WriteAttributeString("NoCertificado", NoCertificado);

            mywriter.WriteAttributeString("Certificado", Certificado);

            if (!string.IsNullOrEmpty(CondicionesPago))
                mywriter.WriteAttributeString("CondicionesDePago", CondicionesPago);

            mywriter.WriteAttributeString("SubTotal", String.Format("{0:0.00}", SubTotal));

            if (Descuento > 0)
                mywriter.WriteAttributeString("Descuento", String.Format("{0:0.00}", Descuento));

            mywriter.WriteAttributeString("Moneda", Moneda);

            if (TipoCambio > 0)
                mywriter.WriteAttributeString("TipoCambio", TipoCambio.ToString());

            mywriter.WriteAttributeString("Total", String.Format("{0:0.00}", Total));

            mywriter.WriteAttributeString("TipoDeComprobante", TipoDeComprobante);

            if (!string.IsNullOrEmpty(MetodoPago))
                mywriter.WriteAttributeString("MetodoPago", MetodoPago);

            mywriter.WriteAttributeString("LugarExpedicion", LugarExpedicion);

            if (!string.IsNullOrEmpty(Confirmacion))
                mywriter.WriteAttributeString("Confirmacion", Confirmacion);

            //*************************************************CFDI RELACIONADOS************************************************
            if (CfdiRelacionados.Cfdi_Relacionados.Any())
            {
                mywriter.WriteStartElement("cfdi:CfdiRelacionados");
                mywriter.WriteAttributeString("TipoRelacion", CfdiRelacionados.TipoRelacion);

                foreach (CfdiRelaccionado unCFDIRelacionado in CfdiRelacionados.Cfdi_Relacionados)
                {
                    mywriter.WriteStartElement("cfdi:CfdiRelacionado");
                    mywriter.WriteAttributeString("UUID", unCFDIRelacionado.UUID);
                    mywriter.WriteEndElement();
                }
                mywriter.WriteEndElement();
            }
            //*************************************************EMISOR**********************************************************
            mywriter.WriteStartElement("cfdi:Emisor");

            mywriter.WriteAttributeString("Rfc", Emisor.RFC);

            if (!string.IsNullOrEmpty(Emisor.Nombre))
                mywriter.WriteAttributeString("Nombre", Emisor.Nombre);

            mywriter.WriteAttributeString("RegimenFiscal", Emisor.RegimenFiscal);

            mywriter.WriteEndElement();

            //*************************************************RECEPTOR**********************************************************
            mywriter.WriteStartElement("cfdi:Receptor");

            mywriter.WriteAttributeString("Rfc", Receptor.RFC);

            if (!string.IsNullOrEmpty(Receptor.Nombre))
                mywriter.WriteAttributeString("Nombre", Receptor.Nombre);

            if (!string.IsNullOrEmpty(Receptor.ResidenciaFiscal))
                mywriter.WriteAttributeString("ResidenciaFiscal", Receptor.ResidenciaFiscal);

            if (!string.IsNullOrEmpty(Receptor.NumRegIdTrib))
                mywriter.WriteAttributeString("NumRegIdTrib", Receptor.NumRegIdTrib);

            mywriter.WriteAttributeString("UsoCFDI", Receptor.UsoCFDI);

            mywriter.WriteEndElement();

            //*************************************************CONCEPTOS*********************************************************
            mywriter.WriteStartElement("cfdi:Conceptos");

            foreach (Concepto unConcepto in Conceptos)
            {
                mywriter.WriteStartElement("cfdi:Concepto");

                mywriter.WriteAttributeString("ClaveProdServ", unConcepto.ClaveProdServ);

                if (!string.IsNullOrEmpty(unConcepto.NoIdentificacion))
                    mywriter.WriteAttributeString("NoIdentificacion", unConcepto.NoIdentificacion);

                mywriter.WriteAttributeString("Cantidad", unConcepto.Cantidad.ToString());

                mywriter.WriteAttributeString("ClaveUnidad", unConcepto.ClaveUnidad);

                if (!string.IsNullOrEmpty(unConcepto.Unidad))
                    mywriter.WriteAttributeString("Unidad", unConcepto.Unidad);

                mywriter.WriteAttributeString("Descripcion", unConcepto.Descripcion);

                mywriter.WriteAttributeString("ValorUnitario", unConcepto.ValorUnitario.ToString("N" + 2).Replace(",", ""));

                mywriter.WriteAttributeString("Importe", unConcepto.Importe.ToString("N" + 2).Replace(",", ""));

                if (unConcepto.Descuento > 0)
                    mywriter.WriteAttributeString("Descuento", unConcepto.Descuento.ToString("N" + 2).Replace(",", ""));

                //************************************************************************IMPUESTOS DEL CONCEPTO***********************************************************               

                if (unConcepto.Impuestos.Traslados.Any() || unConcepto.Impuestos.Retenciones.Any())
                {
                    mywriter.WriteStartElement("cfdi:Impuestos");

                    if (unConcepto.Impuestos.Traslados.Any())
                    {
                        mywriter.WriteStartElement("cfdi:Traslados");

                        foreach (Traslado unTraslado in unConcepto.Impuestos.Traslados)
                        {
                            mywriter.WriteStartElement("cfdi:Traslado");

                            mywriter.WriteAttributeString("Base", unTraslado.Base.ToString());

                            mywriter.WriteAttributeString("Impuesto", unTraslado.Impuesto);

                            mywriter.WriteAttributeString("TipoFactor", unTraslado.TipoFactor);

                            if (unTraslado.TipoFactor == "Exento")
                                exentoT = true;
                            else
                                exentoT = false;

                            if (unTraslado.TasaOCuota >= 0 && exentoT == false)
                                mywriter.WriteAttributeString("TasaOCuota", unTraslado.TasaOCuota.ToString("N" + 6));

                            if (unTraslado.Importe >= 0 && exentoT == false)
                                mywriter.WriteAttributeString("Importe", unTraslado.Importe.ToString());

                            mywriter.WriteEndElement();
                        }

                        mywriter.WriteEndElement();
                    }

                    if (unConcepto.Impuestos.Retenciones.Any())
                    {
                        mywriter.WriteStartElement("cfdi:Retenciones");

                        foreach (Retencion unaRetencion in unConcepto.Impuestos.Retenciones)
                        {
                            mywriter.WriteStartElement("cfdi:Retencion");

                            mywriter.WriteAttributeString("Base", unaRetencion.Base.ToString());

                            mywriter.WriteAttributeString("Impuesto", unaRetencion.Impuesto);

                            mywriter.WriteAttributeString("TipoFactor", unaRetencion.TipoFactor);

                            if (unaRetencion.TipoFactor == "Exento")
                                exentoR = true;
                            else
                                exentoR = false;

                            if (unaRetencion.TasaOCuota >= 0 && exentoR == false)
                                mywriter.WriteAttributeString("TasaOCuota", unaRetencion.TasaOCuota.ToString("N" + 6));

                            if (unaRetencion.Importe >= 0 && exentoR == false)
                                mywriter.WriteAttributeString("Importe", unaRetencion.Importe.ToString());

                            mywriter.WriteEndElement();
                        }

                        mywriter.WriteEndElement();
                    }

                    mywriter.WriteEndElement();
                }

                //************************************************************************INFORMACION ADUANERA***********************************************************
                if (unConcepto.InformacionAduanera.Any())
                {
                    foreach (InformacionAduanera unaInfoAduanera in unConcepto.InformacionAduanera)
                    {
                        mywriter.WriteStartElement("cfdi:InformacionAduanera");

                        mywriter.WriteAttributeString("NumeroPedimento", unaInfoAduanera.NumeroPedimento);

                        mywriter.WriteEndElement();
                    }
                }
                //************************************************************************CUENTA PREDIAL***********************************************************
                if (!string.IsNullOrEmpty(unConcepto.Cuenta_Predial.Numero))
                {
                    mywriter.WriteStartElement("cfdi:CuentaPredial");

                    mywriter.WriteAttributeString("Numero", unConcepto.Cuenta_Predial.Numero);

                    mywriter.WriteEndElement();
                }
                //************************************************************************COMPLEMENTO CONCEPTO***********************************************************


                //mywriter.WriteStartElement("cfdi:ComplementoConcepto");
                //mywriter.WriteEndElement();

                //************************************************************************PARTE***********************************************************
                if (unConcepto.Parte.Any())
                {
                    foreach (Parte unaParte in unConcepto.Parte)
                    {
                        mywriter.WriteStartElement("cfdi:Parte");

                        mywriter.WriteAttributeString("ClaveProdServ", unaParte.ClaveProdServ);

                        if (!string.IsNullOrEmpty(unaParte.NoIdentificacion))
                            mywriter.WriteAttributeString("NoIdentificacion", unaParte.NoIdentificacion);

                        mywriter.WriteAttributeString("Cantidad", unaParte.Cantidad.ToString());

                        if (!string.IsNullOrEmpty(unaParte.Unidad))
                            mywriter.WriteAttributeString("Unidad", unaParte.Unidad);

                        mywriter.WriteAttributeString("Descripcion", unaParte.Descripcion);

                        if (unaParte.ValorUnitario > 0)
                            mywriter.WriteAttributeString("ValorUnitario", String.Format("{0:0.00}", unaParte.ValorUnitario));

                        if (unaParte.Importe > 0)
                            mywriter.WriteAttributeString("Importe", String.Format("{0:0.00}", unaParte.Importe));

                        foreach (InformacionAduanera unaInfoAduanera in unaParte.InformacionAduanera)
                        {
                            mywriter.WriteStartElement("cfdi:InformacionAduanera");

                            mywriter.WriteAttributeString("NumeroPedimento", unaInfoAduanera.NumeroPedimento);

                            mywriter.WriteEndElement();
                        }

                        mywriter.WriteEndElement();
                    }
                }

                mywriter.WriteEndElement();
            }

            mywriter.WriteEndElement();

            //*************************************************IMPUESTOS*********************************************************
            if (Impuestos.Traslados.Any() || Impuestos.Retenciones.Any())
            {
                mywriter.WriteStartElement("cfdi:Impuestos");

                if (Impuestos.Retenciones.Any())
                {
                    if ((Impuestos.TotalImpuestosRetenidos >= 0) || (exentoR == true))
                        mywriter.WriteAttributeString("TotalImpuestosRetenidos", String.Format("{0:0.00}", Math.Round(Impuestos.TotalImpuestosRetenidos, 2)));
                }

                if (Impuestos.Traslados.Any())
                {
                    if ((Impuestos.TotalImpuestosTrasladados >= 0) || (exentoT == true))
                        mywriter.WriteAttributeString("TotalImpuestosTrasladados", String.Format("{0:0.00}", Math.Round(Impuestos.TotalImpuestosTrasladados, 2)));
                }

                if (Impuestos.Retenciones.Any())
                {
                    mywriter.WriteStartElement("cfdi:Retenciones");

                    foreach (Retencion unaRetencion in Impuestos.Retenciones)
                    {
                        mywriter.WriteStartElement("cfdi:Retencion");

                        mywriter.WriteAttributeString("Impuesto", unaRetencion.Impuesto);

                        mywriter.WriteAttributeString("Importe", String.Format("{0:0.00}", unaRetencion.Importe));

                        mywriter.WriteEndElement();
                    }

                    mywriter.WriteEndElement();
                }

                if (Impuestos.Traslados.Any())
                {
                    mywriter.WriteStartElement("cfdi:Traslados");

                    foreach (Traslado unTraslado in Impuestos.Traslados)
                    {
                        mywriter.WriteStartElement("cfdi:Traslado");

                        mywriter.WriteAttributeString("Impuesto", unTraslado.Impuesto);

                        mywriter.WriteAttributeString("TipoFactor", unTraslado.TipoFactor);

                        mywriter.WriteAttributeString("TasaOCuota", unTraslado.TasaOCuota.ToString("N" + 6));

                        mywriter.WriteAttributeString("Importe", String.Format("{0:0.00}", unTraslado.Importe));

                        mywriter.WriteEndElement();
                    }

                    mywriter.WriteEndElement();
                }


                mywriter.WriteEndElement();
            }
            //*************************************************COMPLEMENTO*********************************************************
            mywriter.WriteStartElement("cfdi:Complemento");
            mywriter.WriteEndElement();
            //*************************************************ADDENDA*********************************************************

            //mywriter.WriteStartElement("cfdi:Addenda");
            //mywriter.WriteEndElement();

            //mywriter.WriteEndElement();

            mywriter.WriteEndDocument();

            mywriter.Flush();


            streamXML.Position = 0;

            return streamXML;
        }
        private Stream CreaXML_Basico(int redondeo)
        {
            Stream streamXML = new MemoryStream();
            Boolean exentoT = false;
            Boolean exentoR = false;

            XmlTextWriter mywriter = new XmlTextWriter(streamXML, Encoding.UTF8);
            mywriter.Indentation = 4;
            mywriter.IndentChar = ' ';
            mywriter.Formatting = Formatting.Indented;

            mywriter.WriteStartDocument();
            //*********************************************************COMPROBANTE*******************************************************************                       

            mywriter.WriteStartElement("cfdi:Comprobante");
            mywriter.WriteAttributeString("xmlns:cfdi", "http://www.sat.gob.mx/cfd/3");
            mywriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            mywriter.WriteAttributeString("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd");

            mywriter.WriteAttributeString("Version", Version);

            if (!string.IsNullOrEmpty(Serie))
                mywriter.WriteAttributeString("Serie", Serie);

            if (!string.IsNullOrEmpty(Folio))
                mywriter.WriteAttributeString("Folio", Folio);

            mywriter.WriteAttributeString("Fecha", FuncionesGlobales.getString("SAT", Fecha));

            mywriter.WriteAttributeString("Sello", Sello);

            if (!string.IsNullOrEmpty(FormaPago))
                mywriter.WriteAttributeString("FormaPago", FormaPago);

            mywriter.WriteAttributeString("NoCertificado", NoCertificado);

            mywriter.WriteAttributeString("Certificado", Certificado);

            if (!string.IsNullOrEmpty(CondicionesPago))
                mywriter.WriteAttributeString("CondicionesDePago", CondicionesPago);

            mywriter.WriteAttributeString("SubTotal", String.Format("{0:0.00}", SubTotal));

            if (Descuento > 0)
                mywriter.WriteAttributeString("Descuento", String.Format("{0:0.00}", Descuento));

            mywriter.WriteAttributeString("Moneda", Moneda);

            if (TipoCambio > 0)
                mywriter.WriteAttributeString("TipoCambio", TipoCambio.ToString());

            mywriter.WriteAttributeString("Total", String.Format("{0:0.00}", Total));

            mywriter.WriteAttributeString("TipoDeComprobante", TipoDeComprobante);

            if (!string.IsNullOrEmpty(MetodoPago))
                mywriter.WriteAttributeString("MetodoPago", MetodoPago);

            mywriter.WriteAttributeString("LugarExpedicion", LugarExpedicion);

            if (!string.IsNullOrEmpty(Confirmacion))
                mywriter.WriteAttributeString("Confirmacion", Confirmacion);

            //*************************************************CFDI RELACIONADOS************************************************
            if (CfdiRelacionados.Cfdi_Relacionados.Any())
            {
                mywriter.WriteStartElement("cfdi:CfdiRelacionados");
                mywriter.WriteAttributeString("TipoRelacion", CfdiRelacionados.TipoRelacion);

                foreach (CfdiRelaccionado unCFDIRelacionado in CfdiRelacionados.Cfdi_Relacionados)
                {
                    mywriter.WriteStartElement("cfdi:CfdiRelacionado");
                    mywriter.WriteAttributeString("UUID", unCFDIRelacionado.UUID);
                    mywriter.WriteEndElement();
                }
                mywriter.WriteEndElement();
            }
            //*************************************************EMISOR**********************************************************
            mywriter.WriteStartElement("cfdi:Emisor");

            mywriter.WriteAttributeString("Rfc", Emisor.RFC);

            if (!string.IsNullOrEmpty(Emisor.Nombre))
                mywriter.WriteAttributeString("Nombre", Emisor.Nombre);

            mywriter.WriteAttributeString("RegimenFiscal", Emisor.RegimenFiscal);

            mywriter.WriteEndElement();

            //*************************************************RECEPTOR**********************************************************
            mywriter.WriteStartElement("cfdi:Receptor");

            mywriter.WriteAttributeString("Rfc", Receptor.RFC);

            if (!string.IsNullOrEmpty(Receptor.Nombre))
                mywriter.WriteAttributeString("Nombre", Receptor.Nombre);

            if (!string.IsNullOrEmpty(Receptor.ResidenciaFiscal))
                mywriter.WriteAttributeString("ResidenciaFiscal", Receptor.ResidenciaFiscal);

            if (!string.IsNullOrEmpty(Receptor.NumRegIdTrib))
                mywriter.WriteAttributeString("NumRegIdTrib", Receptor.NumRegIdTrib);

            mywriter.WriteAttributeString("UsoCFDI", Receptor.UsoCFDI);

            mywriter.WriteEndElement();

            //*************************************************CONCEPTOS*********************************************************
            mywriter.WriteStartElement("cfdi:Conceptos");

            foreach (Concepto unConcepto in Conceptos)
            {
                mywriter.WriteStartElement("cfdi:Concepto");

                mywriter.WriteAttributeString("ClaveProdServ", unConcepto.ClaveProdServ);

                if (!string.IsNullOrEmpty(unConcepto.NoIdentificacion))
                    mywriter.WriteAttributeString("NoIdentificacion", unConcepto.NoIdentificacion);

                mywriter.WriteAttributeString("Cantidad", unConcepto.Cantidad.ToString());

                mywriter.WriteAttributeString("ClaveUnidad", unConcepto.ClaveUnidad);

                if (!string.IsNullOrEmpty(unConcepto.Unidad))
                    mywriter.WriteAttributeString("Unidad", unConcepto.Unidad);

                mywriter.WriteAttributeString("Descripcion", unConcepto.Descripcion);

                mywriter.WriteAttributeString("ValorUnitario", unConcepto.ValorUnitario.ToString("N" + redondeo).Replace(",", ""));

                mywriter.WriteAttributeString("Importe", unConcepto.Importe.ToString("N" + redondeo).Replace(",", ""));

                if (unConcepto.Descuento > 0)
                    mywriter.WriteAttributeString("Descuento", unConcepto.Descuento.ToString("N" + redondeo).Replace(",", ""));
                //mywriter.WriteAttributeString("Descuento", unConcepto.Descuento.ToString("N" + 2).Replace(",", ""));

                //************************************************************************IMPUESTOS DEL CONCEPTO***********************************************************               

                if (unConcepto.Impuestos.Traslados.Any() || unConcepto.Impuestos.Retenciones.Any())
                {
                    mywriter.WriteStartElement("cfdi:Impuestos");

                    if (unConcepto.Impuestos.Traslados.Any())
                    {
                        mywriter.WriteStartElement("cfdi:Traslados");

                        foreach (Traslado unTraslado in unConcepto.Impuestos.Traslados)
                        {
                            mywriter.WriteStartElement("cfdi:Traslado");

                            mywriter.WriteAttributeString("Base", unTraslado.Base.ToString());

                            mywriter.WriteAttributeString("Impuesto", unTraslado.Impuesto);

                            mywriter.WriteAttributeString("TipoFactor", unTraslado.TipoFactor);

                            if (unTraslado.TipoFactor == "Exento")
                                exentoT = true;
                            else
                                exentoT = false;

                            if (unTraslado.TasaOCuota >= 0 && exentoT == false)
                                mywriter.WriteAttributeString("TasaOCuota", unTraslado.TasaOCuota.ToString("N" + 6).Replace(",", ""));

                            if (unTraslado.Importe >= 0 && exentoT == false)
                                mywriter.WriteAttributeString("Importe", unTraslado.Importe.ToString());

                            mywriter.WriteEndElement();
                        }

                        mywriter.WriteEndElement();
                    }

                    if (unConcepto.Impuestos.Retenciones.Any())
                    {
                        mywriter.WriteStartElement("cfdi:Retenciones");

                        foreach (Retencion unaRetencion in unConcepto.Impuestos.Retenciones)
                        {
                            mywriter.WriteStartElement("cfdi:Retencion");

                            mywriter.WriteAttributeString("Base", unaRetencion.Base.ToString());

                            mywriter.WriteAttributeString("Impuesto", unaRetencion.Impuesto);

                            mywriter.WriteAttributeString("TipoFactor", unaRetencion.TipoFactor);

                            if (unaRetencion.TipoFactor == "Exento")
                                exentoR = true;
                            else
                                exentoR = false;

                            if (unaRetencion.TasaOCuota >= 0 && exentoR == false)
                                mywriter.WriteAttributeString("TasaOCuota", unaRetencion.TasaOCuota.ToString("N" + 6).Replace(",", ""));

                            if (unaRetencion.Importe >= 0 && exentoR == false)
                                mywriter.WriteAttributeString("Importe", unaRetencion.Importe.ToString());

                            mywriter.WriteEndElement();
                        }

                        mywriter.WriteEndElement();
                    }

                    mywriter.WriteEndElement();
                }

                //************************************************************************INFORMACION ADUANERA***********************************************************
                if (unConcepto.InformacionAduanera.Any())
                {
                    foreach (InformacionAduanera unaInfoAduanera in unConcepto.InformacionAduanera)
                    {
                        mywriter.WriteStartElement("cfdi:InformacionAduanera");

                        mywriter.WriteAttributeString("NumeroPedimento", unaInfoAduanera.NumeroPedimento);

                        mywriter.WriteEndElement();
                    }
                }
                //************************************************************************CUENTA PREDIAL***********************************************************
                if (!string.IsNullOrEmpty(unConcepto.Cuenta_Predial.Numero))
                {
                    mywriter.WriteStartElement("cfdi:CuentaPredial");

                    mywriter.WriteAttributeString("Numero", unConcepto.Cuenta_Predial.Numero);

                    mywriter.WriteEndElement();
                }
                //************************************************************************COMPLEMENTO CONCEPTO***********************************************************


                //mywriter.WriteStartElement("cfdi:ComplementoConcepto");
                //mywriter.WriteEndElement();

                //************************************************************************PARTE***********************************************************
                if (unConcepto.Parte.Any())
                {
                    foreach (Parte unaParte in unConcepto.Parte)
                    {
                        mywriter.WriteStartElement("cfdi:Parte");

                        mywriter.WriteAttributeString("ClaveProdServ", unaParte.ClaveProdServ);

                        if (!string.IsNullOrEmpty(unaParte.NoIdentificacion))
                            mywriter.WriteAttributeString("NoIdentificacion", unaParte.NoIdentificacion);

                        mywriter.WriteAttributeString("Cantidad", unaParte.Cantidad.ToString());

                        if (!string.IsNullOrEmpty(unaParte.Unidad))
                            mywriter.WriteAttributeString("Unidad", unaParte.Unidad);

                        mywriter.WriteAttributeString("Descripcion", unaParte.Descripcion);

                        if (unaParte.ValorUnitario > 0)
                            mywriter.WriteAttributeString("ValorUnitario", String.Format("{0:0.00}", unaParte.ValorUnitario));

                        if (unaParte.Importe > 0)
                            mywriter.WriteAttributeString("Importe", String.Format("{0:0.00}", unaParte.Importe));

                        foreach (InformacionAduanera unaInfoAduanera in unaParte.InformacionAduanera)
                        {
                            mywriter.WriteStartElement("cfdi:InformacionAduanera");

                            mywriter.WriteAttributeString("NumeroPedimento", unaInfoAduanera.NumeroPedimento);

                            mywriter.WriteEndElement();
                        }

                        mywriter.WriteEndElement();
                    }
                }

                mywriter.WriteEndElement();
            }

            mywriter.WriteEndElement();

            //*************************************************IMPUESTOS*********************************************************
            if ((Impuestos.TotalImpuestosTrasladados + Impuestos.TotalImpuestosRetenidos) > 0)
            {
                if (Impuestos.Traslados.Any() || Impuestos.Retenciones.Any())
                {
                    mywriter.WriteStartElement("cfdi:Impuestos");

                    if (Impuestos.Retenciones.Any())
                    {
                        if ((Impuestos.TotalImpuestosRetenidos >= 0) || (exentoR == true))
                            mywriter.WriteAttributeString("TotalImpuestosRetenidos", String.Format("{0:0.00}", Math.Round(Impuestos.TotalImpuestosRetenidos, 2)));
                    }

                    if (Impuestos.Traslados.Any())
                    {
                        if ((Impuestos.TotalImpuestosTrasladados >= 0))
                            mywriter.WriteAttributeString("TotalImpuestosTrasladados", String.Format("{0:0.00}", Math.Round(Impuestos.TotalImpuestosTrasladados, 2)));
                    }

                    if (Impuestos.Retenciones.Any())
                    {
                        mywriter.WriteStartElement("cfdi:Retenciones");

                        foreach (Retencion unaRetencion in Impuestos.Retenciones)
                        {
                            mywriter.WriteStartElement("cfdi:Retencion");

                            mywriter.WriteAttributeString("Impuesto", unaRetencion.Impuesto);

                            mywriter.WriteAttributeString("Importe", String.Format("{0:0.00}", unaRetencion.Importe));

                            mywriter.WriteEndElement();
                        }

                        mywriter.WriteEndElement();
                    }

                    if (Impuestos.Traslados.Any())
                    {
                        mywriter.WriteStartElement("cfdi:Traslados");

                        foreach (Traslado unTraslado in Impuestos.Traslados)
                        {
                            if (unTraslado.TipoFactor != "Exento" || unTraslado.Importe > 0)
                            {
                                mywriter.WriteStartElement("cfdi:Traslado");

                                mywriter.WriteAttributeString("Impuesto", unTraslado.Impuesto);

                                mywriter.WriteAttributeString("TipoFactor", unTraslado.TipoFactor);

                                mywriter.WriteAttributeString("TasaOCuota", unTraslado.TasaOCuota.ToString("N" + 6).Replace(",", ""));

                                mywriter.WriteAttributeString("Importe", String.Format("{0:0.00}", unTraslado.Importe));

                                mywriter.WriteEndElement();
                            }
                        }

                        mywriter.WriteEndElement();
                    }



                    mywriter.WriteEndElement();
                }
            }
            //*************************************************COMPLEMENTO*********************************************************
            //mywriter.WriteStartElement("cfdi:Complemento");
            //mywriter.WriteEndElement();
            //*************************************************ADDENDA*********************************************************

            //mywriter.WriteStartElement("cfdi:Addenda");
            //mywriter.WriteEndElement();

            //mywriter.WriteEndElement();

            mywriter.WriteEndDocument();

            mywriter.Flush();


            streamXML.Position = 0;

            return streamXML;
        }

        /// <summary>
        /// //Metodo encargado de generar la Cadena Original del cfdi
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        private string generarCadenaOriginal(string xmlString)
        {
            string Result = "";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.LoadXml(xmlString);

                XslCompiledTransform xsl = new XslCompiledTransform();
                MemoryStream mem = new MemoryStream();
                byte[] bytesCadenaOriginal = null;

                xsl.Load(System.Reflection.Assembly.Load("cadenaoriginal_3_3").GetType("cadenaoriginal_3_3"));
                xsl.Transform(xmlDoc, null, mem);

                /*
                using (var reader = new StringReader(CFDI33.Properties.Resources.cadenaoriginal_3_3))
                {
                    using (XmlReader xmlReader = XmlReader.Create(reader))
                    {
                        //xsl.Load(xmlReader);
                        //xsl.Load("C:\\Desarrollo de Software\\ULP\\GIT\\SIP80.NET\\CFDI33\\Esquemas\\cadenaoriginal_3_3.xslt");
                        xsl.Transform(xmlDoc, null, mem);
                        xsl = null;
                    }
                }
                */


                bytesCadenaOriginal = mem.GetBuffer();
                //DETECTAMOS LA PRECENCIA DEL BOM
                int startPoint = 0;
                int endPoint = 0;

                endPoint = bytesCadenaOriginal.Length;

                if (endPoint >= 3 && bytesCadenaOriginal[0] == 0xEF && bytesCadenaOriginal[1] == 0xBB && bytesCadenaOriginal[2] == 0xBF)
                {
                    startPoint += 3;
                    endPoint -= 3;

                    CadenaOriginal = Encoding.UTF8.GetString(bytesCadenaOriginal, startPoint, endPoint).Trim().Replace("\0", "");
                }
                else
                    CadenaOriginal = Encoding.UTF8.GetString(bytesCadenaOriginal).Trim().Replace("\0", "");


                Result = "";
            }
            catch (Exception ex)
            {
                Result = "Error en cadena original: " + ex.Message;
            }

            return Result;
        }

        /// <summary>
        /// //Metodo encargado de validar todos los objetos relacionados
        /// </summary>
        /// <returns></returns>
        private string valida()
        {
            string result = "";

            if (string.IsNullOrEmpty(NoCertificado))
                result += "Sin NoCertificado (Comprobante) |";

            if (string.IsNullOrEmpty(Certificado))
                result += "Sin Certificado (Comprobante) |";

            if (string.IsNullOrEmpty(Moneda))
                result += "Sin Moneda (Comprobante) |";

            if (string.IsNullOrEmpty(TipoDeComprobante))
                result += "Sin TipoDeComprobante (Comprobante) |";

            if (string.IsNullOrEmpty(LugarExpedicion))
                result += "Sin LugarExpedicion (Comprobante) |";


            result += Emisor.valida();
            result += Receptor.valida();

            return result;
        }
        private string cerosIzq(string Numero)
        {
            string num;
            num = Numero.PadLeft(12, '0');
            return num;
        }
        /// <summary>
        /// //Metodo encargado de cortar espacios de todos los objetos tipo string
        /// </summary>
        /// <returns></returns>
        private string mondar()
        {
            string result = "";
            //Comprobante     
            result += FuncionesGlobales.mondar(this);
            //CFDI Relacionados
            result += CfdiRelacionados.mondar();
            //CFDI Relacionados Detalle
            foreach (CfdiRelaccionado unCfdiRelacionado in CfdiRelacionados.Cfdi_Relacionados)
            {
                result += unCfdiRelacionado.mondar();
            }
            //Emisor
            result += Emisor.mondar();
            //Receptor
            result += Receptor.mondar();
            //Conceptos
            foreach (Concepto unConcepto in Conceptos)
            {
                result += unConcepto.mondar();

                foreach (Parte unaParte in unConcepto.Parte)
                {
                    result += unaParte.mondar();
                }
            }


            return result;
        }
        private void calculo(int nRedonde)
        {
            SubTotal = 0;
            Total = 0;
            int redondeo = nRedonde == 0 ? 6 : nRedonde;

            foreach (Concepto unConcepto in Conceptos)
            {
                SubTotal += unConcepto.Importe;
                Descuento += unConcepto.Descuento;

                foreach (Traslado unTraslado in unConcepto.Impuestos.Traslados)
                {
                    //SE HACE EL ADD AND GROUP
                    //Impuestos.TotalImpuestosTrasladados += Math.Round(unTraslado.Importe, redondeo);

                    if (!Impuestos.Traslados.Exists(c => c.Impuesto == unTraslado.Impuesto && c.TasaOCuota == unTraslado.TasaOCuota && c.TipoFactor == unTraslado.TipoFactor))
                    {
                        Traslado oTraslado = new Traslado();

                        oTraslado.Impuesto = unTraslado.Impuesto;
                        oTraslado.TasaOCuota = unTraslado.TasaOCuota;
                        oTraslado.TipoFactor = unTraslado.TipoFactor;
                        oTraslado.Importe = Math.Round(unTraslado.Importe, redondeo);

                        Impuestos.Traslados.Add(oTraslado);
                    }
                    else
                    {
                        Traslado oTraslado = Impuestos.Traslados.FirstOrDefault(c => c.Impuesto == unTraslado.Impuesto && c.TasaOCuota == unTraslado.TasaOCuota && c.TipoFactor == unTraslado.TipoFactor);

                        oTraslado.Importe += Math.Round(unTraslado.Importe, redondeo);
                    }
                }
                foreach (Retencion unaRetencion in unConcepto.Impuestos.Retenciones)
                {
                    //Impuestos.TotalImpuestosRetenidos += Math.Round(unaRetencion.Importe, redondeo);

                    if (!Impuestos.Retenciones.Exists(c => c.Impuesto == unaRetencion.Impuesto))
                    {
                        Retencion oRetencion = new Retencion();

                        oRetencion.Impuesto = unaRetencion.Impuesto;
                        oRetencion.Importe = Math.Round(unaRetencion.Importe, redondeo);

                        Impuestos.Retenciones.Add(oRetencion);
                    }
                    else
                    {
                        Retencion oRetencion = Impuestos.Retenciones.FirstOrDefault(c => c.Impuesto == unaRetencion.Impuesto);

                        oRetencion.Importe += Math.Round(unaRetencion.Importe, redondeo);
                    }
                }


                Total = SubTotal;
            }

            if (Impuestos.Retenciones.Any())
            {

                foreach (Retencion unaRetencion in Impuestos.Retenciones)
                {
                    Decimal auxRet = Math.Round(unaRetencion.Importe, 2, MidpointRounding.AwayFromZero);
                    Impuestos.TotalImpuestosRetenidos += auxRet;
                }
            }

            if (Impuestos.Traslados.Any())
            {

                foreach (Traslado unTraslado in Impuestos.Traslados)
                {
                    Decimal auxTras = Math.Round(unTraslado.Importe, 2, MidpointRounding.AwayFromZero);
                    Impuestos.TotalImpuestosTrasladados += auxTras;

                }
            }

            Total = Total + Impuestos.TotalImpuestosTrasladados - Impuestos.TotalImpuestosRetenidos - Descuento;
        }





        #endregion
    }
}
