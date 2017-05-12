using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface IMenuService
    {
        List<MenuViewModel> GetAllMenu();
        List<MenuViewModel> GetMenuByPId(Guid id);
        MenuViewModel GetMenuById(Guid id);
        void InsertMenu(MenuViewModel viewModel);
        void UpdateMenu(MenuViewModel viewModel);
        void DeleteMenu(Guid id);
    }
}
