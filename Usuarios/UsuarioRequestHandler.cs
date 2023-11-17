using MongoDB.Driver;
public static class UsuarioRequestHandler{
    public static IResult Registrar(Registro datos){
        if (string.IsNullOrWhiteSpace(datos.Nombre)){
            return Results.BadRequest("El nombre es requerido");
        }
         if (string.IsNullOrWhiteSpace(datos.Correo)){
            return Results.BadRequest("El correo es requerido");
        }
         if (string.IsNullOrWhiteSpace(datos.Password)){
            return Results.BadRequest("La contraseña es requerida");
        }
        BaseDatos bd=new BaseDatos ();
        var coleccion=bd.ObtenerColeccion<Registro>("Usuarios");

        if (coleccion==null){
            throw new Exception ("No existe la colección usuario");
        }
        FilterDefinitionBuilder <Registro> filterBuilder=new FilterDefinitionBuilder<Registro>();
        var filter=filterBuilder.Eq(x=>x.Correo, datos.Correo);
        Registro? usuarioExistente=coleccion.Find(filter).FirstOrDefault();
        if (usuarioExistente !=null){
            return Results.BadRequest($"Ya existe un usuario con el correo{datos.Correo}");
        }
        coleccion.InsertOne(datos);
        return Results.Ok();
            }


    public static IResult Recuperar (recuperar datos){

         if (string.IsNullOrWhiteSpace(datos.Correo)){
            return Results.BadRequest("El correo es requerido");
        }

        BaseDatos bd=new BaseDatos ();
        var coleccion=bd.ObtenerColeccion<Registro>("Usuarios");

        if (coleccion==null){
            throw new Exception ("No existe la colección usuario");
        }

        FilterDefinitionBuilder <Registro> filterBuilder=new FilterDefinitionBuilder<Registro>();
        var filter=filterBuilder.Eq(x=>x.Correo, datos.Correo);

        Registro? usuarioExistente=coleccion.Find(filter).FirstOrDefault();

        if (usuarioExistente == null){
            return Results.BadRequest($"No existe un usuario con el correo {datos.Correo}");
        }else if(usuarioExistente.Correo==datos.Correo){
            Correos c = new Correos();
            c.Destinatario = usuarioExistente.Correo;
            c.Asunto = "Recuperar contraseña SENA";
            c.Mensaje = $"Tu contraseña es - {usuarioExistente.Password} -";
            c.Enviar();
        }
        return Results.Ok();
            }


            public static IResult Acceso(Acceso datos){
         if (string.IsNullOrWhiteSpace(datos.Correo)){
            return Results.BadRequest("El correo es requerido");
        }
         if (string.IsNullOrWhiteSpace(datos.Password)){
            return Results.BadRequest("La contraseña es requerida");
        }
        BaseDatos bd=new BaseDatos ();
        var coleccion=bd.ObtenerColeccion<Registro>("Usuarios");

        if (coleccion==null){
            throw new Exception ("No existe la colección usuario");
        }
        FilterDefinitionBuilder <Registro> filterBuilder=new FilterDefinitionBuilder<Registro>();
        var filter=filterBuilder.Eq(x=>x.Correo,datos.Correo);
        Registro? usuarioExistente=coleccion.Find(filter).FirstOrDefault();
        if (usuarioExistente ==null){
            return Results.BadRequest($"No existe un usuario con el correo {datos.Correo}");
        }

        if (usuarioExistente.Password!=datos.Password){
            return Results.BadRequest("El usuario o contraseña son incorrectas.");
        }
        return Results.Ok();
            }
}
 