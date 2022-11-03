using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.DefaultSystemPath;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using File = Library.Entities.Concrete.File;

namespace Library.Admin.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ILanguageService _languageService;
        private readonly ICategoryService _categoryService;
        private readonly IFileTypeService _fileTypeService;
        private readonly IEducationalProgramService _educationalProgramService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IAuthorService _authorService;
        private readonly IFileAuthorService _fileAuthorService;

        public ResourceController(IFileService fileService, ILanguageService languageService,
            ICategoryService categoryService,
            IFileTypeService fileTypeService, IEducationalProgramService educationalProgramService,
            ISpecialtyService specialtyService, IAuthorService authorService, IFileAuthorService fileAuthorService)
        {
            _fileService = fileService;
            _languageService = languageService;
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
            _educationalProgramService = educationalProgramService;
            _specialtyService = specialtyService;
            _authorService = authorService;
            _fileAuthorService = fileAuthorService;
        }

        #region Resources

        [HttpGet]
        public async Task<IActionResult> ShowResources(int id)
        {
            ResourceViewModel viewModel = new ResourceViewModel();
            var result = await _fileService.GetAll();
            var fileTypes = await _fileTypeService.GetAll();
            switch (id)
            {
                case 0:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 1);
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == false && f.FileType.Id == 1).ToList();
                    break;
                case 1:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 1);
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == true && f.FileType.Id == 1).ToList();
                    break;
                case 2:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 1);
                    viewModel.Files = result.Data.Where(f => f.FileType.Id == 1).ToList();
                    break;
                case 3:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 2);
                    viewModel.Files = result.Data.Where(f => f.FileType.Id == 2).ToList();
                    break;
                case 4:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 2);
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == false && f.FileType.Id == 2).ToList();
                    break;
                case 5:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 2);
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == true && f.FileType.Id == 2).ToList();
                    break;
                case 6:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 3);
                    viewModel.Files = result.Data.Where(f => f.FileType.Id == 3).ToList();
                    break;
                case 7:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 4);
                    viewModel.Files = result.Data.Where(f => f.FileType.Id == 4).ToList();
                    break;
                case 8:
                    viewModel.File.FileType = fileTypes.Data.FirstOrDefault(ft => ft.Id == 5);
                    viewModel.Files = result.Data.Where(f => f.FileType.Id == 5).ToList();
                    break;
                case 9:
                    viewModel.Files = result.Data.Where(f => f.EditionStatus == true).ToList();
                    break;
            }

            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            var authors = await _authorService.GetAll(accessToken);
            List<SelectListItem> authorList = new List<SelectListItem>();
            foreach (var author in authors.Data)
            {
                authorList.Add(new SelectListItem() { Text = $"{author.LastName} {author.FirstName} {author.FatherName}", Value = $"{author.Id}" });
            }
            viewModel.AuthorList = new SelectList(authorList, "Value", "Text");
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SaveResource(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if(accessToken!=null)
            {
                ResourceViewModel viewModel = new ResourceViewModel();
                var categories = await _categoryService.GetAll();
                categories.Data.Add(new Category()
                {
                    Name = "Seç",
                    Id = 0
                });
                var languages = await _languageService.GetAll(accessToken);
                languages.Data.Add(new Language()
                {
                    Name = "Seç",
                    Id = 0
                });
                var fileTypes = await _fileTypeService.GetAll();
                fileTypes.Data.Add(new FileType()
                {
                    Name = "Seç",
                    Id = 0
                });
                var authors = await _authorService.GetAll(accessToken);
                List<SelectListItem> authorList = new List<SelectListItem>();
                foreach (var author in authors.Data)
                {
                    authorList.Add(new SelectListItem() { Text = $"{author.LastName} {author.FirstName} {author.FatherName}", Value = $"{author.Id}" });
                }


                viewModel.CategoryList = new SelectList(categories.Data, "Id", "Name", "Seç");
                viewModel.LanguageList = new SelectList(languages.Data, "Id", "Name", "Seç");
                viewModel.FileTypeList = new SelectList(fileTypes.Data, "Id", "Name", "Seç");
                viewModel.AuthorList = new SelectList(authorList, "Value", "Text");

                if (id == 0)
                    return PartialView(viewModel);

                var file = await _fileService.Get(id);

                viewModel.File = file.Data;

                var authorIds = await _fileAuthorService.GetAllFileAuthors(viewModel.File.Id);
                viewModel.Members = authorIds.Data;

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        //existingStatus
        [HttpPost]
        public async Task<IActionResult> SaveResource(ResourceViewModel viewModel)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if(accessToken!=null)
            {
                try
                {
                    FileAuthorDto fileAuthorDto = new FileAuthorDto()
                    {
                        AuthorIds = new List<int>(viewModel.Members),
                        FileId = viewModel.File.Id

                    };

                    if (viewModel.File.Id == 0)
                    {
                        viewModel.File.FilePath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedFile.FileName);
                        viewModel.File.GUID = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.File.Name);
                        viewModel.File.PhotoPath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedPicture.FileName);

                        var uploadFile = await UploadToFileSystem(viewModel.AddedFile, viewModel.File.FilePath);
                        var uploadPhoto = await UploadToFileSystem(viewModel.AddedPicture, viewModel.File.PhotoPath);

                        viewModel.File.FilePath += uploadFile.Data;
                        viewModel.File.PhotoPath +=uploadPhoto.Data;

                        if (uploadFile.Success && uploadPhoto.Success)
                        {
                            var fileDto = new FileDto()
                            {
                                Id = viewModel.File.Id,
                                Name = viewModel.File.Name,
                                CategoryId = viewModel.File.Category.Id,
                                OriginalLanguageId = viewModel.File.OriginalLanguage.Id,
                                PublicationLanguageId = viewModel.File.PublicationLanguage.Id,
                                EditionStatus = viewModel.File.EditionStatus,
                                ExistingStatus = viewModel.File.ExistingStatus,
                                FileTypeId = viewModel.File.FileType.Id,
                                PhotoPath = viewModel.File.PhotoPath,
                                FilePath = viewModel.File.FilePath,
                                PublisherName = viewModel.File.PublisherName,
                                PublicationDate = viewModel.File.PublicationDate,
                                PageNumber = viewModel.File.PageNumber,
                                Description = viewModel.File.Description,
                                GUID = viewModel.File.GUID,
                                LastModified = viewModel.File.LastModified
                            };

                            var result = await _fileService.Add(accessToken, fileDto);

                            var resultAuthor = await _fileAuthorService.AddAllFileAuthors(accessToken, fileAuthorDto);
                            if (!result.Success && !resultAuthor.Success)
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.File.FilePath));
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));
                            }
                        }
                        else
                        {
                            if (!uploadFile.Success)
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.File.FilePath));
                            }
                            if (!uploadPhoto.Success)
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));
                            }
                            TempData["Message"] = "File isn't upload!";
                        }
                    }
                    else
                    {
                        if(viewModel.AddedFile != null && viewModel.AddedPicture!=null)
                        {
                            string oldFilePath = viewModel.File.FilePath;
                            viewModel.File.FilePath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedFile.FileName);

                            string oldPhotoPath = viewModel.File.PhotoPath;
                            viewModel.File.PhotoPath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedPicture.FileName);

                            var uploadFile = await UploadToFileSystem(viewModel.AddedFile, viewModel.File.FilePath);
                            var uploadPhoto = await UploadToFileSystem(viewModel.AddedPicture, viewModel.File.PhotoPath);
                            viewModel.File.FilePath += uploadFile.Data;
                            viewModel.File.PhotoPath += uploadPhoto.Data;

                            if (uploadFile.Success && uploadPhoto.Success)
                            {
                                var fileDto = new FileDto()
                                {
                                    Id = viewModel.File.Id,
                                    Name = viewModel.File.Name,
                                    CategoryId = viewModel.File.Category.Id,
                                    OriginalLanguageId = viewModel.File.OriginalLanguage.Id,
                                    PublicationLanguageId = viewModel.File.PublicationLanguage.Id,
                                    EditionStatus = viewModel.File.EditionStatus,
                                    ExistingStatus = viewModel.File.ExistingStatus,
                                    FileTypeId = viewModel.File.FileType.Id,
                                    PhotoPath = viewModel.File.PhotoPath,
                                    FilePath = viewModel.File.FilePath,
                                    PublisherName = viewModel.File.PublisherName,
                                    PublicationDate = viewModel.File.PublicationDate,
                                    PageNumber = viewModel.File.PageNumber,
                                    Description = viewModel.File.Description,
                                    GUID = viewModel.File.GUID,
                                    LastModified = viewModel.File.LastModified
                                };

                                var result = await _fileService.Update(accessToken, fileDto);
                                if (result.Success)
                                {
                                    await _fileAuthorService.DeleteFileAuthor(accessToken, viewModel.File.Id);
                                    await _fileAuthorService.AddAllFileAuthors(accessToken, fileAuthorDto);
                                }
                                else
                                {
                                    await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, oldFilePath));
                                    viewModel.File.FilePath = oldFilePath;
                                }
                            }
                            else
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.File.FilePath));
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));

                                viewModel.File.FilePath = oldFilePath;
                                viewModel.File.PhotoPath = oldPhotoPath;
                                TempData["Message"] = "File and photo isn't upload!";
                            }
                        }
                        else if (viewModel.AddedFile != null)
                        {
                            string oldFilePath = viewModel.File.FilePath;
                            viewModel.File.FilePath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedFile.FileName);

                            var uploadFile = await UploadToFileSystem(viewModel.AddedFile, viewModel.File.FilePath);
                            viewModel.File.FilePath += uploadFile.Data;

                            if (uploadFile.Success)
                            {
                                var fileDto = new FileDto()
                                {
                                    Id = viewModel.File.Id,
                                    Name = viewModel.File.Name,
                                    CategoryId = viewModel.File.Category.Id,
                                    OriginalLanguageId = viewModel.File.OriginalLanguage.Id,
                                    PublicationLanguageId = viewModel.File.PublicationLanguage.Id,
                                    EditionStatus = viewModel.File.EditionStatus,
                                    ExistingStatus = viewModel.File.ExistingStatus,
                                    FileTypeId = viewModel.File.FileType.Id,
                                    PhotoPath = viewModel.File.PhotoPath,
                                    FilePath = viewModel.File.FilePath,
                                    PublisherName = viewModel.File.PublisherName,
                                    PublicationDate = viewModel.File.PublicationDate,
                                    PageNumber = viewModel.File.PageNumber,
                                    Description = viewModel.File.Description,
                                    GUID = viewModel.File.GUID,
                                    LastModified = viewModel.File.LastModified
                                };

                                var result = await _fileService.Update(accessToken, fileDto);

                                if (result.Success)
                                {
                                    await _fileAuthorService.DeleteFileAuthor(accessToken, viewModel.File.Id);
                                    await _fileAuthorService.AddAllFileAuthors(accessToken, fileAuthorDto);
                                }
                                else
                                {
                                    await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, oldFilePath));
                                    viewModel.File.FilePath = oldFilePath;
                                }
                            }
                            else
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.File.FilePath));

                                viewModel.File.FilePath = oldFilePath;
                                TempData["Message"] = "File isn't upload!";
                            }
                        }
                        else if (viewModel.AddedPicture != null)
                        {
                            string oldPhotoPath = viewModel.File.FilePath;
                            viewModel.File.PhotoPath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedPicture.FileName);

                            var uploadPhoto = await UploadToFileSystem(viewModel.AddedPicture, viewModel.File.PhotoPath);
                            viewModel.File.PhotoPath += uploadPhoto.Data;

                            if (uploadPhoto.Success)
                            {
                                var fileDto = new FileDto()
                                {
                                    Id = viewModel.File.Id,
                                    Name = viewModel.File.Name,
                                    CategoryId = viewModel.File.Category.Id,
                                    OriginalLanguageId = viewModel.File.OriginalLanguage.Id,
                                    PublicationLanguageId = viewModel.File.PublicationLanguage.Id,
                                    EditionStatus = viewModel.File.EditionStatus,
                                    ExistingStatus = viewModel.File.ExistingStatus,
                                    FileTypeId = viewModel.File.FileType.Id,
                                    PhotoPath = viewModel.File.PhotoPath,
                                    FilePath = viewModel.File.FilePath,
                                    PublisherName = viewModel.File.PublisherName,
                                    PublicationDate = viewModel.File.PublicationDate,
                                    PageNumber = viewModel.File.PageNumber,
                                    Description = viewModel.File.Description,
                                    GUID = viewModel.File.GUID,
                                    LastModified = viewModel.File.LastModified
                                };

                                var result = await _fileService.Update(accessToken, fileDto);

                                if (result.Success)
                                {
                                    await _fileAuthorService.DeleteFileAuthor(accessToken, viewModel.File.Id);
                                    await _fileAuthorService.AddAllFileAuthors(accessToken, fileAuthorDto);
                                }
                                else
                                {
                                    await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, oldPhotoPath));
                                    viewModel.File.PhotoPath = oldPhotoPath;
                                }
                            }
                            else
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, viewModel.File.PhotoPath));
                                viewModel.File.PhotoPath = oldPhotoPath;
                                TempData["Message"] = "File isn't upload!";
                            }
                        }
                        else
                        {
                            var fileDto = new FileDto()
                            {
                                Id = viewModel.File.Id,
                                Name = viewModel.File.Name,
                                CategoryId = viewModel.File.Category.Id,
                                OriginalLanguageId = viewModel.File.OriginalLanguage.Id,
                                PublicationLanguageId = viewModel.File.PublicationLanguage.Id,
                                EditionStatus = viewModel.File.EditionStatus,
                                ExistingStatus = viewModel.File.ExistingStatus,
                                FileTypeId = viewModel.File.FileType.Id,
                                PhotoPath = viewModel.File.PhotoPath,
                                FilePath = viewModel.File.FilePath,
                                PublisherName = viewModel.File.PublisherName,
                                PublicationDate = viewModel.File.PublicationDate,
                                PageNumber = viewModel.File.PageNumber,
                                Description = viewModel.File.Description,
                                GUID = viewModel.File.GUID,
                                LastModified = viewModel.File.LastModified
                            };

                            await _fileService.Update(accessToken, fileDto);
                            await _fileAuthorService.DeleteFileAuthor(accessToken, viewModel.File.Id);
                            await _fileAuthorService.AddAllFileAuthors(accessToken, fileAuthorDto);
                        }
                    }
                    TempData["Message"] = "Operation successfully";
                }
                catch (Exception exc)
                {
                    // log exception here

                    TempData["Message"] = "Operation unsuccessfully";
                }
                return RedirectToAction("ShowResources","Resource", "2");
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteResource(ResourceViewModel viewModel)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");

            if(token!=null)
            {
                var file = await _fileService.Get(viewModel.DeletedResource.Id);

                var deleteFileFromFileSytem = await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, file.Data.FilePath));
                var deletePhotoFromFileSytem = await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultPhotoPath, file.Data.PhotoPath));

                var result = await _fileService.Delete(token, viewModel.DeletedResource.Id);

                if (result.Success)
                {
                    TempData["Message"] = "Operation successfully";
                    return RedirectToAction("ShowResources","Resource", "2");
                }
                // Must be return error message to UI
                return RedirectToAction("ShowResources","Resource", "2");
            }
            return new NotFoundResult();
        }

        #endregion

        #region EducationalPrograms
        [HttpGet]
        public async Task<IActionResult> ShowEducationalPrograms()
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
            {
                ResourceViewModel viewModel = new ResourceViewModel();
                var educationalPrograms = await _educationalProgramService.GetAll(accessToken);
                viewModel.EducationalPrograms = educationalPrograms.Data;
                return View(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveEducationalProgram(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if(accessToken!=null)
            {
                ResourceViewModel viewModel = new ResourceViewModel();
                var specialties = await _specialtyService.GetAll(accessToken);
                specialties.Data.Add(new Specialty()
                {
                    Id = 0,
                    Name = "Seç"
                });
                viewModel.SpecialtyList = new SelectList(specialties.Data, "Id", "Name", "Seç");

                List<dynamic> educationLevels = new List<dynamic>();
                educationLevels.Add(new { Id = 0, Name = "Seç" });
                educationLevels.Add(new { Id = 1, Name = "Bakalavr" });
                educationLevels.Add(new { Id = 2, Name = "Magistr" });
                educationLevels.Add(new { Id = 3, Name = "Doktorant" });
                viewModel.EducationLevelList = new SelectList(educationLevels, "Name", "Name", "Seç");

                if (id == 0)
                    return PartialView(viewModel);

                var educationalProgram = await _educationalProgramService.Get(accessToken, id);

                viewModel.EducationalProgram = educationalProgram.Data;

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEducationalProgram(ResourceViewModel viewModel)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if(accessToken!=null)
            {
                try
                {
                    if (viewModel.EducationalProgram.Id == 0)
                    {
                        viewModel.EducationalProgram.FilePath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedFile.FileName);
                        viewModel.EducationalProgram.GUID = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.EducationalProgram.Name);

                        var uploadFile = await UploadToFileSystem(viewModel.AddedFile, viewModel.EducationalProgram.FilePath);
                        
                        viewModel.EducationalProgram.FilePath += uploadFile.Data;

                        if (uploadFile.Success)
                        {
                            var educationalProgramDto = new EducationalProgramDto()
                            {
                                Id = viewModel.EducationalProgram.Id,
                                Name = viewModel.EducationalProgram.Name,
                                EducationLevel = viewModel.EducationalProgram.EducationLevel,
                                FilePath = viewModel.EducationalProgram.FilePath,
                                GUID = viewModel.EducationalProgram.GUID,
                                IsActive = viewModel.EducationalProgram.IsActive,
                                LastModified = viewModel.EducationalProgram.LastModified,
                                SpecialtyId = viewModel.EducationalProgram.Specialty.Id,
                                ProgramDate = viewModel.EducationalProgram.ProgramDate,
                                EducationTime = viewModel.EducationalProgram.EducationTime,
                            };

                            var result = await _educationalProgramService.Add(accessToken, educationalProgramDto);

                            if (!result.Success)
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.EducationalProgram.FilePath));
                            }
                        }
                        else
                        {
                            await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.EducationalProgram.FilePath));
                            TempData["Message"] = "File isn't upload!";
                        }
                    }
                    else
                    {
                        if (viewModel.AddedFile != null)
                        {
                            string oldFilePath = viewModel.EducationalProgram.FilePath;
                            viewModel.EducationalProgram.FilePath = UniqueNameGenerator.UniqueFilePathGenerator(viewModel.AddedFile.FileName);

                            var uploadFile = await UploadToFileSystem(viewModel.AddedFile, viewModel.EducationalProgram.FilePath);
                            viewModel.EducationalProgram.FilePath += uploadFile.Data;

                            if (uploadFile.Success)
                            {
                                var educationalProgramDto = new EducationalProgramDto()
                                {
                                    Id = viewModel.EducationalProgram.Id,
                                    Name = viewModel.EducationalProgram.Name,
                                    EducationLevel = viewModel.EducationalProgram.EducationLevel,
                                    FilePath = viewModel.EducationalProgram.FilePath,
                                    GUID = viewModel.EducationalProgram.GUID,
                                    IsActive = viewModel.EducationalProgram.IsActive,
                                    LastModified = viewModel.EducationalProgram.LastModified,
                                    SpecialtyId = viewModel.EducationalProgram.Specialty.Id,
                                    ProgramDate = viewModel.EducationalProgram.ProgramDate,
                                    EducationTime = viewModel.EducationalProgram.EducationTime,
                                };

                                var result = await _educationalProgramService.Update(accessToken, educationalProgramDto);

                                if (!result.Success)
                                {
                                    await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, oldFilePath));
                                    viewModel.EducationalProgram.FilePath = oldFilePath;
                                }
                            }
                            else
                            {
                                await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, viewModel.EducationalProgram.FilePath));

                                viewModel.EducationalProgram.FilePath = oldFilePath;
                                TempData["Message"] = "File isn't upload!";
                            }
                        }
                        else
                        {
                            var educationalProgramDto = new EducationalProgramDto()
                            {
                                Id = viewModel.EducationalProgram.Id,
                                Name = viewModel.EducationalProgram.Name,
                                EducationLevel = viewModel.EducationalProgram.EducationLevel,
                                FilePath = viewModel.EducationalProgram.FilePath,
                                GUID = viewModel.EducationalProgram.GUID,
                                IsActive = viewModel.EducationalProgram.IsActive,
                                LastModified = viewModel.EducationalProgram.LastModified,
                                SpecialtyId = viewModel.EducationalProgram.Specialty.Id,
                                ProgramDate = viewModel.EducationalProgram.ProgramDate,
                                EducationTime = viewModel.EducationalProgram.EducationTime,
                            };

                            var result2 = await _educationalProgramService.Update(accessToken, educationalProgramDto);
                        }
                    }
                    TempData["Message"] = "Operation successfully";
                }
                catch (Exception exc)
                {
                    // log exception here

                    TempData["Message"] = "Operation unsuccessfully";
                }

                return RedirectToAction("ShowEducationalPrograms");
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEducationalProgram(ResourceViewModel viewModel)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if(token!=null)
            {
                var educationalProgram = await _educationalProgramService.Get(token, viewModel.DeletedEducationalProgram.Id);

                var deleteFileFromFileSytem = await DeleteFileFromFileSystem(Path.Combine(DefaultPath.OriginalDefaultFilePath, educationalProgram.Data.FilePath));

                var result = await _educationalProgramService.Delete(token, viewModel.DeletedEducationalProgram.Id);

                if (result.Success)
                {
                    TempData["Message"] = "Operation successfully";
                    return RedirectToAction("ShowEducationalPrograms");
                }
                // Must be return error message to UI
                return RedirectToAction("ShowEducationalPrograms");
            }
            return new NotFoundResult();
        }

        #endregion

        #region FileUpload&Delete
        [HttpPost]
        public async Task<DataResult<string>> UploadToFileSystem(IFormFile file, string uniqueFileName)
        {
            bool basePhotoPathExists = System.IO.Directory.Exists(DefaultPath.OriginalDefaultPhotoPath);
            if (!basePhotoPathExists) Directory.CreateDirectory(DefaultPath.OriginalDefaultPhotoPath);

            bool baseFilePathExists = System.IO.Directory.Exists(DefaultPath.OriginalDefaultFilePath);
            if (!baseFilePathExists) Directory.CreateDirectory(DefaultPath.OriginalDefaultFilePath);

            string extension = Path.GetExtension(file.FileName);
            StringBuilder builder = new StringBuilder();
            builder.Append(uniqueFileName);
            builder.Append(extension);
            string filePath;
            if (file.ContentType.Contains("image"))
                filePath = Path.Combine(DefaultPath.OriginalDefaultPhotoPath, builder.ToString());
            else
                filePath = Path.Combine(DefaultPath.OriginalDefaultFilePath, builder.ToString());


            if (!System.IO.File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return new SuccessDataResult<string>(extension,"Successfully uploaded!");
            }
            return new ErrorDataResult<string>();
        }

        public async Task<Result> DeleteFileFromFileSystem(string fullFilePath)
        {
            if (System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Delete(fullFilePath);
            }
            TempData["Message"] = $"Removed this file successfully from File System.";
            return new SuccessResult();
        }

        public async Task<IActionResult> DownloadFileFromFileSystem(File file)
        {
            string filePath, contentType, filename;

            filePath = Path.Combine(DefaultPath.OriginalDefaultFilePath, file.FilePath);
            contentType = "pdf/*";
            filename = file.FilePath;

            if (file == null) return null;
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, contentType, filename);
        }

        [HttpGet]
        public IActionResult ShowPhoto(string path)
        {
            try
            {
                string fullpath = Path.Combine(DefaultPath.OriginalDefaultPhotoPath, path);

                var content = System.IO.File.ReadAllBytes(fullpath);

                return File(content, "img/*");
            }
            catch
            {
                return Ok();
            }
        }

        [HttpGet]
        public IActionResult ShowPhotoModal(string path)
        {
            ResourceViewModel viewModel = new ResourceViewModel()
            {
                File = new File()
                {
                    PhotoPath = path
                }
            };
            return PartialView(viewModel);
        }

        [HttpGet]
        public IActionResult OpenFile(string path)
        {
            try
            {
                string fullPath = Path.Combine(DefaultPath.OriginalDefaultFilePath, path);

                var content = System.IO.File.ReadAllBytes(fullPath);
                return File(content, "application/pdf");
            }
            catch
            {
                return BadRequest("Not found");
            }
        }

        /*public IActionResult QrCodeGenerator(string guid, string fileName)
        {
            //Generate normal QrCode
            //QRCodeWriter.CreateQrCode("Ehmed başına daş. Geberrr", 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("MyQR.png");

            string filename = Guid.NewGuid().ToString();

            *//*var MyQRWithLogo = QRCodeWriter.CreateQrCodeWithLogo(Defaults.DefaultUrlForQRCode+guid,
                "wwwroot/logo.png", 500).SaveAsPng(Defaults.DefaultPhotoPath + photopath);
            *//*
            return RedirectToAction("Index", "File");
        }*/
        #endregion

    }
}
