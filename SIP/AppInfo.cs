using System;

public static class AppInfo
{
    public static String RutaApp;
    
	public static string VersionMayor
	{
		get
		{
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();    
		}
	}  
	public static string VersionMenor
	{
		get
		{
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
		}
	}
    public static string VersionCompilacion
	{
		get
		{
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
		}
	}
   
    public static string VersionCompleta
	{
		get
		{
            //return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
		    return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}
	}  
	
    public static string NombreApp
	{
		get
		{
            return System.IO.Path.GetFileNameWithoutExtension(RutaApp);
		}
	}

    public static bool ChecarActualizaciones 
    {
            get { return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["tmrCheckForUpdate"]); }
    }

    public static int IntervaloChequeoActualizaciones
    {
        get
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["tmrCheckForUpdateMilliSeconds"]);
        }
    }

    public static Uri UrlParaActualizaciones
    {
        get
        {
            return new Uri( System.Configuration.ConfigurationManager.AppSettings["tmrCheckForUpdateUrl"]);
        }
    }
}
