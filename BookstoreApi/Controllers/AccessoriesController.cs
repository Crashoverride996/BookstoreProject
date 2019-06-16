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

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoriesController : ControllerBase
    {
        private IGetAccessoriesCommand _getAccs;
        private IGetAccessoryCommand _getAcc;
        private IAddAccessoryCommand _addAcc;
        private IDeleteAccessoryCommand _delAcc;
        private IEditAccessoryCommand _editAcc;

        public AccessoriesController(IGetAccessoriesCommand getAccs, IGetAccessoryCommand getAcc, IAddAccessoryCommand addAcc, IDeleteAccessoryCommand delAcc, IEditAccessoryCommand editAcc)
        {
            _getAccs = getAccs;
            _getAcc = getAcc;
            _addAcc = addAcc;
            _delAcc = delAcc;
            _editAcc = editAcc;
        }



        // GET: api/Accessories
        [HttpGet]
        public IActionResult Get([FromQuery] AccessorySearch search)
        {
            var accessories = _getAccs.Execute(search);
            return Ok(accessories);
        }

        // GET: api/Accessories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var accessory = _getAcc.Execute(id);
                return Ok(accessory);
            }
            catch (EntityNotFoundException e)
            {
                if (e.Message == "Accessory not found")
                {
                    return NotFound(e.Message);
                }

                return UnprocessableEntity(e.Message);

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // POST: api/Accessories
        [HttpPost]
        public IActionResult Post([FromBody] AccessoryDto accessory)
        {
            try
            {
                _addAcc.Execute(accessory);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Accessories/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateAccessoryDto accessory)
        {
            accessory.Id = id;
            try
            {
                _editAcc.Execute(accessory);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                if (e.Message == "Accessory doesn't exist.")
                {
                    return NotFound(e.Message);
                }

                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _delAcc.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
