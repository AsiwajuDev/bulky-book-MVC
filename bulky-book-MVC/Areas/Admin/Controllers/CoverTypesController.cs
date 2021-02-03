using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBookMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverTypes coverTypes = new CoverTypes();
            if(id == null)
            {
                //to create
                return View(coverTypes);
            }
            //to edit
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            coverTypes = _unitOfWork.StoredProcedure_Call.OneRecord<CoverTypes>(SD.Proc_CoverType_Get, parameter);

            if (coverTypes == null)
            {
                return NotFound();
            }
            return View(coverTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverTypes coverTypes)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverTypes.Name);
                if (coverTypes.Id == 0)
                {
                    _unitOfWork.StoredProcedure_Call.Execute(SD.Proc_CoverType_Create, parameter);
                    ;
                }
                else
                {
                    parameter.Add("@Id", coverTypes.Id);
                    _unitOfWork.StoredProcedure_Call.Execute(SD.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverTypes);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.StoredProcedure_Call.List<CoverTypes>(SD.Proc_CoverType_GetAll, null);
            return Json(new { data = allObj });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.StoredProcedure_Call.OneRecord<CoverTypes>(SD.Proc_CoverType_Get, parameter);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitOfWork.StoredProcedure_Call.Execute(SD.Proc_CoverType_Delete, parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
