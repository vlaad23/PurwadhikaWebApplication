using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PurwadhikaWebApplication.Models;

namespace PurwadhikaWebApplication.APIController
{
    public class CategoryController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Category
        public IQueryable<CategoryMaster> GetCategoryMaster()
        {
            return db.CategoryMaster;
        }

        [HttpGet]
        [Route("~/api/category")]
        public IHttpActionResult GetCategory()
        {
            var category = db.CategoryMaster.ToList();
            return Ok(category);
        }

        // GET: api/Category/5
        [ResponseType(typeof(CategoryMaster))]
        public IHttpActionResult GetCategoryMaster(int id)
        {
            CategoryMaster categoryMaster = db.CategoryMaster.Find(id);
            if (categoryMaster == null)
            {
                return NotFound();
            }

            return Ok(categoryMaster);
        }

        // PUT: api/Category/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoryMaster(int id, CategoryMaster categoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryMaster.Id)
            {
                return BadRequest();
            }

            db.Entry(categoryMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Category
        [ResponseType(typeof(CategoryMaster))]
        public IHttpActionResult PostCategoryMaster(CategoryMaster categoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoryMaster.Add(categoryMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoryMaster.Id }, categoryMaster);
        }

        // DELETE: api/Category/5
        [ResponseType(typeof(CategoryMaster))]
        public IHttpActionResult DeleteCategoryMaster(int id)
        {
            CategoryMaster categoryMaster = db.CategoryMaster.Find(id);
            if (categoryMaster == null)
            {
                return NotFound();
            }

            db.CategoryMaster.Remove(categoryMaster);
            db.SaveChanges();

            return Ok(categoryMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryMasterExists(int id)
        {
            return db.CategoryMaster.Count(e => e.Id == id) > 0;
        }
    }
}