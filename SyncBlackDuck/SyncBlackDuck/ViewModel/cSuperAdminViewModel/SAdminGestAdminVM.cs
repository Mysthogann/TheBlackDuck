﻿using SyncBlackDuck.Model.Objetos;
using SyncBlackDuck.Services.Implementaciones;
using SyncBlackDuck.Views.SuperAdminViews;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SyncBlackDuck.ViewModel.cSuperAdminViewModel
{
    public partial class SAdminGestAdminVM : SAdminBaseVM
    {
        private user usuarioSeleccionado = new user();
        private List<user> listaUsuarios = new List<user>();
        private userImpl userController = new userImpl();
        public SAdminGestAdminVM(INavigation navigation, SfDataGrid datagrid)
        {
            Navigation = navigation;
            usuariosInfo = new ObservableCollection<user>();
            selectedItem = new Object();
            CargarAdministradores();
            DatagridControlls grid = new DatagridControlls();
            datagrid.CurrentCellBeginEdit += grid.DataGrid_CurrentCellBeginEdit;
            datagrid.CurrentCellEndEdit += grid.DataGrid_CurrentCellEndEdit;
        }

        #region Listas

        private ObservableCollection<user> usuariosInfo;
        public ObservableCollection<user> usuariosInfoCollection
        {
            get { return usuariosInfo; }
            set
            {
                if (this.usuariosInfo != value)
                {
                    Console.WriteLine(value);
                    Console.WriteLine("se modifico el OC de userCollection");
                    this.usuariosInfo = value;
                }
            }
        }

        private Object selectedItem;
        public Object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                Console.WriteLine(value);
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    //Se podria salvar para aplicar cambios en la BD....
                    //Actualizar(value); //Donde value es el usuario (objeto) seleccionado.
                    //Despues de actualizar necesitamos recargar la tabla(?)
                }
            }
        }

        #endregion Listas

        #region DatagridControlls

        public class DatagridControlls
        {
            public DatagridControlls()
            {

            }

            public void DataGrid_CurrentCellBeginEdit(object sender, GridCurrentCellBeginEditEventArgs args)
            {
                Console.WriteLine("CurrentCellBeginEdit");
                Console.WriteLine("Row index: " + args.RowColumnIndex);
                Console.WriteLine("Column: " + args.Column);
            }

            public void DataGrid_CurrentCellEndEdit(object sender, GridCurrentCellEndEditEventArgs args)
            {
                Console.WriteLine("CurrentCellEndEdit");
                Console.WriteLine("Row index: " + args.RowColumnIndex);
                Console.WriteLine("Column: " + args.OldValue);
                Console.WriteLine("Column: " + args.NewValue);

            }
            public void EndEditCell(object sender, GridCurrentCellEndEditEventArgs args)
            {
                Console.WriteLine("CurrentCellEndEdit");
                Console.WriteLine("Row index: " + args.RowColumnIndex);
                Console.WriteLine("Column: " + args.OldValue);
                Console.WriteLine("Column: " + args.NewValue);

            }
        }

        #endregion DatagridControlls

        #region Commands
        public ICommand SBackAdminMain => BackSAdminMainP();

        private Command BackSAdminMainP()
        {
            return new Command(async () => await BackSAdminAsync());
        }

        private Task BackSAdminAsync()
        {
            try
            {
                Navigation.PopAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Error al cambiar de pagina");
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        private Command agregarUsuario;
        public ICommand AgregarUsuario => agregarUsuario ??= new Command(PerformAgregarUsuario);

        private void PerformAgregarUsuario()
        {
        }

        private Command modificarUsuario;
        public ICommand ModificarUsuario => modificarUsuario ??= new Command(PerformModificarUsuario);

        private void PerformModificarUsuario()
        {
        }

        private Command borrarUsuario;
        public ICommand BorrarUsuario => borrarUsuario ??= new Command(PerformBorrarUsuario);

        private void PerformBorrarUsuario()
        {
        }

        #endregion Commands

        private void CargarAdministradores()
        {
            try
            {
                listaUsuarios = userController.verAdministradores();
                for (int i = 0; i < listaUsuarios.Count; i++)
                {
                    usuariosInfo.Add(listaUsuarios.ElementAt(i));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}