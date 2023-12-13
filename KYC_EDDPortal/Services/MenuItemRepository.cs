using KYC_EDDPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Services
{
    public class MenuItemRepository
    {
        private static IList<MenuItems> _menus;

        public static IList<MenuItems> Menus()
        {
            if (_menus != null)
                return _menus;

            _menus = new List<MenuItems>
            {
                //MenuItems("Code","MenuName","MenuTitle","Controllers","Action","Parameter")
                new MenuItems("A01","Initiation","Initiation", "Home","Initiation",null,1),
                new MenuItems("A02","KYCReview","KYCReview", "Home","KYCReview",null,2),
                new MenuItems("A03","EDDReview","EDDReview", "Home","EDDReview",null,8),
                new MenuItems("A04","Review","Review", "Home","Review",null,1),
                
            };
            return _menus;
        }



    }
}
