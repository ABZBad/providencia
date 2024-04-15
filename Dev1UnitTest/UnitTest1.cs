using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SIP
{
    [TestClass]
    public class Accesso
    {
        [TestMethod]
        public void ValidarAcceso()
        {
            //bool tiene_acceso = ulp_bl.Permisos.Acceso.ValidarUsuario("", "");

            //Validando vacíos
            //Assert.AreEqual(tiene_acceso, false);

            //Validando usuario con dato y pwd vacío,
            // tiene_acceso = ulp_bl.Permisos.Acceso.ValidarUsuario("a", "");
            //Assert.AreEqual(tiene_acceso, false,"Usr != vacío, Pwd= vacío");



            //tiene_acceso = ulp_bl.Permisos.Acceso.ValidarUsuario("sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss", "");
            //Assert.AreEqual(tiene_acceso, false,"Desbordamiento");

            //tiene_acceso = ulp_bl.Permisos.Acceso.ValidarUsuario("SUP", "46b00837bacbb2beb1291ed6d7d52c15");
            //Assert.AreEqual(tiene_acceso, true,"Usuario válido");
        }
    }
}
