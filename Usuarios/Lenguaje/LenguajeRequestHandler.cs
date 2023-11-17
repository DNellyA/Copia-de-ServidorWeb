using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;
public static class LenguajeRequestHandler { 
    public static IResult ListarRegistros (string idCategoria){
        //listado de todas las señas de una categoría
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Eq(x => x.IdCategoria, idCategoria);

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<LenguajeDbMap>("Lenguaje");
        var lista = coleccion.Find(filter).ToList();

        return Results.Ok(lista.Select(x => new{
            Id = x.Id.ToString(),
            IdCategoria = x.IdCategoria,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EsVideo = x.EsVideo,
            Url = x.Url

        }).ToList());

    }

    public static IResult CrearRegistro (LenguajeDTO datos){
        //Crea un nuevo registro de imagen o video para mostrar la seña de lo que se quiere comunicar
        if (!ObjectId.TryParse(datos.IdCategoria, out ObjectId idCategoria)){
        return Results.BadRequest($"El id de la categoría ({datos.IdCategoria}) no es válido");
        }
    BaseDatos bd = new BaseDatos();
    
    var filterBuiderCategorias = new FilterDefinitionBuilder<CategoriaDbMap>();
    var filterCategoria = filterBuiderCategorias.Eq(x => x.Id, idCategoria);
    var coleccionCategoria = bd.ObtenerColeccion<CategoriaDbMap>("Categorias");
    var categoria = coleccionCategoria.Find(filterCategoria).FirstOrDefault();

    if (categoria == null){
        return Results.NotFound($"No existe una categoria con ID='{datos.IdCategoria}'");
    }

    LenguajeDbMap registro = new LenguajeDbMap();
    registro.Titulo = datos.Titulo;
    registro.EsVideo = datos.EsVideo;
    registro.Descripcion = datos.Descripcion;
    registro.Url = datos.Url;
    registro.IdCategoria = datos.IdCategoria;

    if(string.IsNullOrWhiteSpace(datos.Titulo)){
        return Results.BadRequest("El nombre es requerido.");
    }

    if(string.IsNullOrWhiteSpace(datos.Descripcion)){
        return Results.BadRequest("La descripcion es requerida.");
    }

    if(string.IsNullOrWhiteSpace(datos.Url)){
        return Results.BadRequest("El Url es requerido.");
    }

    var coleccionLenguaje = bd.ObtenerColeccion<LenguajeDbMap>("Lenguaje");
    coleccionLenguaje!.InsertOne(registro);

    return Results.Ok(registro.Id.ToString());

}
    public static IResult Eliminar (string id){
        //Elimina un registro de la seña que se quiere quitar de la base de datos
        if(!ObjectId.TryParse(id, out ObjectId idLenguaje)){
            return Results.BadRequest($"El ID proporcionado ({id}) no es valido");
        }

        BaseDatos bd = new BaseDatos();
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Eq(x => x.Id, idLenguaje);
        var coleccion = bd.ObtenerColeccion<LenguajeDbMap>("Lenguaje");
        coleccion!.DeleteOne(filter);

        return Results.NoContent();

    }
    public static IResult Buscar (string texto){
        var queryExpr = new BsonRegularExpression(new Regex(texto, RegexOptions.IgnoreCase));
        var filterBuilder = new FilterDefinitionBuilder<LenguajeDbMap>();
        var filter = filterBuilder.Regex("Titulo", queryExpr) |
        filterBuilder.Regex("Descripcion", queryExpr);

        BaseDatos bd = new BaseDatos();
        var coleccion = bd.ObtenerColeccion<LenguajeDbMap>("Lenguaje");
        var lista = coleccion.Find(filter).ToList();

        return Results.Ok(lista.Select(x => new {
            Id = x.Id.ToString(),
            IdCategoria = x.IdCategoria,
            Titulo = x.Titulo,
            Descripcion = x.Descripcion,
            EsVideo = x.EsVideo,
            Url = x.Url
        }).ToList());
    }
}