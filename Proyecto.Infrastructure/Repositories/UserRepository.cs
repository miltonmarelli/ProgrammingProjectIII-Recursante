
using System.Collections.Generic;
using System.Linq;
using Proyecto.Application.Models.Dtos;
using Proyecto.Domain.Repositories;
using Proyecto.Domain.Models;
using Proyecto.Infraestructure.Context;

namespace Proyecto.Infraestructure.Repositories
        {
        public class UserRepository : IUserRepository
        {
            private readonly ProyectoDbContext _context;

            public UserRepository(ProyectoDbContext context)
            {
                _context = context;
            }

            public ICollection<User> GetAllUsers()
            {
                try
                {
                    return _context.Users.ToList();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al obtener todos los usuarios", ex);
                }
            }

            public ICollection<Client> GetAllClients()
            {
                try
                {
                    return _context.Users.OfType<Client>().ToList();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al obtener todos los clientes", ex);
                }
            }

            public ICollection<Admin> GetAllAdmins()
            {
                try
                {
                    return _context.Users.OfType<Admin>().ToList();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al obtener todos los administradores", ex);
                }
            }

            public ICollection<Dev> GetAllDevs()
            {
                try
                {
                    return _context.Users.OfType<Dev>().ToList();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al obtener todos los desarrolladores", ex);
                }
            }

            public User GetByName(string name)
            {
                try
                {
                    var userEntity = _context.Users.FirstOrDefault(u => u.Name == name);
                    if (userEntity == null)
                    {
                        throw new ArgumentException("El usuario no existe");
                    }
                    return userEntity;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al obtener el usuario por nombre", ex);
                }
            }

        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario", ex);
            }
        }

        public void DeleteUser(int id)
            {
                var userToDelete = _context.Users.Find(id);
                if (userToDelete != null)
                {
                    userToDelete.Activo = false;  
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("El usuario no existe");
                }
            }


        public void UpdateUser(User user)
        {
            try
            {
                var existingUser = _context.Users.Find(user.Id);
                if (existingUser == null)
                {
                    throw new ArgumentException("El usuario no existe");
                }

                if (user is Client client && existingUser is Client)
                {
                    var shoppingCart = _context.ShoppingCarts
                        .FirstOrDefault(sc => sc.ClientId == client.Id);

                    if (shoppingCart == null)
                    {         
                        shoppingCart = new ShoppingCart { ClientId = client.Id };
                        _context.ShoppingCarts.Add(shoppingCart);
                    }
                }

                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar el usuario", ex);
            }
        }

        public User GetUserById(int id)
            {
                try
                {
                    var user = _context.Users.Find(id);
                    if (user == null)
                    {
                        throw new ArgumentException("El usuario no existe");
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al obtener el usuario por ID", ex);
                }
            }
        }
}

