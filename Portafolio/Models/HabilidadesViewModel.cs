public class HabilidadesViewModel
{
    // BACK-END
    public string BackTitulo { get; set; }
    public string BackDescripcion { get; set; }
    public string BackStack { get; set; }

    // ESTE ES EL QUE DAPPER LLENA
    public string BackExperienciaString { get; set; }

    // ESTE ES SOLO PARA LA VISTA
    public List<string> BackExperiencia { get; set; }


    // FRONT-END
    public string FrontTitulo { get; set; }
    public string FrontDescripcion { get; set; }
    public string FrontStack { get; set; }

    public string FrontExperienciaString { get; set; }
    public List<string> FrontExperiencia { get; set; }
}
