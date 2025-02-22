﻿using SyncBlackDuck.Model.Objetos;
using SyncBlackDuck.Services.Login;
using SyncBlackDuck.Views.AdminViews;
using SyncBlackDuck.Views.ClientViews;
using SyncBlackDuck.Views.SuperAdminViews;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace SyncBlackDuck.ViewModel
{
    public class LoginVM : ContentPage
    {
        private string telefono;
        private string password;
        private User loggedInUser;
        private string userId;
        private readonly LoginService loginController = new LoginService();
        public new INavigation Navigation;

        //Getters y setters para los bindings de la vista
        public string Telefono { get => telefono; set => telefono = value; }
        public string Password { get => password; set => password = value; }
        public LoginVM(INavigation navigation)
        {
            this.Navigation = navigation;
            AsyncCommand();
        }

        #region Commands

        //Binding del boton login en la vista
        public ICommand Login => LoginCommand();

        private Command LoginCommand()
        {
            return new Command(async () => await LoginAsync());
        }

        private Command AsyncCommand()
        {
            return new Command(async () => await AsyncSession());
        }

        //Metodo Async para guardar la informacion del usuario
        private Task AsyncSession()
        {
            try
            {
                //Revisamos si ya existe una session guardada
                if (Application.Current.Properties.ContainsKey("id"))
                {
                    var id = Application.Current.Properties["id"] as string;
                    //Guardamos la session
                    userId = id;
                    if (userId != null || !userId.Equals(0))
                    {
                        loggedInUser = loginController.LoginByPhone(int.Parse(userId));
                        new Command(async () => await LoginAsync());
                        /* Aqui si encuentra el usuario, deberia redireccionar al
                           main page de cada usuario por medio de un if, que revise
                           el tipo de rol y a partir de este, lo mande a su respectivo
                           main page */
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Error en AsyncSession");
                return Task.CompletedTask;
            }
        }
        //Metodo Async para iniciar sesion
        private Task LoginAsync()
        {
            try
            {
                if(loggedInUser == null)
                {
                    loggedInUser = loginController.LoginByRank(int.Parse(Telefono), Password);
                    Application.Current.Properties["id"] = loggedInUser.User_telefono.ToString();
                }

                switch (loggedInUser.User_rol)
                {
                    case "Admin":
                        //Redireccion admin
                         Navigation.PushAsync(new AdminMainPage());
                        break;
                    case "SuperAdmin":
                        //Redireccion superAdmin
                         Navigation.PushAsync(new SuperAdminMainPage());
                        break;
                    case null:
                        //Mostrar error de login
                        break;
                    default:
                        //Deberia ser cliente
                         Navigation.PushAsync(new ClienteMainPage());
                        break;

                }
                //Fin metodo Async
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                App.Current.MainPage.DisplayAlert("Credenciales Incorrectas", "Numero de telefono o contraseña incorrecta", "Ok");
                Console.WriteLine(e);
                Console.WriteLine("Error en LoginAsync");
                return Task.CompletedTask;

            }
        }

        #endregion Commands
    }

}
