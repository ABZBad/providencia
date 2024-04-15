using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class DatosModelo
    {
        public string MODELO { set; get; } //varchar(8)
        public string DESCR { set; get; } //varchar(40)
        public string LIN_PROD { set; get; } //varchar(5)
        public string DESC_LIN { set; get; } //varchar(20)
        public double PRECIO { set; get; } //float
        public string CVE_ART { set; get; } //varchar(16)
        public string OBSERVACIONES { set; get; } //varchar(255)
        public double PESO { get; set; } //float
        public string ETIQUETA { get; set; } //VARCHAR(255)
        public string AGRUPACION { get; set; } //VARCHAR(255)

    }
    public class ModeloYTallas
    {
        public DatosModelo DatosModelo { get; set; }
        public DataTable CodigosExistentes { get; set; }
        public List<int> Almacenes { get; set; }
    }
    public class DupCodProdEstr
    {
        private static void Log(string Message)
        {
            Debug.WriteLine(string.Format("{0}>{1}", DateTime.Now.ToLongTimeString(), Message));
        }
        private static DataTable RegresaEstructuraCodigosExistentes()
        {
            DataTable dtCodigos = new DataTable();
            dtCodigos.Columns.Add(new DataColumn("TALLA", typeof(string)));
            dtCodigos.Columns.Add(new DataColumn("DESCR", typeof(string)));
            dtCodigos.Columns.Add(new DataColumn("ESTATUS", typeof(string)));
            dtCodigos.Columns.Add(new DataColumn("EXIST", typeof(double)));
            dtCodigos.Columns.Add(new DataColumn("ETIQUETA", typeof(string)));
            dtCodigos.Columns.Add(new DataColumn("Accion", typeof(string)));
            return dtCodigos;
        }
        public static ModeloYTallas RegresaDatosModelo(string Modelo)
        {
            ModeloYTallas modeloYTallas = new ModeloYTallas();

            DataSet dsDatosModelo = new DataSet();
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ModelosYTallasPorModelo";
            cmd.Parameters.Add(new SqlParameter("@modelo", Modelo + "%"));
            dsDatosModelo = cmd.GetDataSet();
            cmd.Connection.Close();


            modeloYTallas.DatosModelo = new DatosModelo();

            if (dsDatosModelo.Tables[0].Rows.Count > 0)
            {
                DataRow row = dsDatosModelo.Tables[0].Rows[0];

                //Asigna valores de los datos modelo
                modeloYTallas.DatosModelo.MODELO = (row["MODELO"] == System.DBNull.Value ? null : (string)row["MODELO"]);
                modeloYTallas.DatosModelo.DESCR = (row["DESCR"] == System.DBNull.Value ? null : (string)row["DESCR"]);
                modeloYTallas.DatosModelo.LIN_PROD = (row["LIN_PROD"] == System.DBNull.Value
                    ? null
                    : (string)row["LIN_PROD"]);
                modeloYTallas.DatosModelo.DESC_LIN = (string)row["DESC_LIN"];
                modeloYTallas.DatosModelo.PRECIO = (double)row["PRECIO"];
                modeloYTallas.DatosModelo.CVE_ART = (string)row["CVE_ART"];
                modeloYTallas.DatosModelo.OBSERVACIONES = (string)row["OBSERVACIONES"];
                modeloYTallas.DatosModelo.PESO = (double)row["PESO"];
                modeloYTallas.DatosModelo.ETIQUETA = (string)row["ETIQUETA"];
                modeloYTallas.DatosModelo.AGRUPACION = (string)row["AGRUPACION"];

                //Asigna valores de Códigos Existentes
                modeloYTallas.CodigosExistentes = dsDatosModelo.Tables[2];

                //Asigna valores de Almacenes Existentes
                DataTable dtAlmacenes = dsDatosModelo.Tables[1];
                modeloYTallas.Almacenes = new List<int>();
                foreach (DataRow rowAlmacen in dtAlmacenes.Rows)
                {
                    modeloYTallas.Almacenes.Add((int)rowAlmacen["CVE_ALM"]);
                }
            }
            else
            {
                modeloYTallas.CodigosExistentes = RegresaEstructuraCodigosExistentes();
            }
            //System.Threading.Thread.Sleep(6000);
            return modeloYTallas;
        }
        public static DataTable RegresaEstructuraExistente(string CVE_ART)
        {
            DataTable datos = new DataTable();
            sm_dl.SqlServer.SqlServerCommand resultado = new SqlServerCommand();
            using (var dbContext = new SIPNegocioContext())
            {
                resultado.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
            }
            resultado.ObjectName = "usp_ModelosYTallasPorModelo_EstructuraExistente";
            resultado.Parameters.Add(new SqlParameter("@CVE_ART", CVE_ART));
            datos = resultado.GetDataTable();
            return datos;
        }
        public static DataTable RegresaEstructuraExistente()
        {
            DataTable dtEstructura = new DataTable();
            dtEstructura.Columns.Add(new DataColumn("PT_ALMACEN", typeof(Int16)));
            dtEstructura.Columns.Add(new DataColumn("CLAVE", typeof(string)));
            dtEstructura.Columns.Add(new DataColumn("PROCESO", typeof(string)));
            dtEstructura.Columns.Add(new DataColumn("CANTIDAD", typeof(double)));
            dtEstructura.Columns.Add(new DataColumn("ALMACEN", typeof(Int16)));
            dtEstructura.Columns.Add(new DataColumn("COMPONENTE", typeof(string)));
            dtEstructura.Columns.Add(new DataColumn("TIPO", typeof(string)));
            dtEstructura.Columns.Add(new DataColumn("COSTOU", typeof(double)));
            return dtEstructura;

        }
        public static string RegresaDescripcionCodigo(string descripcionCodigo, string talla, string sufijo)
        {
            string valor = "";
            sufijo = sufijo.ToUpper().Replace("XX", talla.Substring(0, 2));
            sufijo = sufijo.ToUpper().Replace("YY", talla.Substring(2, 2));
            valor = descripcionCodigo + " " + sufijo;
            return valor;
        }
        /// <summary>
        /// Descripciones
        /// </summary>
        /// <param name="Texto">Texto a buscar</param>
        /// <param name="tipo">"1=búsqueda de productos en INVE01 2. búsqueda de insumos en INSUMOS01"</param>
        /// <param name="almacen">"Id del tipo de almacén sólo para cuando es  búsqueda de pruductos (para búsqueda de insumos en INSUMOS01 se debe enviar cero)"</param>
        /// <returns></returns>
        public static DataTable ComponenteConsultarCoincidencias(string Texto, int tipo, int almacen)
        {
            DataTable datos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand resultado = new sm_dl.SqlServer.SqlServerCommand();
                resultado.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                if (tipo == 1)
                {
                    resultado.ObjectName = "usp_Devuelve_INVE01_por_coincidencias";
                    resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@almacen", almacen));
                }
                else
                {
                    resultado.ObjectName = "usp_Devuelve_INSUMOS01_por_coincidencias";
                }
                resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@descr", Texto));
                datos = resultado.GetDataTable();
            }
            return datos;
        }
        public static DataTable UltimoCostoUnitarioConsultar(string tipo, string Componente, int almacen)
        {
            DataTable datos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                /*
                var resultado = (from res1 in dbContext.INVE01
                                 join res2 in dbContext.MULT01 on res1.CVE_ART equals res2.CVE_ART
                                 where res1.CVE_ART == Componente && res2.CVE_ALM == almacen
                                 select res1).FirstOrDefault();
                 * */
                sm_dl.SqlServer.SqlServerCommand resultado = new sm_dl.SqlServer.SqlServerCommand();
                resultado.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                resultado.ObjectName = "usp_DupCodProdEstr_Devuelve_UltimoCostoUnitario";
                resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tipo", tipo));
                resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Componente", Componente));
                resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@almacen", almacen));

                datos = resultado.GetDataTable();

            }
            return datos;

        }

        private static void ExecuteSQL(string sql, string connStr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        private static void EliminarPT(string CVE_ART, string ConnStr)
        {
            string strSQL = "";
            //' Eliminar Cabeza PT
            strSQL = string.Format("DELETE FROM PROD_PRODTERM01 WHERE CVE_ART='{0}'", CVE_ART);
            ExecuteSQL(strSQL, ConnStr);
            //' Eliminar Detalle PT
            strSQL = string.Format("DELETE FROM PROD_PRODTERM_DET01 WHERE CVE_ART='{0}'", CVE_ART);
            ExecuteSQL(strSQL, ConnStr);
        }
        private static void InsertarMULT01(string CVE_ART, int CVE_ALM, string connStr)
        {
            string strSQL = string.Format("INSERT INTO MULT01(CVE_ART, CVE_ALM, STATUS, EXIST, STOCK_MIN, STOCK_MAX, COMP_X_REC) VALUES ('{0}', {1}, 'A', 0, 0, 0, 0)", CVE_ART, CVE_ALM);
            ExecuteSQL(strSQL, connStr);
        }
        private static void InsertarPXP(string CVE_ART, double PRECIO, string ConnStr)
        {
            string strSQL = "";
            strSQL = string.Format("INSERT INTO PRECIO_X_PROD01(CVE_ART, CVE_PRECIO, PRECIO) VALUES ('{0}', 1, {1})", CVE_ART, PRECIO);
            ExecuteSQL(strSQL, ConnStr);
            strSQL = string.Format("INSERT INTO PRECIO_X_PROD01(CVE_ART, CVE_PRECIO, PRECIO) VALUES ('{0}', 2, 1)", CVE_ART);
            ExecuteSQL(strSQL, ConnStr);
            strSQL = string.Format("INSERT INTO PRECIO_X_PROD01(CVE_ART, CVE_PRECIO, PRECIO) VALUES ('{0}', 3, 1)", CVE_ART);
            ExecuteSQL(strSQL, ConnStr);
            strSQL = string.Format("INSERT INTO PRECIO_X_PROD01(CVE_ART, CVE_PRECIO, PRECIO) VALUES ('{0}', 4, 1)", CVE_ART);
            ExecuteSQL(strSQL, ConnStr);
            strSQL = string.Format("INSERT INTO PRECIO_X_PROD01(CVE_ART, CVE_PRECIO, PRECIO) VALUES ('{0}', 5, 1)", CVE_ART);
            ExecuteSQL(strSQL, ConnStr);
            strSQL = string.Format("INSERT INTO PRECIO_X_PROD01(CVE_ART, CVE_PRECIO, PRECIO) VALUES ('{0}', 6, 1)", CVE_ART);
            ExecuteSQL(strSQL, ConnStr);


        }
        private static void ActualizarPXP(string CVE_ART, double PRECIO, string ConnStr)
        {
            string strSQL = "";
            strSQL = string.Format("UPDATE PRECIO_X_PROD01 SET PRECIO={1} WHERE CVE_ART='{0}' AND CVE_PRECIO=1", CVE_ART, PRECIO);
            ExecuteSQL(strSQL, ConnStr);
        }
        private static void ActualizarINVE(string CVE_ART, string DESCR, string LIN_PROD, string STATUS, string ConnStr, double PESO = 0)
        {
            string sql = string.Format("UPDATE INVE01 SET DESCR='{1}', LIN_PROD='{2}', STATUS='{3}', CUENT_CONT = '1134-0001-000', CVE_PRODSERV = '53102700', CVE_UNIDAD = 'H87', PESO = {4}   WHERE CVE_ART='{0}'", CVE_ART, DESCR, LIN_PROD, STATUS, PESO);
            ExecuteSQL(sql, ConnStr);
        }
        private static void InsertarINVE(string CVE_ART, string DESCR, string LIN_PROD, string STATUS, string ConnStr, double PESO = 0)
        {

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("INSERT INTO INVE01(CVE_ART, DESCR, LIN_PROD, CON_SERIE, UNI_MED, UNI_EMP, TIEM_SURT, STOCK_MIN, STOCK_MAX, ");
            strSQL.Append("TIP_COSTEO, NUM_MON, COMP_X_REC, PEND_SURT, EXIST, COSTO_PROM, ULT_COSTO, CVE_OBS, TIPO_ELE, UNI_ALT, FAC_CONV, APART, ");
            strSQL.Append("CON_LOTE, CON_PEDIMENTO, PESO, VOLUMEN, CVE_ESQIMPU, VTAS_ANL_C, VTAS_ANL_M, COMP_ANL_C, COMP_ANL_M, BLK_CST_EXT, STATUS, CUENT_CONT,CVE_PRODSERV, CVE_UNIDAD) ");
            strSQL.Append("VALUES('{0}', '{1}', '{2}', 'N', 'PIEZA', 1, 0, 0, 0, ");
            strSQL.Append("'U', 1, 0, 0, 0, 0, 0, 0, 'P', 'PIEZA', 1, 0, ");
            strSQL.Append("'N', 'N', {4}, 0, 9, 0, 0, 0, 0, 'N', '{3}','1134-0001-000','53102700','H87')");

            string sql = string.Format(strSQL.ToString(), new object[] { CVE_ART, DESCR, LIN_PROD, STATUS, PESO });

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DALUtil.GetConnection(ConnStr);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            /*
            ' SUTL: 29 Ago 2014
            ' Debido a inconsitencias, primero se borrará en INVE_CLIB antes de insertar, para evitar un posible error    
            */

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = DALUtil.GetConnection(ConnStr);
            cmd2.CommandText = string.Format("DELETE INVE_CLIB01 WHERE CVE_PROD = '{0}'", CVE_ART);
            cmd2.ExecuteNonQuery();
            cmd2.Connection.Close();

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = DALUtil.GetConnection(ConnStr);
            cmd3.CommandText = string.Format("INSERT INTO INVE_CLIB01 (CVE_PROD) VALUES('{0}')", CVE_ART);
            cmd3.ExecuteNonQuery();
            cmd3.Connection.Close();

        }
        private static object RegresaEscalar(string Campo, string Tabla, string ConnStr)
        {
            SqlServerSelectCommand select = new SqlServerSelectCommand();
            select.Connection = DALUtil.GetConnection(ConnStr);
            select.ObjectName = string.Format("SELECT {0} FROM {1}", Campo, Tabla);

            object result = select.GetScalar();

            select.Connection.Close();
            return result;
        }
        private static bool ExisteClave(string Clave, string CampoClave, string Tabla, string Condicion, string ConnStr)
        {
            SqlServerSelectCommand select = new SqlServerSelectCommand();
            select.Connection = DALUtil.GetConnection(ConnStr);
            select.ObjectName = string.Format("SELECT COUNT(*) FROM {0} WHERE {1} = @clave" + (Condicion == string.Empty ? "" : string.Format(" AND " + Condicion)), Tabla, CampoClave);
            select.Parameters.Add(new SqlParameter("@clave", Clave));


            int result = (int)select.GetScalar();

            select.Connection.Close();
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;

            }



        }
        public static void Guardar2(string Modelo, string LineaProduccion, double Precio, double CostoPrenda, Int16? AlmacenParaPT, List<int> LstAlmacenes, DataTable dtTallas, DataTable dtEstr, ref Exception exception, String Observaciones = "", double Peso = 0, String ObservacionesEtiqueta = "", String Agrupacion = "")
        {
            try
            {
                Stopwatch stopW = Stopwatch.StartNew();
                Log("Inicio del proceso...");

                #region Cadenas de conexión

                string connStrSae80 = Globales.ConnectionStringSae80;

                #endregion



                Int16? almacenRc = 0;
                short? ptMovinv = 3; //CONCEPTO PARA PT:          3 Entrada de Fáb.
                short? tipoCto = 3; //TIPO DE COSTEO             REAL GRUPO 3
                double? loteSug = 1; //LOTE SUGERIDO:             1
                short? perProd = 0; //PERIODO DE PRODUCCIÓN:     Semanal
                short costpMp = 1; //COSTO ESTIMADO:            Último Costo

                int cantidadAgrupada = 0;
                int.TryParse(Agrupacion, out cantidadAgrupada);
                InsertarAgrupacion(Modelo, cantidadAgrupada);

                Log("Segmento CICLADO DE TALLA...");
                foreach (DataRow row in dtTallas.Rows)
                {

                    string talla = (string)row["TALLA"];
                    string descr = (string)row["DESCR"];
                    string status = (string)row["ESTATUS"];
                    double existencia = (double)row["EXIST"];
                    string accion = (string)row["Accion"];


                    descr = descr.Replace("'", "''");

                    string cve_art = Modelo + talla;
                    Log("Segmento Existencia...");
                    #region Existencia

                    if (existencia == 0)
                    {
                        if (status.ToUpper() == "ACTIVO")
                        {
                            status = "A";
                        }
                        else
                        {
                            status = "B";
                        }
                    }
                    else
                    {
                        status = "A";
                    }

                    #endregion
                    Log("Segmento 1.0...");
                    #region 1.0 CATÁLOGO DE PRODUCTOS

                    if (!ExisteClave(cve_art, "CVE_ART", "INVE01", "", connStrSae80))
                    {
                        InsertarINVE(cve_art, descr, LineaProduccion, status, connStrSae80, Peso);
                    }
                    else
                    {
                        ActualizarINVE(cve_art, descr, LineaProduccion, status, connStrSae80, Peso);
                    }

                    if (Observaciones != "")
                        InsertarINVEObservaciones(cve_art, Observaciones);

                    InsertarINVEObservacionesEtiqueta(cve_art, ObservacionesEtiqueta);

                    #endregion
                    Log("Segmento 2.0...");
                    #region 2.0 LISTA DE PRECIOS

                    if (!ExisteClave(cve_art, "CVE_ART", "PRECIO_X_PROD01", "CVE_PRECIO=1", connStrSae80))
                    {
                        InsertarPXP(cve_art, Precio, connStrSae80);
                    }
                    else
                    {
                        ActualizarPXP(cve_art, Precio, connStrSae80);
                    }

                    #endregion
                    Log("Segmento 3.0...");
                    #region 3.0 DISTRIBUCION DE ALMACENES

                    foreach (int cveAlmacen in LstAlmacenes)
                    {
                        //using (var dbContext = new AspelSae50Context())
                        //{
                        //MULT01 mult01Object = dbContext.MULT01.Find(cve_art, cveAlmacen);
                        bool existeMult01Object = ExisteClave(cve_art, "CVE_ART", "MULT01", string.Format("CVE_ALM={0}", cveAlmacen), connStrSae80);
                        if (!existeMult01Object)
                        {

                            InsertarMULT01(cve_art, cveAlmacen, connStrSae80);
                            //mult01Object = new ulp_dl.aspel_sae50.MULT01();
                            //mult01Object.CVE_ART = cve_art;
                            //mult01Object.CVE_ALM = cveAlmacen;
                            //mult01Object.STATUS = "A";
                            //mult01Object.EXIST = 0;
                            //mult01Object.STOCK_MIN = 0;
                            //mult01Object.STOCK_MAX = 0;
                            //mult01Object.COMP_X_REC = 0;
                            //dbContext.MULT01.Add(mult01Object);
                            //dbContext.SaveChanges();
                        }
                        //}
                    }



                    #endregion
                    Log("Segmento 4.0...");
                    #region 4.0 ELIMINAR LA ESTRUCTURA DEL PT

                    EliminarPT(cve_art, connStrSae80);

                    #endregion

                    if (status == "A")
                    {
                        /*
                     * ' 5.0 GUARDAR LA NUEVA ESTRUCTURA
                    ' 5.1 CABEZA PT
                    'PRO_TERM01.NUM_REG
                     */
                        Log("Segmento 5.0...");
                        #region  5.0 GUARDAR LA NUEVA ESTRUCTURA
                        /*
                        int sigReg = (int) RegresaEscalar("NUM_REGS", "PRO_TE0M01", connStrProd30);
                        sigReg += 1;
                        ExecuteSQL(string.Format("UPDATE PRO_TE0M01 SET NUM_REGS={0}", sigReg), connStrProd30);*/

                        //'INSERTAR CABEZA PT
                        StringBuilder strSQL = new StringBuilder();
                        strSQL.Append(
                            "INSERT INTO PROD_PRODTERM01 (CVE_ART, PT_ALMACEN, ALMACENRC, CVE_CPTO, MOVRC, COSTOE, ");
                        strSQL.Append(
                            "TIPOCOSTEO, TIEMPOFAB, IMAGEN, LOTESUG, NUM_PART, PER_PROD, NUMDIASPER, OCUPADO, COSTOMP, CVE_OBS, STATUS_REGISTRO,CVE_ESQ_INDI,CVE_PLANTA) ");
                        strSQL.Append(string.Format("VALUES ('{0}', {1}, {2}, {3}, '', {4},",
                            new object[] { cve_art, AlmacenParaPT, almacenRc, ptMovinv, CostoPrenda }));
                        strSQL.Append(string.Format("{0}, 0, '', {1}, {2}, {3}, 0, '', {4}, 0, 'A', 3, 1)", tipoCto, loteSug,
                            dtEstr.Rows.Count.ToString(), perProd, costpMp));

                        ExecuteSQL(strSQL.ToString(), connStrSae80);

                        #endregion
                        Log("Segmento 5.2...");
                        #region 5.2 DETALLE PT

                        int j = 0;
                        foreach (DataRow dataRow in dtEstr.Rows)
                        {
                            j++;
                            int tipoComp = 0;
                            string tipoG = "";

                            string proceso = (string)dataRow["PROCESO"];
                            double cantidad = (double)dataRow["CANTIDAD"];
                            string componente = (string)dataRow["COMPONENTE"];
                            string tipo = (string)dataRow["TIPO"];
                            double costoU = (double)dataRow["COSTOU"];
                            int almacen = int.Parse(dataRow["ALMACEN"].ToString().Trim());
                            var clave = dataRow["CLAVE"];

                            switch (tipo.ToUpper())
                            {
                                case "PT":
                                    tipoComp = 0;
                                    tipoG = "0";
                                    break;
                                case "MP":
                                    tipoComp = 1;
                                    tipoG = "0";
                                    break;
                                case "GD":
                                    tipoComp = 2;
                                    tipoG = "0";
                                    break;
                                case "GI":
                                    tipoComp = 2;
                                    tipoG = "1";
                                    break;
                                case "INS":
                                    tipoComp = 2;
                                    tipoG = "0";
                                    break;
                            }
                            Log("Segmento PT_DET01.NUMREG...");
                            #region PT_DET01.NUM_REG

                            /*
                            int sigNumReg = (int) RegresaEscalar("NUM_REGS", "PT_D0T01", connStrProd30);
                            sigNumReg += 1;
                            string strSQL2 = string.Format("UPDATE PT_D0T01 SET NUM_REGS={0}", sigNumReg);
                            ExecuteSQL(strSQL2, connStrProd30);
                            */
                            #endregion
                            Log("Segmento DETALLE_PT...");
                            #region INSERTAR DETALLE PT

                            StringBuilder sbSql = new StringBuilder();
                            sbSql.Append(
                                "INSERT INTO PROD_PRODTERM_DET01 (CVE_ART, NUMCOMPO, PROCESO, COMPONENTE, ALMACEN, TIPOCOMP, CANTIDAD, ");
                            sbSql.Append("COSTOU, TIPOG, TIEMPOPROC, SECUENCIA, CVE_OBS,TIPOCOMPONENTE,TOTALCOSTOUNITARIO,CVE_PLANTA) ");
                            sbSql.Append(string.Format("VALUES('{0}', {1}, '{2}', '{3}', {4}, {5}, {6}, ",
                                cve_art, j, proceso, componente, almacen, tipoComp, cantidad));
                            sbSql.Append(string.Format("{0}, '{1}', 0, {2}, 0, '{3}', {4} , 1)", costoU, tipoG, j, tipo.ToUpper(), (cantidad * costoU)));

                            ExecuteSQL(sbSql.ToString(), connStrSae80);

                            #endregion


                        }

                        #endregion
                    }

                }
                Log("Segmento   FIN DEL PROCESO...");
                stopW.Stop();
                Log(stopW.Elapsed.ToString());
            }
            catch (Exception Ex)
            {
                exception = Ex;
            }

        }
        public static bool Guardar(string Modelo, string LineaProduccion, double Precio, double CostoPrenda, Int16? AlmacenParaPT, List<int> LstAlmacenes, DataTable dtTallas, DataTable dtEstr, ref Exception exception)
        {




            var dbContext = new AspelSae80Context();
            var dbContextProd30 = new AspelSae80Context();

            DbContextTransaction sae50Transaction = dbContext.Database.BeginTransaction();
            DbContextTransaction prod30Transaction = dbContextProd30.Database.BeginTransaction();






            Int16? almacenRc = 0;
            short? ptMovinv = 3;    //CONCEPTO PARA PT:          3 Entrada de Fáb.
            short? tipoCto = 3;     //TIPO DE COSTEO             REAL GRUPO 3
            double? loteSug = 1;    //LOTE SUGERIDO:             1
            short? perProd = 0;     //PERIODO DE PRODUCCIÓN:     Semanal
            short costpMp = 1;      //COSTO ESTIMADO:            Último Costo



            foreach (DataRow row in dtTallas.Rows)
            {



                string talla = (string)row["TALLA"];
                string descr = (string)row["DESCR"];
                string status = (string)row["ESTATUS"];
                double existencia = (double)row["EXIST"];
                string accion = (string)row["Accion"];

                string cve_art = Modelo + talla;


                #region Existencia
                if (existencia == 0)
                {
                    if (status.ToUpper() == "ACTIVO")
                    {
                        status = "A";
                    }
                    else
                    {
                        status = "B";
                    }
                }
                else
                {
                    status = "A";
                }
                #endregion
                #region 1.0 CATÁLOGO DE PRODUCTOS

                var query = from inve01 in dbContext.INVE01 where inve01.CVE_ART == cve_art select inve01;

                if (!query.Any())
                {
                    #region Inserción de nuevo registro

                    INVE01 inve01Object = new INVE01();
                    inve01Object.CVE_ART = cve_art;
                    inve01Object.DESCR = descr;
                    inve01Object.LIN_PROD = LineaProduccion;
                    inve01Object.CON_SERIE = "N";
                    inve01Object.UNI_MED = "PIEZA";
                    inve01Object.UNI_EMP = 1;
                    inve01Object.TIEM_SURT = 0;
                    inve01Object.STOCK_MIN = 0;
                    inve01Object.STOCK_MAX = 0;
                    inve01Object.TIP_COSTEO = "U";
                    inve01Object.NUM_MON = 1;
                    inve01Object.COMP_X_REC = 0;
                    inve01Object.PEND_SURT = 0;
                    inve01Object.EXIST = 0;
                    inve01Object.COSTO_PROM = 0;
                    inve01Object.ULT_COSTO = 0;
                    inve01Object.CVE_OBS = 0;
                    inve01Object.TIPO_ELE = "P";
                    inve01Object.UNI_ALT = "PIEZA";
                    inve01Object.FAC_CONV = 1;
                    inve01Object.APART = 0;
                    inve01Object.CON_LOTE = "N";
                    inve01Object.CON_PEDIMENTO = "N";
                    inve01Object.PESO = 0;
                    inve01Object.VOLUMEN = 0;
                    inve01Object.CVE_ESQIMPU = 9;
                    inve01Object.VTAS_ANL_C = 0;
                    inve01Object.VTAS_ANL_M = 0;
                    inve01Object.COMP_ANL_C = 0;
                    inve01Object.COMP_ANL_M = 0;
                    inve01Object.BLK_CST_EXT = "N";
                    inve01Object.STATUS = status;

                    dbContext.INVE01.Add(inve01Object);


                    /*
                     * Comentario desde el Duplicador d VB6
                        ' SUTL: 29 Ago 2014
                        ' Debido a inconsitencias, primero se borrará en INVE_CLIB antes de insertar, para evitar un posible error
                     * */

                    var objectToDelete = dbContext.INVE_CLIB01.Find(cve_art);

                    dbContext.INVE_CLIB01.Remove(objectToDelete);

                    INVE_CLIB01 inve_Clib01ToAddObject = new INVE_CLIB01();

                    inve_Clib01ToAddObject.CVE_PROD = cve_art;

                    dbContext.INVE_CLIB01.Add(inve_Clib01ToAddObject);


                    #endregion
                }
                else
                {
                    #region Actualización del existente

                    INVE01 inve01ObjctToUpdate = dbContext.INVE01.Find(cve_art);

                    inve01ObjctToUpdate.DESCR = descr;
                    inve01ObjctToUpdate.LIN_PROD = LineaProduccion;
                    inve01ObjctToUpdate.STATUS = status;



                    #endregion
                }


                #endregion
                #region 2.0 LISTA DE PRECIOS
                var queryLstPrecios = from lstPrecios in dbContext.PRECIO_X_PROD01
                                      where lstPrecios.CVE_ART == cve_art && lstPrecios.CVE_PRECIO == 1
                                      select lstPrecios;

                if (!queryLstPrecios.Any())
                {
                    #region inserta Precio x Producto

                    PRECIO_X_PROD01 pxp1 = new PRECIO_X_PROD01();
                    PRECIO_X_PROD01 pxp2 = new PRECIO_X_PROD01();
                    PRECIO_X_PROD01 pxp3 = new PRECIO_X_PROD01();
                    PRECIO_X_PROD01 pxp4 = new PRECIO_X_PROD01();
                    PRECIO_X_PROD01 pxp5 = new PRECIO_X_PROD01();
                    PRECIO_X_PROD01 pxp6 = new PRECIO_X_PROD01();


                    pxp1.CVE_ART = cve_art;
                    pxp1.CVE_PRECIO = 1;
                    pxp1.PRECIO = Precio;

                    pxp2.CVE_ART = cve_art;
                    pxp2.CVE_PRECIO = 2;
                    pxp2.PRECIO = 1;

                    pxp3.CVE_ART = cve_art;
                    pxp3.CVE_PRECIO = 3;
                    pxp3.PRECIO = 1;

                    pxp4.CVE_ART = cve_art;
                    pxp4.CVE_PRECIO = 4;
                    pxp4.PRECIO = 1;

                    pxp5.CVE_ART = cve_art;
                    pxp5.CVE_PRECIO = 5;
                    pxp5.PRECIO = 1;

                    pxp6.CVE_ART = cve_art;
                    pxp6.CVE_PRECIO = 6;
                    pxp6.PRECIO = 1;

                    dbContext.PRECIO_X_PROD01.Add(pxp1);
                    dbContext.PRECIO_X_PROD01.Add(pxp2);
                    dbContext.PRECIO_X_PROD01.Add(pxp3);
                    dbContext.PRECIO_X_PROD01.Add(pxp4);
                    dbContext.PRECIO_X_PROD01.Add(pxp5);
                    dbContext.PRECIO_X_PROD01.Add(pxp6);

                    #endregion
                }
                else
                {
                    #region Actualiza Precio x Producto

                    PRECIO_X_PROD01 pxp = dbContext.PRECIO_X_PROD01.Find(cve_art, 1);

                    pxp.PRECIO = Precio;


                    #endregion
                }
                #endregion
                #region 3.0 DISTRIBUCION DE ALMACENES

                foreach (int cveAlmacen in LstAlmacenes)
                {
                    MULT01 mult01Object = dbContext.MULT01.Find(cve_art, cveAlmacen);


                    if (mult01Object == null)
                    {
                        mult01Object.CVE_ART = cve_art;
                        mult01Object.CVE_ALM = cveAlmacen;
                        mult01Object.STATUS = "A";
                        mult01Object.EXIST = 0;
                        mult01Object.STOCK_MIN = 0;
                        mult01Object.STOCK_MAX = 0;
                        mult01Object.COMP_X_REC = 0;
                        dbContext.MULT01.Add(mult01Object);
                    }
                }



                #endregion
                #region 4.0 ELIMINAR LA ESTRUCTURA DEL PT



                PROD_PRODTERM01 proTerm01Object = dbContextProd30.PROD_PRODTERM01.Find(cve_art);
                dbContextProd30.PROD_PRODTERM01.Remove(proTerm01Object);
                dbContextProd30.SaveChanges();

                var ptDet01Query = from ptDet01 in dbContextProd30.PROD_PRODTERM_DET01 where ptDet01.CVE_ART == cve_art select ptDet01;


                foreach (ulp_dl.aspel_sae80.PROD_PRODTERM_DET01 ptDet01 in ptDet01Query)
                {
                    dbContextProd30.PROD_PRODTERM_DET01.Remove(ptDet01);
                    //dbContextProd.SaveChanges();
                }
                dbContextProd30.SaveChanges();

                #endregion


                dbContext.SaveChanges();













                if (status == "A")
                {
                    /*
                     *  5.0 GUARDAR LA NUEVA ESTRUCTURA
                        5.1 CABEZA PT
                        PRO_TERM01.NUM_REG
                     * */
                    /* PROD40 NO APLICA NUM_REG
                    ulp_dl.aspel_sae80.PROD_PRODTERM01 proTe0m01Object = (from proTe0m01 in dbContextProd30.PROD_PRODTERM01 select proTe0m01).SingleOrDefault();

                    //int nextReg = proTe0m01Object.NUM_REGS + 1;

                    proTe0m01Object.NUM_REGS = nextReg;
                    dbContextProd30.SaveChanges();
                     * */

                    #region INSERTA CABEZA PT
                    PROD_PRODTERM01 proTerm01ToAddObject = new PROD_PRODTERM01();
                    //PROD_PRODTERM01 proTerm01ToAddObject = new PROD_PRODTERM01();
                    //proTerm01ToAddObject.NUM_REG = nextReg;
                    proTerm01ToAddObject.CVE_ART = cve_art;
                    proTerm01ToAddObject.PT_ALMACEN = AlmacenParaPT;
                    proTerm01ToAddObject.ALMACENRC = almacenRc; //declarada al inicio valor x defecto: 0
                    proTerm01ToAddObject.CVE_CPTO = ptMovinv;  //declarada al inicio valor x defecto: 3
                    proTerm01ToAddObject.MOVRC = "";
                    proTerm01ToAddObject.COSTOE = CostoPrenda;
                    proTerm01ToAddObject.TIPOCOSTEO = tipoCto;     //declarada al inicio valor x defecto: 3
                    proTerm01ToAddObject.TIEMPOFAB = 0;
                    proTerm01ToAddObject.IMAGEN = "";
                    proTerm01ToAddObject.LOTESUG = loteSug;     //declarada al inicio valor x defecto: 3
                    proTerm01ToAddObject.NUM_PART = (short)(dtEstr.Rows.Count);
                    proTerm01ToAddObject.PER_PROD = perProd;    //declarada al inicio valor x defecto: 0
                    proTerm01ToAddObject.NUMDIASPER = 0;
                    proTerm01ToAddObject.OCUPADO = "";
                    proTerm01ToAddObject.COSTOMP = costpMp;     //declarada al inicio valor x defecto: 1
                    proTerm01ToAddObject.CVE_OBS = 0;
                    //proTerm01ToAddObject.RESTO = "";
                    dbContextProd30.PROD_PRODTERM01.Add(proTerm01ToAddObject);
                    dbContextProd30.SaveChanges();
                    #endregion
                    #region 5.2 INSERTA DETALLE PT

                    int j = 0;
                    foreach (DataRow dataRow in dtEstr.Rows)
                    {
                        j++;
                        short? tipoComp = 0;
                        string tipoG = "";

                        string proceso = (string)dataRow["PROCESO"];
                        double cantidad = (double)dataRow["CANTIDAD"];
                        string componente = (string)dataRow["COMPONENTE"];
                        string tipo = (string)dataRow["TIPO"];
                        double costoU = (double)dataRow["COSTOU"];
                        short? almacen = (short?)dataRow["ALMACEN"];
                        var clave = dataRow["CLAVE"];

                        switch (tipo.ToUpper())
                        {
                            case "PT":
                                //tipoComp = 48;
                                tipoComp = 0;
                                tipoG = "D";
                                break;
                            case "MP":
                                //tipoComp = 49;
                                tipoComp = 1;
                                tipoG = "D";
                                break;
                            case "GD":
                                //tipoComp = 50;
                                tipoComp = 2;
                                tipoG = "D";
                                break;
                            case "GI":
                                //tipoComp = 50;
                                tipoComp = 2;
                                tipoG = "I";
                                break;
                        }

                        //PT_DET01.NUM_REG
                        //ulp_dl.aspel_prod30.PT_D0T01 ptD0t01Object = (from ptD0t01 in dbContextProd30.PT_D0T01 select ptD0t01).SingleOrDefault();

                        //int nextRegPt = ptD0t01Object.NUM_REGS + 1;
                        int nextRegPt = 0;

                        //ptD0t01Object.NUM_REGS = nextRegPt;
                        //dbContextProd30.SaveChanges();

                        ulp_dl.aspel_sae80.PROD_PRODTERM_DET01 ptDet01ToAddObject = new ulp_dl.aspel_sae80.PROD_PRODTERM_DET01();

                        //ptDet01ToAddObject.NUM_REG = nextRegPt;
                        ptDet01ToAddObject.CVE_ART = cve_art;
                        ptDet01ToAddObject.PROCESO = proceso;
                        ptDet01ToAddObject.COMPONENTE = componente;
                        ptDet01ToAddObject.ALMACEN = almacen;
                        ptDet01ToAddObject.TIPOCOMP = tipoComp;
                        ptDet01ToAddObject.CANTIDAD = cantidad;
                        ptDet01ToAddObject.COSTOU = costoU;
                        ptDet01ToAddObject.TIPOG = tipoG;
                        ptDet01ToAddObject.TIEMPOPROC = 0;
                        ptDet01ToAddObject.SECUENCIA = j;
                        ptDet01ToAddObject.NUMCOMPO = j;
                        ptDet01ToAddObject.CVE_OBS = 0;
                        //ptDet01ToAddObject.RESTO = "";
                        ptDet01ToAddObject.CVE_PLANTA = 1;
                        ptDet01ToAddObject.TIPOCOMPONENTE = tipo;
                        ptDet01ToAddObject.TOTALCOSTOUNITARIO = cantidad * costoU;


                        dbContextProd30.PROD_PRODTERM_DET01.Add(ptDet01ToAddObject);
                        dbContextProd30.SaveChanges();
                    }


                    #endregion






                }
            }

            //dbContext.SaveChanges();

            sae50Transaction.Commit();
            prod30Transaction.Commit();

            return false;
        }

        private static void InsertarINVEObservaciones(String CVE_ART, String OBSERVACIONES)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_AltaObservacionesINVE01";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_ARTICULO", CVE_ART));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OBSERVACIONES", OBSERVACIONES));
                guarda.Execute();
            }

        }
        private static void InsertarINVEObservacionesEtiqueta(String CVE_ART, String OBSERVACIONES)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var articulo = dbContext.INVE_CLIB01.Where(x => x.CVE_PROD == CVE_ART).FirstOrDefault();
                if (articulo != null)
                {
                    articulo.CAMPLIB7 = OBSERVACIONES;
                }
                else
                {
                    INVE_CLIB01 oCLIB = new INVE_CLIB01();
                    oCLIB.CVE_PROD = CVE_ART;
                    oCLIB.CAMPLIB7 = OBSERVACIONES;
                    dbContext.INVE_CLIB01.Add(oCLIB);
                }
                dbContext.SaveChanges();

            }

        }
        private static void InsertarAgrupacion(String Modelo, int CantidadAgrupada)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_AltaCodigoBarraModelosEspeciales";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Modelo", Modelo));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CantidadAgrupada", CantidadAgrupada));
                guarda.Execute();
            }
        }
    }
}
