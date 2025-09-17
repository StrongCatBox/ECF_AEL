namespace ECF_AEL.Models;

public class Moniteur
{
    public int IdMoniteur { get; set; }
    public string NomMoniteur { get; set; }
    public string PrenomMoniteur { get; set; }
    public DateTime DateNaissance { get; set; }
    public DateTime DateEmbauche { get; set; }
    public bool Activite { get; set; }

}
