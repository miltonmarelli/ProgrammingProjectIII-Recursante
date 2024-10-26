using Proyecto.Application.IServices;
using Proyecto.Application.Models.Request;
using Proyecto.Application.Models.Dtos;
using Proyecto.Domain.Repositories;
using Proyecto.Domain.Models;

namespace Proyecto.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommercialInvoiceService _commercialInvoiceService;

        public UserService(IUserRepository userRepository, ICommercialInvoiceService commercialInvoiceService)
        {
            _userRepository = userRepository;
            _commercialInvoiceService = commercialInvoiceService;
        }

        public ICollection<UserDto> GetAll()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                return users.Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email, Role = u.Role }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los usuarios", ex);
            }
        }

        public ICollection<UserDto> GetAllClients()
        {
            try
            {
                var clients = _userRepository.GetAllClients();
                return clients.Select(c => new UserDto { Id = c.Id, Name = c.Name, Email = c.Email, Role = c.Role }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los clientes", ex);
            }
        }

        public ICollection<UserDto> GetAllAdmins()
        {
            try
            {
                var admins = _userRepository.GetAllAdmins();
                return admins.Select(a => new UserDto { Id = a.Id, Name = a.Name, Email = a.Email, Role = a.Role }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los administradores", ex);
            }
        }

        public ICollection<UserDto> GetAllDevs()
        {
            try
            {
                var devs = _userRepository.GetAllDevs();
                return devs.Select(d => new UserDto { Id = d.Id, Name = d.Name, Email = d.Email, Role = d.Role }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los desarrolladores", ex);
            }
        }

        public User GetByName(string name)
        {
            try
            {
                return _userRepository.GetByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el usuario con nombre {name}", ex);
            }
        }

        public UserDto Create(UserSaveRequest user)
        {
            try
            {
                switch (user.Role.ToLower())
                {
                    case "admin":
                        var newAdmin = new Admin
                        {
                            Name = user.Name,
                            Email = user.Email,
                            Password = user.Password,
                            Activo = true
                        };
                        _userRepository.AddUser(newAdmin);
                        return new UserDto { Name = newAdmin.Name, Email = newAdmin.Email, Role = newAdmin.Role };

                    case "dev":
                        var newDev = new Dev
                        {
                            Name = user.Name,
                            Email = user.Email,
                            Password = user.Password,
                            Activo = true
                        };
                        _userRepository.AddUser(newDev);
                        return new UserDto { Name = newDev.Name, Email = newDev.Email, Role = newDev.Role };

                    case "client":
                        var newCliente = new Client
                        {
                            Name = user.Name,
                            Email = user.Email,
                            Password = user.Password,
                            Activo = true
                        };

                      
                        var newShoppingCart = new ShoppingCart
                        {
                            ClientId = newCliente.Id,
                            ClientName = newCliente.Name,
                            ClientEmail = newCliente.Email
                        };

                        newCliente.ShoppingCart = newShoppingCart;

                        _userRepository.AddUser(newCliente);
                        return new UserDto { Name = newCliente.Name, Email = newCliente.Email, Role = newCliente.Role };

                    default:
                        throw new ArgumentException("Error en Rol de User");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario", ex);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user != null)
                {
                    user.Activo = false;
                    _userRepository.UpdateUser(user);
                }
                else
                {
                    throw new Exception("El usuario no existe");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario", ex);
            }
        }

        public UserDto GetById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user != null)
                {
                    return new UserDto { Name = user.Name, Email = user.Email, Role = user.Role };
                }
                else
                {
                    throw new Exception("El usuario no existe");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el usuario con ID {id}", ex);
            }
        }

        public UserDto UpdateUser(int id, UserSaveRequest user)
        {
            try
            {
                var NotNullUser = _userRepository.GetUserById(id);
                if (NotNullUser == null)
                {
                    throw new ArgumentException("El usuario no existe");
                }

                NotNullUser.Name = user.Name;
                NotNullUser.Email = user.Email;

                _userRepository.UpdateUser(NotNullUser);
                return new UserDto { Name = NotNullUser.Name, Email = NotNullUser.Email, Role = NotNullUser.Role };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario con ID {id}", ex);
            }
        }
    }
}

