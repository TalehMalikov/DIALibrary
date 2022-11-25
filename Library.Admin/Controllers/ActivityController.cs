using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.DefaultSystemPath;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Library.Entities.Concrete;

namespace Library.Admin.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token == null) return RedirectToAction("Login", "Authentication");
            var result = await _activityService.GetAll();
            var model = new ActivityViewModel
            {
                Activities = result.Data
            };
            return View(model);
        }

        public IActionResult AddActivity()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token == null) return RedirectToAction("Login", "Authentication");
            return View();
        }

        public async Task<IActionResult> ShowActivity(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token == null) return RedirectToAction("Login", "Authentication");
            var activity = await _activityService.Get(id);
            var model = new ActivityViewModel
            {
                Activity = activity.Data
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActivity(ActivityViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");

            if (token == null) return RedirectToAction("Login", "Authentication");

            if (model.ActivityPhoto != null)
            {
                model.Activity.PhotoPath = UniqueNameGenerator.UniqueFilePathGenerator(model.ActivityPhoto.FileName);

                var result = await UploadToFileSystem(model.ActivityPhoto, model.Activity.PhotoPath);

                if (result.Success)
                {
                    var addResult = await _activityService.Add(token, new Activity
                    {
                        Content = model.Activity.Content,
                        CreatedDate = model.Activity.CreatedDate,
                        LastModified = DateTime.Now,
                        PhotoPath = model.Activity.PhotoPath+result.Data,
                        Title = model.Activity.Title,
                        IsDeleted = false
                    });
                    if (addResult.Success) return RedirectToAction("Index", "Activity");
                    DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultActivityPhotoPath, model.Activity.PhotoPath));
                    return RedirectToAction("Index","Activity");
                }
            }


            var response = await _activityService.Add(token, new Activity
            {
                Content = model.Activity.Content,
                CreatedDate = model.Activity.CreatedDate,
                LastModified = DateTime.Now,
                PhotoPath = "",
                Title = model.Activity.Title,
                IsDeleted = false
            });
            if (response.Success) return RedirectToAction("Index", "Activity");
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token == null) return RedirectToAction("Login", "Authentication");
            await _activityService.Delete(token, id);
            return PartialView("SuccessfullyCompleted");
        }

        public async Task<IActionResult> DeleteFromDb(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token == null) return RedirectToAction("Login", "Authentication");
            var activity = await _activityService.Get(id);
            var result = await _activityService.DeleteFromDb(token, id);
            if (result.Success)
                DeleteFileFromFileSystem(activity.Data.PhotoPath);
            return RedirectToAction("DeactivatedActivities");

        }

       
        public async Task<IActionResult> Update(int id)
        {
            if (HttpContext.Session.GetString("AdminAccessToken") == null)
                return RedirectToAction("Login", "Authentication");
            var result = await _activityService.Get(id);
            var model = new ActivityViewModel
            {
                Activity = result.Data,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ActivityViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            Result result;
            if (model.ActivityPhoto == null)
            {
                result = await _activityService.Update(token, new Activity
                {
                    Id = model.Activity.Id,
                    PhotoPath = model.Activity.PhotoPath,
                    Content = model.Activity.Content,
                    CreatedDate = model.Activity.CreatedDate,
                    IsDeleted = model.Activity.IsDeleted,
                    LastModified = DateTime.Now,
                    Title = model.Activity.Title
                });
                if (result.Success)
                    return RedirectToAction("Index","Activity");
            }
            else if (model.ActivityPhoto != null)
            {
                string fileName = UniqueNameGenerator.UniqueFilePathGenerator(model.ActivityPhoto.FileName);
                var newPhotoPath = await UploadToFileSystem(model.ActivityPhoto, fileName);
                if (newPhotoPath.Success)
                {
                    var deleteResponse = DeleteFileFromFileSystem(model.Activity.PhotoPath);
                    if (deleteResponse.Success)
                    {
                        result = await _activityService.Update(token, new Activity
                        {
                            Id = model.Activity.Id,
                            PhotoPath = fileName + newPhotoPath.Data,
                            Content = model.Activity.Content,
                            CreatedDate = model.Activity.CreatedDate,
                            IsDeleted = model.Activity.IsDeleted,
                            LastModified = DateTime.Now,
                            Title = model.Activity.Title
                        });
                        if (result.Success)
                            return RedirectToAction("Index", "Activity");
                    }
                    else
                    {
                        DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultActivityPhotoPath, newPhotoPath.Data));
                    }
                }
            }

            return View(model);
        }

        
        public async Task<IActionResult> Activate(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token == null) return RedirectToAction("Login", "Authentication");
            await _activityService.Activate(token, id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeactivatedActivities()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token == null) return RedirectToAction("Login", "Authentication");
            var result = await _activityService.GetDeletedActivities(token);
            var model = new ActivityViewModel
            {
                Activities = result.Data
            };
            return View(model);
            }

        #region Upload and Delete Photo
        private IActionResult GetPhoto(string path)
        {
            try
            {
                string fullpath = Path.Combine(DefaultPath.OriginalDefaultActivityPhotoPath, path);

                var content = System.IO.File.ReadAllBytes(fullpath);

                return File(content, "img/*");
            }
            catch
            {
                return Ok();
            }
        }
        private async Task<DataResult<string>> UploadToFileSystem(IFormFile file, string uniqueFileName)
        {
            bool basePhotoPathExists = Directory.Exists(DefaultPath.OriginalDefaultActivityPhotoPath);
            if (!basePhotoPathExists) Directory.CreateDirectory(DefaultPath.OriginalDefaultActivityPhotoPath);

            string extension = Path.GetExtension(file.FileName);
            StringBuilder builder = new StringBuilder();
            builder.Append(uniqueFileName);
            builder.Append(extension);
            string filePath = Path.Combine(DefaultPath.OriginalDefaultActivityPhotoPath, builder.ToString());


            if (!System.IO.File.Exists(filePath))
            {
                using var stream = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(stream);

                return new SuccessDataResult<string>(extension, "Successfully uploaded!");
            }
            return new ErrorDataResult<string>();
        }

        private Result DeleteFileFromFileSystem(string fullFilePath)
        {

            if (System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Delete(fullFilePath);
            }
            TempData["Message"] = $"Removed this file successfully from File System.";
            return new SuccessResult();
        }

        #endregion

    }
}
