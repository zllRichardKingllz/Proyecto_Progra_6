using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuario(int idUsuario, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(password);
            return repository.GetUsuario(idUsuario, crytpPasswd);
        }
        public Usuario GetUsuarioByIDSINPASSWORD(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioByIDSINPASSWORD(id);
        }
        public Usuario GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            oUsuario.clave = Cryptography.DecrypthAES(oUsuario.clave);
            return oUsuario;
        }
        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.clave = Cryptography.EncrypthAES(usuario.clave);
            return repository.Save(usuario);
        }
        public IEnumerable<Rol> GetRoles()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetRoles();
        }
        public IEnumerable<Usuario> listaUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.listaUsuarios();
        }
        public Usuario AsociarHR(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.clave = Cryptography.EncrypthAES(usuario.clave);
            return repository.AsociarHR(usuario);
        }
    }
}