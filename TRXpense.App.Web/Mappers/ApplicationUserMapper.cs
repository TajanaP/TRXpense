using System.Collections.Generic;
using System.Web.Mvc;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.Mappers
{
    public static class ApplicationUserMapper
    {
        public static RegisterViewModel MapToView(this ApplicationUser model)
        {
            if (model == null)
                return null;

            return new RegisterViewModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                OIB = model.OIB,
                Phone = model.Phone,
                Position = model.Position,
                UserRole = model.UserRole,
                CostCenter = model.CostCenter,
                CostCenterId = model.CostCenterId,
                Superior = model.Superior,
                SuperiorId = model.SuperiorId
            };
        }

        public static List<RegisterViewModel> MapToViews(this List<ApplicationUser> model)
        {
            if (model == null)
                return null;

            var result = new List<RegisterViewModel>();
            foreach (var item in model)
            {
                result.Add(item.MapToView());
            }

            return result;
        }

        // mapiranje iz Application user-a u RegisterViewModelEdit
        public static RegisterViewModelEdit MapToViewEdit(this ApplicationUser model)
        {
            if (model == null)
                return null;

            return new RegisterViewModelEdit()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                OIB = model.OIB,
                Phone = model.Phone,
                Position = model.Position,
                UserRole = model.UserRole,
                CostCenter = model.CostCenter,
                CostCenterId = model.CostCenterId,
                Superior = model.Superior,
                SuperiorId = model.SuperiorId
            };
        }

        // mapiranje iz RegisterViewModelEdit u Application user-a
        public static ApplicationUser MapToModelEdit(this RegisterViewModelEdit view)
        {
            if (view == null)
                return null;

            return new ApplicationUser()
            {
                Id = view.Id,
                FirstName = view.FirstName,
                LastName = view.LastName,
                UserName = view.Email,
                Email = view.Email,
                DateOfBirth = view.DateOfBirth,
                OIB = view.OIB,
                Phone = view.Phone,
                Position = view.Position,
                UserRole = view.UserRole,
                CostCenterId = view.CostCenterId,
                SuperiorId = view.SuperiorId
            };
        }

        public static SelectListItem MapToListUser(this ApplicationUser model)
        {
            if (model == null)
                return null;

            return new SelectListItem()
            {
                Text = model.LastName,
                Value = model.Id
            };
        }

        public static List<SelectListItem> MapToListUsers(this List<ApplicationUser> model)
        {
            if (model == null)
                return null;

            var result = new List<SelectListItem>();
            foreach (var item in model)
            {
                result.Add(item.MapToListUser());
            }

            return result;
        }

        public static SelectListItem MapToListCostCenter(this CostCenter model)
        {
            if (model == null)
                return null;

            return new SelectListItem()
            {
                Text = model.Name,
                Value = model.Id.ToString()
            };
        }

        public static List<SelectListItem> MapToListCostCenters(this List<CostCenter> model)
        {
            if (model == null)
                return null;

            var result = new List<SelectListItem>();
            foreach (var item in model)
            {
                result.Add(item.MapToListCostCenter());
            }

            return result;
        }
    }
}