using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ClientesBLL
{
    private readonly Contexto _Contexto;

    public ClientesBLL(Contexto Contexto)
    {
        _Contexto =Contexto;
    }

    public bool Existe(int ClienteId){
         return _Contexto.Clientes.Any(o=> o.ClienteId==ClienteId);
    }

    private bool Insertar (Clientes cliente){
         _Contexto.Clientes.Add(cliente);
          bool save = _Contexto.SaveChanges() >0;
         _Contexto.Entry(cliente).State = EntityState.Detached;
         return save;
    }

    private bool Modificar (Clientes cliente){
         _Contexto.Entry(cliente).State = EntityState.Modified;
         bool save = _Contexto.SaveChanges() >0;
         _Contexto.Entry(cliente).State = EntityState.Detached;
         return save;
    }

    public bool Guardar( Clientes cliente){
         if(!Existe(cliente.ClienteId)){
             return this.Insertar(cliente);
         }
         else{
             return this.Modificar(cliente);
         }
    }

    public Clientes? Buscar(int ClienteId){
         return _Contexto.Clientes.AsNoTracking().FirstOrDefault(o => o.ClienteId== ClienteId);
            
    }

     public bool Eliminar (Clientes cliente){
         _Contexto.Entry(cliente).State = EntityState.Deleted;
         bool save = _Contexto.SaveChanges() >0;
         _Contexto.Entry(cliente).State = EntityState.Detached;
         return save;
    }
    public List<Clientes> GetList(Expression<Func<Clientes,bool>>criterio){
         return _Contexto.Clientes.AsNoTracking().Where(criterio).ToList();
    }
}   