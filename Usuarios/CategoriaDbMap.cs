using MongoDB.Bson; 
public class CategoriaDbMap{
    public ObjectId Id { get; set; }
    public string? Nombre { get; set; } 
    public string UrlIcono { get; set; } = string.Empty;
}
