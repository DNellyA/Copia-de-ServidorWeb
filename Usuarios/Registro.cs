using MongoDB.Bson;
public class Registro{
    public string? Correo{get; set;}
    public string? Nombre{get; set;}
    public string? Password{get; set;}
    public ObjectId Id {get; set;}
}
public class recuperar{
    public string? Correo{get; set;}
}
public class Acceso{
    public string? Correo{get; set;}
    public string? Password{get; set;}
    public ObjectId Id {get; set;}
}

