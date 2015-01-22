using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileWithEncryption.Models;
using System.IO;
using System.Data.Entity.Validation;
using System.Text;

namespace FileWithEncryption.Controllers
{
    public class HomeController : Controller
    {
        POCEntities entity = new POCEntities();
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase image)
        {
            try
            {
                byte[] imgBype = null;
                using (BinaryReader breader = new BinaryReader(image.InputStream))
                {
                    imgBype = breader.ReadBytes(image.ContentLength);
                }



                var key = Guid.NewGuid();

                byte[] encriptByte = SymmetricEncryption.Encrypt(imgBype, key.ToString());

                //FileWithEncryption.Models.File file = new Models.File();
                //file.data = encriptByte;
                //file.doctype = image.ContentType;
                //file.name = image.FileName;
                //file.type = image.ContentType;
                //file.encryptkey = key.ToString();

                //entity.Files.Add(file);
                //entity.SaveChanges();

                int size = 1024;
                string publicKey;
                string privatepublicKey;
                AsymmetricEncryption.GenerateKeys(size, out publicKey, out privatepublicKey);

                string encryptName = AsymmetricEncryption.EncryptText(image.FileName, size, publicKey);

                string encryptType = AsymmetricEncryption.EncryptText(image.ContentType, size, publicKey);

                FileUploadDB fileup = new FileUploadDB();

                fileup.asymprivatepublicKey = privatepublicKey;
                fileup.filedata = encriptByte;
                fileup.name = encryptName;
                fileup.type = encryptType;
                fileup.symmentryKey = key.ToString();

                entity.FileUploadDBs.Add(fileup);
                entity.SaveChanges();

                return RedirectToAction("fileUploadList");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string value = validationError.PropertyName;
                        string message = validationError.ErrorMessage;
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult UploadList()
        {
            List<FileWithEncryption.Models.File> listFile = new List<Models.File>();
            listFile = entity.Files.ToList();
            return View(listFile);
        }

        [HttpGet]
        public ActionResult fileUploadList()
        {
            List<FileUploadDB> listFile = new List<FileUploadDB>();
            listFile = entity.FileUploadDBs.ToList();
            foreach (var item in listFile)
            {
                item.name = AsymmetricEncryption.DecryptText(item.name, 1024, item.asymprivatepublicKey);
                item.type = AsymmetricEncryption.DecryptText(item.type, 1024, item.asymprivatepublicKey);
            }
            return View(listFile);
        }

        [HttpGet]
        public ActionResult DownloadFile(int fileId)
        {
            var filedata = entity.FileUploadDBs.Where(x => x.asymfileid == fileId).FirstOrDefault();
            string filename = AsymmetricEncryption.DecryptText(filedata.name, 1024, filedata.asymprivatepublicKey);
            string contentType = AsymmetricEncryption.DecryptText(filedata.type, 1024, filedata.asymprivatepublicKey);
            byte[] file = SymmetricEncryption.Decrypt(filedata.filedata, filedata.symmentryKey);
            return File(file, contentType, filename);
        }

        [HttpGet]
        public ActionResult Download(int fileId)
        {
            var filedata = entity.Files.Where(x => x.fileid == fileId).FirstOrDefault();
            byte[] decriptByte = SymmetricEncryption.Decrypt(filedata.data, filedata.encryptkey);
            return File(decriptByte, filedata.type, filedata.name);
        }
    }
}
