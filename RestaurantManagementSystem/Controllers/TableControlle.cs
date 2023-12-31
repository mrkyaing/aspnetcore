﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using System.Data;

namespace RestaurantManagementSystem.Controllers {
    public class TableController : Controller {
        private readonly RMSDBContext rMSDBContext;
        private readonly IMapper mapper;

        //constructor injection  for RMSDBContext
        public TableController(RMSDBContext context,IMapper mapper) {
            rMSDBContext = context;
            this.mapper = mapper;
        }
        public IActionResult List() {
            IList<TableViewModel> positions =mapper.Map<List<TableViewModel>>(rMSDBContext.Tables.OrderBy(o => o.No).ToList());
            return View(positions);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Entry() => View();
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Entry(TableViewModel viewModel) {
            try {
                var entity = new TableEntity();
                entity.Id = Guid.NewGuid().ToString();//for new id when uer create the record 36 char GUID  , UUID 
                entity.No = viewModel.No;
                entity.Status=viewModel.Status;
                entity.AvailableCapacityPerson=viewModel.AvailableCapacityPerson;
                entity.IsAvailable = viewModel.IsAvailable.Equals("y") ? true : false;
                rMSDBContext.Tables.Add(entity);//adding the record to the products of db context
                rMSDBContext.SaveChanges();// actually save to the database 
                TempData["Msg"] = "1 record is created successfully";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occur when record is created because of " + ex.Message;
            }
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string Id) {
            try {
                var entity = rMSDBContext.Tables.Where(x => x.Id.Equals(Id)).SingleOrDefault();
                if (entity == null) {
                    TempData["Msg"] = "There is no recrod that you select.";
                }
                rMSDBContext.Tables.Remove(entity);// collect the data to remove
                rMSDBContext.SaveChanges();// remove the record from the database 
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string Id) {
            var viewModel =mapper.Map<TableViewModel>( rMSDBContext.Tables.Where(x => x.Id.Equals(Id)).SingleOrDefault());
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(TableViewModel viewModel) {
            try {
                var entity = new TableEntity();
                entity.Id =viewModel.Id;//for update purpose
                entity.No = viewModel.No;
                entity.Status = viewModel.Status;
                entity.AvailableCapacityPerson = viewModel.AvailableCapacityPerson;
                entity.IsAvailable = viewModel.IsAvailable.Equals("y") ? true : false;
                rMSDBContext.Entry(entity).State = EntityState.Modified;//editing the record to the products of db context
                rMSDBContext.SaveChanges();// actually update to the database 
                TempData["Msg"] = "update process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is updated.";
            }
            return RedirectToAction("List");
        }

    }
}
