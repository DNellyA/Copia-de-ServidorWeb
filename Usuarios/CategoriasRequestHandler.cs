using MongoDB.Driver;
public static class CategoriasRequestHandler {
public static IResult Crear (CategoriaDTO datos){

    if(string.IsNullOrWhiteSpace(datos.UrlIcono)){
        return Results.BadRequest("Es necesario el codigo Url.");
    }

    if(string.IsNullOrWhiteSpace(datos.Nombre)){
        return Results.BadRequest("El nombre es requerido.");
    }

var filterBuilder = new FilterDefinitionBuilder<CategoriaDbMap>();
var filter = filterBuilder.Eq(x => x.Nombre, datos.Nombre);

BaseDatos bd = new BaseDatos ();
var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");

CategoriaDbMap? registro = coleccion.Find(filter).FirstOrDefault();

        if(registro != null){
            return Results.BadRequest($"La categoría '{datos.Nombre}' ya existe en la base de datos");
        }

        registro = new CategoriaDbMap();
        registro. Nombre = datos.Nombre;
        registro.UrlIcono = datos.UrlIcono;

        coleccion!.InsertOne (registro);
        string nuevoId = registro.Id.ToString();

        return Results.Ok (nuevoId);
    }
    public static IResult Listar(){
        var filterBuilder = new FilterDefinitionBuilder<CategoriaDbMap>();
        var filter = filterBuilder.Empty;

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
        List<CategoriaDbMap> mongoDbList = coleccion.Find(filter).ToList();

        var lista = mongoDbList.Select(x => new{
            Id = x.Id. ToString(),
            Nombre = x.Nombre,
            UrlIcono = x.UrlIcono
        }). ToList();
        return Results.Ok(lista); 
    }
}