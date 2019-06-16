using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using BookstoreAplication.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly IGetAccessoryCommand _getAccessory;
        private readonly IGetAccessoriesWebCommand _getAccessories;
        private readonly IAddAccessoryCommand _addAccessory;
        private readonly IEditAccessoryCommand _editAccessory;
        private readonly IDeleteAccessoryCommand _delAccessory;

        public AccessoriesController(IGetAccessoryCommand getAccessory, IGetAccessoriesWebCommand getAccessories, IAddAccessoryCommand addAccessory, IEditAccessoryCommand editAccessory, IDeleteAccessoryCommand delAccessory)
        {
            _getAccessory = getAccessory;
            _getAccessories = getAccessories;
            _addAccessory = addAccessory;
            _editAccessory = editAccessory;
            _delAccessory = delAccessory;
        }

        // GET: Accessories
        public ActionResult Index(AccessorySearch accessory)
        {
            var accessories = _getAccessories.Execute(accessory);
            return View(accessories);
        }

        // GET: Accessories/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var accessory = _getAccessory.Execute(id);
                return View(accessory);
            }
            catch (Exception e)
            {
                TempData["error"] = e.ToString();
                return View();
            }
        }

        // GET: Accessories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accessories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccessoryDto accessory)
        {
            if (!ModelState.IsValid)
            {
                return View(accessory);
            }
            try
            {
                _addAccessory.Execute(accessory);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Accessory with that name already exists";
            }
            catch (Exception e)
            {
                TempData["error"] = e.ToString();
            }
            return View();
        }

        // GET: Accessories/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Accessories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateAccessoryDto accessory)
        {
            accessory.Id = id;
            try
            {
                _editAccessory.Execute(accessory);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Accessories/Delete/5
        
        public ActionResult Delete(int id)
        {
            var accessory = _getAccessory.Execute(id);
            return View(accessory);
        }
        

        // POST: Accessories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _delAccessory.Execute(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}