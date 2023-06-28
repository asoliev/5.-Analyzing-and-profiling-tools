﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ProfileSampleEntities())
            {
                var sources = context.ImgSources
                    .Take(20)
                    .Select(x => new ImageModel()
                    {
                        Name = x.Name,
                        Data = x.Data
                    }).ToList();
                return View(sources);
            }
        }

        public ActionResult UploadImages()
        {
            var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), "*.jpg");

            using (var context = new ProfileSampleEntities())
            {
                foreach (var file in files)
                {
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            var entity = new ImgSource()
                            {
                                Name = Path.GetFileName(file),
                                Data = reader.ReadBytes((int)stream.Length)
                            };

                            context.ImgSources.Add(entity);
                            context.SaveChanges();
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Some information";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}